using System.Collections.Generic;
using System.ComponentModel;
using Acr.UserDialogs;
using MSNew.Controller;
using MSNew.Model.Doc;
using Xamarin.Forms;
using Newtonsoft.Json;
using static MSNew.Controller.HttpJsonItem;
using System.Collections.ObjectModel;

namespace MSNew.ViewModel.Doc
{
    public class MarkPageVM : INotifyPropertyChanged
    {
        public ObservableCollection<DocMark> MarkSpec { get; set; }
        public string box_rn { get; set; }
        public string del_sign { get; set; }

        public MarkPageVM(string _box_rn, string _box_name, string _del_sign)
        {
            MarkSpec = new ObservableCollection<DocMark>();

            box_rn = _box_rn;
            del_sign = _del_sign;
        }

        public void startPage()
        {                     
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetMarkSpec", (sender, arg) =>
            {
                fillList(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "Error", (sender, arg) =>
            {
                showError(arg.Trim());
            });
            UserDialogs.Instance.ShowLoading("Загрузка данных");
            HttpControler.SendGetMarkSpec(box_rn);
        }

        public void endPage()
        {
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetMarkSpec");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "Error");
        }

        public void fillList(string content)
        {
            MarkSpec.Clear();

            List<GetMarkSpecResponce> tmp = JsonConvert.DeserializeObject<List<GetMarkSpecResponce>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            foreach (GetMarkSpecResponce q in tmp)
            {
                MarkSpec.Add(new DocMark { BoxRn = q.BOX_RN, MarkRn = q.MARK_RN, Numb = q.NUMB, Pref = q.PREF });
            }
            UserDialogs.Instance.HideLoading();
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

        private string _boxNum { get; set; }
        public string BoxNum
        {
            get
            {
                return _boxNum;
            }
            set
            {
                if (_boxNum != value)
                {
                    _boxNum = value;
                    OnPropertyChanged("BoxNum");
                }
            }
        }

    }
}
