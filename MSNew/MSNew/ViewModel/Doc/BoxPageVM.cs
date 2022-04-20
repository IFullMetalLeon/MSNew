using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Acr.UserDialogs;
using MSNew.Controller;
using MSNew.View.Doc;
using MSNew.Model.Doc;
using Plugin.Settings;
using Xamarin.Forms;
using Newtonsoft.Json;
using static MSNew.Controller.HttpJsonItem;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MSNew.ViewModel.Doc
{
    public class BoxPageVM : INotifyPropertyChanged
    {
        public ObservableCollection<DocBox> BoxSpec { get; set; }

        public INavigation Navigation { get; set; }
        public ICommand DeleteBox { get; set; }
        public ICommand ShowMark { get; set; }

        public string term_name { get; set; }
        public string nm_name { get; set; }
        public string del_sign { get; set; }
        public string doc_rn { get; set; }
        public string nm_rn { get; set; }

        public BoxPageVM(string _doc_rn, string _nm_rn, string _nm_name, string _del_sign)
        {
            BoxSpec = new ObservableCollection<DocBox>();
            SelectedBox = new DocBox();
            DeleteBox = new Command(sendDeleteBox);
            ShowMark = new Command(showMarks);


            term_name = CrossSettings.Current.GetValueOrDefault("TerminalNumber", "");
            doc_rn = _doc_rn;
            nm_rn = _nm_rn;
            NmName = _nm_name;
            del_sign = _del_sign;
        }

        public void startPage()
        {
            UserDialogs.Instance.ShowLoading("Получение данных");
            HttpControler.SendGetBoxSpec(doc_rn, nm_rn);
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetBoxSpec", (sender, arg) =>
            {
                fillList(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "PostBoxSpec", (sender, arg) =>
            {
                PostBoxSpecResponce(arg.Trim());
            });

            MessagingCenter.Subscribe<string, string>("HttpControler", "Error", (sender, arg) =>
            {
                showError(arg.Trim());
            });
            BoxNumber = "0";
        }

        public void endPage()
        {
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetBoxSpec");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "PostBoxSpec");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "Error");
        }

        public void fillList(string content)
        {
            BoxSpec.Clear();

            List<GetBoxSpecResponce> tmp = JsonConvert.DeserializeObject<List<GetBoxSpecResponce>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            foreach (GetBoxSpecResponce q in tmp)
            {
                BoxSpec.Add(new DocBox { TcRn = q.TC_RN, NmRn = q.NM_RN, BoxNum = q.BOX_NUM, BoxRn = q.BOX_RN, Quant = q.QUANT });
            }
            BoxNumber = BoxSpec.Count.ToString();
            UserDialogs.Instance.HideLoading();
        }

        public void PostBoxSpecResponce(string content)
        {
            try
            {
                PostDocSpecResponce responce = JsonConvert.DeserializeObject<PostDocSpecResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                if (responce.type == "0")  //Ошибка
                {
                    UserDialogs.Instance.Alert(responce.comment, "Ошибка", "Ок");
                }
                else
                {
                    UserDialogs.Instance.Alert("Коробка удалена успешно", "Сообщение", "Ок");
                }
            }
            catch (Exception ex)
            {
                this.showError(ex.Message);
            }
        }

        public void showError(string error)
        {
            UserDialogs.Instance.HideLoading();
            UserDialogs.Instance.Alert(error, "Ошибка","Ок");
        }

        public async void sendDeleteBox()
        {
            if (SelectedBox != null)
            {
                if (del_sign == "-1")
                {
                    UserDialogs.Instance.Alert("Документ доступен только для ПРОСМОТРА", "Внимание");
                }
                else
                {
                    var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                    {
                        Message = "Удалить коробку",
                        OkText = "Удалить",
                        CancelText = "Отмена"
                    });
                    if (result)
                        HttpControler.SendPostDocSpec(term_name, "0", doc_rn, SelectedBox.BoxRn, "1", "0", "PostBoxSpec");
                }
            }
            else
                this.showError("Не выбрана коробка");
        }

        public void showMarks()
        {
            if (SelectedBox != null)
            {
                Navigation.PushAsync(new MarkPage(SelectedBox.BoxRn, SelectedBox.BoxNum, del_sign));
            }
            else
                this.showError("Не выбрана коробка");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string _boxNumber { get; set; }
        public string BoxNumber
        {
            get
            {
                return _boxNumber;
            }
            set
            {
                if (_boxNumber != value)
                {
                    _boxNumber = value;
                    OnPropertyChanged("BoxNumber");
                }
            }
        }

        private string _nmName { get; set; }
        public string NmName
        {
            get
            {
                return _nmName;
            }
            set
            {
                if (_nmName != value)
                {
                    _nmName = value;
                    OnPropertyChanged("NmName");
                }
            }
        }

        private DocBox _selectedBox { get; set; }
        public DocBox SelectedBox
        {
            get
            {
                return _selectedBox;
            }
            set
            {
                if (_selectedBox != value)
                {
                    if (_selectedBox != null) _selectedBox.IsSelected = false;

                    value.IsSelected = true;

                    _selectedBox = value;
                    OnPropertyChanged("SelectedBox");
                }
            }
        }
    }
}
