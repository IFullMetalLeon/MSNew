using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Doc
{
    public class DocBox : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string _tcRn { get; set; }
        public string TcRn
        {
            get
            {
                return _tcRn;
            }
            set
            {
                if (_tcRn != value)
                {
                    _tcRn = value;
                    OnPropertyChanged("TcRn");
                }
            }
        }

        private string _nmRn { get; set; }
        public string NmRn
        {
            get
            {
                return _nmRn;
            }
            set
            {
                if (_nmRn != value)
                {
                    _nmRn = value;
                    OnPropertyChanged("NmRn");
                }
            }
        }

        private string _boxRn { get; set; }
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

        private string _boxNum { get; set;}
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

        private string _quant { get; set; }
        public string Quant
        {
            get
            {
                return _quant;
            }
            set
            {
                if (_quant != value)
                {
                    _quant = value;
                    OnPropertyChanged("Quant");
                }
            }
        }

        private bool _isSelected { get; set; }
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }
    }
}
