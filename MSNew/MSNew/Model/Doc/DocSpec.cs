using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Doc
{
    public class DocSpec : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string _rn { get; set; }
        public string Rn
        {
            get
            {
                return _rn;
            }
            set
            {
                if (_rn != value)
                {
                    _rn = value;
                    OnPropertyChanged("Rn");
                }
            }
        }

        private string _name { get; set; }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _quantNeed { get; set; }
        public string QuantNeed
        {
            get
            {
                return _quantNeed;
            }
            set
            {
                if (_quantNeed != value)
                {
                    _quantNeed = value;
                    OnPropertyChanged("QuantNeed");
                }
            }
        }

        private string _quantDo { get; set; }
        public string QuantDo
        {
            get
            {
                return _quantDo;
            }
            set
            {
                if (_quantDo != value)
                {
                    _quantDo = value;
                    OnPropertyChanged("QuantDo");
                    OnPropertyChanged("QuantDoTC");
                }
            }
        }

        public string QuantDoTC
        {
            get
            {
                if (QuantNeed != QuantDo)
                    return "Red";
                else
                    return "Green";
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
