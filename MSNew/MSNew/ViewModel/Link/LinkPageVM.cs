using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Acr.UserDialogs;
using MSNew.Controller;
using MSNew.Model.Link;
using Plugin.Settings;
using Xamarin.Forms;
using Newtonsoft.Json;
using static MSNew.Controller.HttpJsonItem;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MSNew.ViewModel.Link
{
    public class LinkPageVM : INotifyPropertyChanged
    {
        public ObservableCollection<LinkSpec> LinkSpec { get; set; }

        public string term_name;
        public string in_del_sign;
        public string box_rn;
        public ICommand LinkDelete { get; set; }
        public ICommand LinkSend { get; set; }
        public LinkPageVM()
        {
            LinkSpec = new ObservableCollection<LinkSpec>();
            LinkDelete = new Command(linkDelete);
            LinkSend = new Command(linkSend);

            term_name = CrossSettings.Current.GetValueOrDefault("TerminalNumber", "");
            in_del_sign = "0";
            box_rn = "0";
        }

        public void startPage()
        {
            MessagingCenter.Subscribe<string, string>("MainActivity", "GetBarcode", (sender, arg) =>
            {
                work(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "PostLinkSpec", (sender, arg) =>
            {
                PostLinkSpecResponce(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "PostLinkSpecAdditional", (sender, arg) =>
            {
                PostLinkSpecAddResponce(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetLinkSpec", (sender, arg) =>
            {
                fillList(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "Error", (sender, arg) =>
            {
                showError(arg.Trim());
            });
            CurrentMarkCount = "0";
        }

        public void endPage()
        {
            MessagingCenter.Unsubscribe<string, string>("MainActivity", "GetBarcode");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "PostLinkSpec");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "PostLinkSpecAdditional");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetLinkSpec");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "Error");
        }

        public void work(string _barcode)
        {
            UserDialogs.Instance.ShowLoading();
            in_del_sign = "0";
            HttpControler.SendPostLink(term_name, _barcode, box_rn, in_del_sign, "PostLinkSpec");
        }

        public void PostLinkSpecResponce(string content)
        {
            PostLinkResponce tmp = JsonConvert.DeserializeObject<PostLinkResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            if (tmp.box_rn != "0")
            {
                box_rn = tmp.box_rn;
                LinkHead = tmp.comment;
                HttpControler.SendGetLinkSpec(box_rn);
            }
            else
            {
                if (!String.IsNullOrEmpty(tmp.comment))
                    this.showError(tmp.comment);
                HttpControler.SendGetLinkSpec(box_rn);
            }
        }

        public void PostLinkSpecAddResponce(string content)
        {
            try
            {
                PostLinkResponce tmp = JsonConvert.DeserializeObject<PostLinkResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                UserDialogs.Instance.Alert(tmp.comment, "Сообщение", "Ок");
                HttpControler.SendGetLinkSpec(box_rn);
            }
            catch (Exception ex)
            {
                this.showError(ex.Message);
            }
        }

        public void fillList(string content)
        {
            try
            {
                LinkSpec.Clear();
                List<GetLinkSpecResponce> tmp = JsonConvert.DeserializeObject<List<GetLinkSpecResponce>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                foreach (GetLinkSpecResponce q in tmp)
                {
                    LinkSpec.Add(new LinkSpec { Prn = q.PRN, ShortBarcode = q.SHORT_BARCODE, IsLinked = q.IS_LINKED });
                }
                CurrentMarkCount = LinkSpec.Count.ToString();
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                this.showError(ex.Message);
            }

        }

        public void linkDelete()
        {
            if (box_rn != "0")
            {
                if (SelectLinkSpec != null)
                {
                    UserDialogs.Instance.ShowLoading();
                    HttpControler.SendPostLink(term_name, SelectLinkSpec.ShortBarcode, box_rn, "1", "PostLinkSpecAdditional");
                }
                else
                    this.showError("Не выбрана марка");
            }
            else
                this.showError("Не указана коробка");
        }

        public void linkSend()
        {
            if (box_rn != "0")
            {
                UserDialogs.Instance.ShowLoading();
                HttpControler.SendPostLink(term_name, "0", box_rn, "2", "PostLinkSpecAdditional");
            }
            else
                this.showError("Не указана коробка");
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

        private string _linkHead { get; set; }
        public string LinkHead
        {
            get
            {
                return _linkHead;
            }
            set
            {
                if (_linkHead != value)
                {
                    _linkHead = value;
                    OnPropertyChanged("LinkHead");
                }
            }
        }

        private string _currentMarkCount { get; set; }
        public string CurrentMarkCount
        {
            get
            {
                return _currentMarkCount;
            }
            set
            {
                if (_currentMarkCount != value)
                {
                    _currentMarkCount = value;
                    OnPropertyChanged("CurrentMarkCount");
                }
            }
        }

        private LinkSpec _selectLinkSpec { get; set; }
        public LinkSpec SelectLinkSpec
        {
            get
            {
                return _selectLinkSpec;
            }
            set
            {
                if (_selectLinkSpec != value)
                {
                    if (_selectLinkSpec != null) _selectLinkSpec.IsSelected = false;

                    value.IsSelected = true;

                    _selectLinkSpec = value;
                    OnPropertyChanged("SelectLinkSpec");
                }
            }
        }
    }
}
