using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Acr.UserDialogs;
using MSNew.Controller;
using MSNew.Model;
using MSNew.Model.Doc;
using MSNew.View.Doc;
using Newtonsoft.Json;
using Plugin.Settings;
using Xamarin.Forms;
using static MSNew.Controller.HttpJsonItem;

namespace MSNew.ViewModel.Doc
{
    public class DocQueueVM : INotifyPropertyChanged
    {

        public string Term { get; set; }
        public ObservableCollection<DocQueue> DocQueue { get; set; }
        public ICommand DocQueueMove { get; set; }
        public ICommand DocQueueDelete { get; set; }
        public string _barcode { get; set; }
        public INavigation Navigation { get; set; }

        public DocQueueVM()
        {
            DocQueue = new ObservableCollection<DocQueue>();
            SelectDocQueue = new DocQueue();
            DocQueueMove = new Command(MoveDoc);
            DocQueueDelete = new Command(DeleteDoc);

            Term = CrossSettings.Current.GetValueOrDefault("TerminalNumber", "");
        }

        public void beginPage()
        {

            CurDocRn = CrossSettings.Current.GetValueOrDefault("DocRn", "0");
            MessagingCenter.Subscribe<string, string>("MainActivity", "GetBarcode", (sender, arg) =>
            {
                work(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "PostDocQueue", (sender, arg) =>
            {
                PostDocSpecResponce(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetDocQueue", (sender, arg) =>
            {
                fillList(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "Error", (sender, arg) =>
            {
                showError(arg.Trim());
            });

            UserDialogs.Instance.ShowLoading("Обмен данными");
            HttpControler.SendGetDocQueue(Term);

        }

        public void endPage()
        {
            MessagingCenter.Unsubscribe<string, string>("MainActivity", "GetBarcode");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "PostDocQueue");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetDocQueue");
            MessagingCenter.Unsubscribe<string, string>("DocSpec", "HideKeyboard");
        }

        public void PostDocSpecResponce(string content)
        {
            try
            {
                PostDocSpecResponce responce = JsonConvert.DeserializeObject<PostDocSpecResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                if (responce.type == "0")  //Ошибка
                {
                    this.showError(responce.comment);
                }


                if (responce.type == "1" || responce.type == "2" || responce.type == "3" || responce.type == "4" || responce.type == "5" || responce.type == "14" || responce.type == "-1")
                {
                    HttpControler.SendGetDocQueue(Term);
                }
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                this.showError(ex.Message);
            }
        }

        public void fillList(string content)
        {
            DocQueue.Clear();
            if (content.Length > 2)
            {
                try
                {
                    List<GetDocQueueResponce> tmp = JsonConvert.DeserializeObject<List<GetDocQueueResponce>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    tmp.Sort(new Comparison<GetDocQueueResponce>((x, y) => Convert.ToInt32(!String.IsNullOrEmpty(x.Q_STATUS) ? x.Q_STATUS : "0").CompareTo(Convert.ToInt32(!String.IsNullOrEmpty(y.Q_STATUS) ? y.Q_STATUS : "0"))));

                    foreach (GetDocQueueResponce q in tmp)
                    {
                        bool curSelected = false;
                        if (CurDocRn != "0")
                        {
                            if (q.DOC_RN == CurDocRn)
                            {
                                SelectDocQueue = new DocQueue
                                {
                                    Rn = q.Q_RN,
                                    Status = q.Q_STATUS,
                                    DocRn = q.DOC_RN,
                                    DocNumb = q.Q_NUMB,
                                    Agent = q.AGNABBR,
                                    Quant = q.DOC_QUANT,
                                    Mesta = q.DOC_MEST
                                };
                                curSelected = true;
                            }

                        }
                        DocQueue.Add(new DocQueue
                        {
                            Rn = q.Q_RN,
                            Status = q.Q_STATUS,
                            DocRn = q.DOC_RN,
                            DocNumb = q.Q_NUMB,
                            Agent = q.AGNABBR,
                            Quant = q.DOC_QUANT,
                            Mesta = q.DOC_MEST,
                            IsSelected = curSelected
                        });
                    }
                    UserDialogs.Instance.HideLoading();

                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.HideLoading();
                    this.showError(ex.Message);
                }
            }
            else
                UserDialogs.Instance.HideLoading();
        }

        public void work(string _barcode)
        {
            UserDialogs.Instance.ShowLoading("Обмен данными");
            HttpControler.SendPostDocSpec(Term, _barcode, "0", "0", "0", "0", "PostDocQueue");
        }


        public void MoveDoc()
        {
            if (SelectDocQueue != null)
            {
                CrossSettings.Current.AddOrUpdateValue("DocRn", SelectDocQueue.DocRn);
                Navigation.PushAsync(new DocPage(SelectDocQueue.DocRn));
            }
            else
                this.showError("Не выбран документ");
        }

        public void DeleteDoc()
        {
            if (SelectDocQueue != null)
            {
                HttpControler.SendPostDocSpec(Term, SelectDocQueue.DocRn, SelectDocQueue.DocRn, "0", "4", "0", "PostDocQueue");
            }
            else
                this.showError("Не выбран документ");
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

        private string _curDocRn { get; set; }
        public string CurDocRn
        {
            get
            {
                return _curDocRn;
            }
            set
            {
                if (_curDocRn != value)
                {
                    _curDocRn = value;
                    OnPropertyChanged("CurDocRn");
                }
            }
        }

        private string _curDocNumb { get; set; }
        public string CurDocNumb
        {
            get
            {
                return _curDocNumb;
            }
            set
            {
                if (_curDocNumb != value)
                {
                    _curDocNumb = value;
                    OnPropertyChanged("CurDocNumb");
                }
            }
        }

        private DocQueue _selecttDocQueue { get; set; }
        public DocQueue SelectDocQueue
        {
            get
            {
                return _selecttDocQueue;
            }
            set
            {
                if (_selecttDocQueue != value)
                {
                    if (_selecttDocQueue != null)
                        _selecttDocQueue.IsSelected = false;


                    value.IsSelected = true;
                    _selecttDocQueue = value;
                    CurDocNumb = _selecttDocQueue.DocNumb;
                    for (int i = 0; i < DocQueue.Count; i++)
                    {
                        if (DocQueue[i].DocNumb != CurDocNumb)
                            DocQueue[i].IsSelected = false;
                    }
                    OnPropertyChanged("SelectDocQueue");
                }
            }
        }


    }
}
