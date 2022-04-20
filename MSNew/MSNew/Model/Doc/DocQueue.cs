using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSNew.Model.Doc
{
    public class DocQueue : INotifyPropertyChanged
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

        private string _status { get; set; }
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private string _docRn { get; set; }
        public string DocRn
        {
            get
            {
                return _docRn;
            }
            set
            {
                if (_docRn != value)
                {
                    _docRn = value;
                    OnPropertyChanged("DocRn");
                }
            }
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

        private string _agent { get; set; }
        public string Agent
        {
            get
            {
                return _agent;
            }
            set
            {
                if (_agent != value)
                {
                    _agent = value;
                    OnPropertyChanged("Agent");
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
        private string _mesta { get; set; }
        public string Mesta
        {
            get
            {
                return _mesta;
            }
            set
            {
                if (_mesta != value)
                {
                    _mesta = value;
                    OnPropertyChanged("Mesta");
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
