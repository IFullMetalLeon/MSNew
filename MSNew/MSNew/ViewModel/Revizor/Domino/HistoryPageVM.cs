using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSNew.ViewModel.Revizor.Domino
{
    public class HistoryPageVM : INotifyPropertyChanged
    {
        

        public HistoryPageVM()
        {

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
    }
}
