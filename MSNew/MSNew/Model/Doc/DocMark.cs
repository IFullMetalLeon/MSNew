using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Doc
{
    public class DocMark : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string _boxRn { get; set;}
        public string BoxRn
        {
            get
            {
                return _boxRn;
            }
            set
            {
                if (_boxRn != value)
                {
                    _boxRn = value;
                    OnPropertyChanged("BoxRn");
                }
            }
        }

        private string _markRn { get; set; }
        public string MarkRn
        {
            get
            {
                return _markRn;
            }
            set
            {
                if (_markRn != value)
                {
                    _markRn = value;
                    OnPropertyChanged("MarkRn");
                }
            }
        }

        private string _pref { get; set; }
        public string Pref
        {
            get
            {
                return _pref;
            }
            set
            {
                if (_pref != value)
                {
                    _pref = value;
                    OnPropertyChanged("Pref");
                }
            }
        }

        private string _numb { get; set; }
        public string Numb
        {
            get
            {
                return _numb;
            }
            set
            {
                if (_numb != value)
                {
                    _numb = value;
                    OnPropertyChanged("Numb");
                }
            }
        }

    }
}
