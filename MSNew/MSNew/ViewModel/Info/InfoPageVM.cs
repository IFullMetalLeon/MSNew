using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Acr.UserDialogs;
using MSNew.Controller;
using MSNew.Model.Info;
using MSNew.View.Doc;
using Plugin.Settings;
using Xamarin.Forms;
using Newtonsoft.Json;
using static MSNew.Controller.HttpJsonItem;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace MSNew.ViewModel.Info
{
    public class InfoPageVM : INotifyPropertyChanged
    {
        public ObservableCollection<InfoSpec> InfoSpec { get; set; }
        public string term_name { get; set; }
        public string doc_rn { get; set; }

        public InfoPageVM()
        {
            InfoSpec = new ObservableCollection<InfoSpec>();
            term_name = CrossSettings.Current.GetValueOrDefault("TerminalNumber", "");
        }

        public void startPage()
        {
            MessagingCenter.Subscribe<string, string>("MainActivity", "GetBarcode", (sender, arg) =>
            {
                work(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "Info", (sender, arg) =>
            {
                PostInfoResponce(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetInfoHead", (sender, arg) =>
            {
                GetInfoHeadResponce(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetInfoSpec", (sender, arg) =>
            {
                fillList(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "Error", (sender, arg) =>
            {
                showError(arg.Trim());
            });
        }

        public void endPage()
        {
            MessagingCenter.Unsubscribe<string, string>("MainActivity", "GetBarcode");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "Info");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetInfoHead");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetInfoSpec");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "Error");
            
        }

        public void work(string _barcode)
        {
            InfoHead = "";
            UserDialogs.Instance.ShowLoading("Обмен данными");
            HttpControler.SendPostInfo(_barcode, "Info");
        }

        public void PostInfoResponce(string content)
        {
            try
            {
                PostInfoResponce tmp = JsonConvert.DeserializeObject<PostInfoResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                doc_rn = tmp.doc_rn;
                HttpControler.SendGetInfoHead(doc_rn);
                HttpControler.SendGetInfoSpec(doc_rn);
            }
            catch (Exception ex)
            {
                this.showError(ex.Message);
            }
        }

        public void GetInfoHeadResponce(string content)
        {
            try
            {
                GetInfoHeadResponce tmp = JsonConvert.DeserializeObject<GetInfoHeadResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                InfoHead = tmp.STYPE + "\n" + tmp.SDOC_TYPE + "  " + tmp.SDOC;
                if (!String.IsNullOrEmpty(tmp.SPALLET))
                {
                    InfoHead += "\n" + tmp.SPALLET;
                }
                if (!String.IsNullOrEmpty(tmp.SCOMMENT))
                {
                    InfoHead += "\n" + tmp.SCOMMENT;
                }
                InfoHead += "\n" + tmp.SNOMMODIF;
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
                List<GetInfoSpecResponce> tmp = JsonConvert.DeserializeObject<List<GetInfoSpecResponce>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                InfoSpec.Clear();
                foreach (GetInfoSpecResponce q in tmp)
                {
                    InfoSpec.Add(new InfoSpec { Rn = q.RN, Prn = q.PRN, Barcode = q.BARCODE, Comment = q.SCOMMENT });
                }
                InfoHead += " КОЛ-ВО " + InfoSpec.Count.ToString();
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                this.showError(ex.Message);
            }

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

        private string _infoHead { get; set; }
        public string InfoHead
        {
            get
            {
                return _infoHead;
            }
            set
            {
                if (_infoHead != value)
                {
                    _infoHead = value;
                    OnPropertyChanged("InfoHead");
                }
            }
        }
    }
}
