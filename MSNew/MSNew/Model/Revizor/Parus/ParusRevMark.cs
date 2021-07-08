using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Revizor.Parus
{
    public class ParusRevMark : INotifyPropertyChanged
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
        
        private string _prn { get; set; }
        public string Prn
        {
            get
            {
                return _prn;
            }
            set
            {
                if (_prn != value)
                {
                    OnPropertyChanged("Prn");
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

        private string _fullName { get; set; }
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged("FullName");
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
