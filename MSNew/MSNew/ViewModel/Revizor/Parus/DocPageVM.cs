using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Plugin.Settings;
using System.Collections.ObjectModel;
using MSNew.Model.Revizor.Parus;
using Xamarin.Forms;
using Acr.UserDialogs;
using MSNew.Controller;
using Newtonsoft.Json;
using static MSNew.Controller.HttpJsonItem;
using System.Windows.Input;

namespace MSNew.ViewModel.Revizor.Parus
{
    public class DocPageVM : INotifyPropertyChanged
    {
        ObservableCollection<ParusRevBox> RevizorBox { get; set; }
        public ParusRevBox SelectRevizorBox { get; set; }

        public INavigation Navigation { get; set; }
        public ICommand NewBox { get; set; }
        public ICommand MarkShow { get; set; }

        public string term_name;
        public string doc_rn;
        public string box_rn;

        public bool isMarkShow { get; set; }
        public DocPageVM()
        {
            term_name = CrossSettings.Current.GetValueOrDefault("TerminalNumber", "");
            doc_rn = "0";
            box_rn = "0";
            isMarkShow = false;

            RevizorBox = new ObservableCollection<ParusRevBox>();
            SelectRevizorBox = new ParusRevBox();
            NewBox = new Command(addBox);
            MarkShow = new Command(showBox);
        }

        public void startPage()
        {
            MessagingCenter.Subscribe<string, string>("MainActivity", "GetBarcode", (sender, arg) =>
            {
                work(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "PostRevizor", (sender, arg) =>
            {
                postResponce(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetRevizorHead", (sender, arg) =>
            {
                fillHead(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetRevizorBox", (sender, arg) =>
            {
                fillList(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("RevizorBox", "BoxNum", (sender, arg) =>
            {
                sendBox(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "Error", (sender, arg) =>
            {
                showError(arg.Trim());
            });
        }

        public void endPage()
        {
            MessagingCenter.Unsubscribe<string, string>("MainActivity", "GetBarcode");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "PostRevizor");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetRevizorHead");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetRevizorBox");
            MessagingCenter.Unsubscribe<string, string>("RevizorBox", "BoxNum");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "Error");
        }

        public void work(string _barcode)
        {
            UserDialogs.Instance.ShowLoading("Обмен данными");
            HttpControler.SendPostRevizor(term_name, _barcode, doc_rn, "0", box_rn, "0", "PostRevizor");
        }

        public void postResponce(string content)
        {
            try
            {
                isMarkShow = false;
                PostRevizorResponce tmp = JsonConvert.DeserializeObject<PostRevizorResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                if (tmp.out_responce == "-1")   //ошибка
                {
                    UserDialogs.Instance.HideLoading();
                    this.showError(tmp.comment);
                }
                if (tmp.out_responce == "0")    //ввод документа
                {
                    doc_rn = tmp.doc_rn;
                    HttpControler.SendGetRevizorHead(doc_rn);
                    HttpControler.SendGetRevizorBox(doc_rn);
                }
                if (tmp.out_responce == "1")    //добавление коробки с количеством
                {
                    box_rn = tmp.box_rn;
                    isMarkShow = true;
                    HttpControler.SendGetRevizorBox(doc_rn);
                    UserDialogs.Instance.HideLoading();
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                this.showError(ex.Message);
            }
        }

        public void fillHead(string content)
        {
            try
            {
                GetRevizorHeadResponce tmp = JsonConvert.DeserializeObject<GetRevizorHeadResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                DocNumb = tmp.DOC_NUM + " " + tmp.NOTE;
                TotalQuantDo = Convert.ToInt32(String.IsNullOrEmpty(tmp.QUANT_DO) ? "0" : tmp.QUANT_DO);
                TotalQuantNeed = Convert.ToInt32(String.IsNullOrEmpty(tmp.QUANT) ? "0" : tmp.QUANT);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                this.showError(ex.Message);
            }

        }

        public void fillList(string content)
        {
            try
            {
                RevizorBox.Clear();
                List<GetRevizorBoxResponce> tmp = JsonConvert.DeserializeObject<List<GetRevizorBoxResponce>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                foreach (GetRevizorBoxResponce q in tmp)
                {
                    RevizorBox.Add(new ParusRevBox { Rn = q.RN, DocRn = q.DOC_RN, BoxNum = q.BOX_NUM, ProdName = q.PROD_NAME, Capacity = q.CAPACITY, Quantity = q.QUANTITY, QuantDo = q.QUANT_DO, Comment = q.SCOMMENTS });
                    if (q.RN == box_rn)
                        SelectRevizorBox = new ParusRevBox { Rn = q.RN, DocRn = q.DOC_RN, BoxNum = q.BOX_NUM, ProdName = q.PROD_NAME, Capacity = q.CAPACITY, Quantity = q.QUANTITY, QuantDo = q.QUANT_DO, Comment = q.SCOMMENTS };
                }

                if (isMarkShow)
                    showBox();
                UserDialogs.Instance.HideLoading();

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                this.showError(ex.Message);
            }

        }

        public void addBox()
        {
            showInput("Добавление коробки", "Укажите количество бутылок");
        }

        public async void showInput(string title, string message)
        {
            PromptConfig pc = new PromptConfig();
            pc.Title = title;
            pc.Message = message;
            pc.OkText = "ОК";
            pc.InputType = InputType.Number;

            var prompt = await UserDialogs.Instance.PromptAsync(pc);
            if (prompt.Ok)
                MessagingCenter.Send<string, string>("RevizorBox", "BoxNum", prompt.Text);
        }

        public void sendBox(string content)
        {
            HttpControler.SendPostRevizor(term_name, "", doc_rn, "1", box_rn, content, "PostRevizor");
        }

        public void showBox()
        {
            if (SelectRevizorBox != null)
            {
                //Navigation.PushAsync(new RevizorMarkPage(term_name, doc_rn, SelectRevizorBox.Rn, SelectRevizorBox.BoxNum, SelectRevizorBox.ProdName, SelectRevizorBox.Quantity, SelectRevizorBox.Comment));
            }
            else
                this.showError("Не выбрана коробка");
        }

        public void showError(string error)
        {
            UserDialogs.Instance.HideLoading();
            UserDialogs.Instance.Alert(error, "Ошибка", "Ок");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string _docNumb { get; set; }
        public string DocNumb
        {
            get
            {
                return _docNumb;
            }
            set
            {
                if (_docNumb != value)
                {
                    _docNumb = value;
                    OnPropertyChanged("DocNumb");
                }
            }
        }

        private int _totalQuantDo { get; set; }
        public int TotalQuantDo
        {
            get
            {
                return _totalQuantDo;
            }
            set
            {
                if (_totalQuantDo != value)
                {
                    _totalQuantDo = value;
                    OnPropertyChanged("TotalQuantDo");
                }
            }
        }

        private int _totalQuantNeed { get; set; }
        public int TotalQuantNeed
        {
            get
            {
                return _totalQuantNeed;
            }
            set
            {
                if (_totalQuantNeed != value)
                {
                    _totalQuantNeed = value;
                    OnPropertyChanged("TotalQuantNeed");
                }
            }
        }


    }
}
