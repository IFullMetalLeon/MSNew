using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Info
{
    public class InfoHead : INotifyPropertyChanged
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

        private string _type { get; set; }
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private string _docType { get; set; }
        public string DocType
        {
            get
            {
                return _docType;
            }
            set
            {
                if (_docType != value)
                {
                    _docType = value;
                    OnPropertyChanged("DocType");
                }
            }
        }

        private string _doc { get; set; }
        public string Doc
        {
            get
            {
                return _doc;
            }
            set
            {
                if (_doc != value)
                {
                    _doc = value;
                    OnPropertyChanged("Doc");
                }
            }
        }

        private string _pallet { get; set; }
        public string Pallet
        {
            get
            {
                return _pallet;
            }
            set
            {
                if (_pallet != value)
                {
                    _pallet = value;
                    OnPropertyChanged("Pallet");
                }
            }
        }

        private string _nommodif { get; set; }
        public string Nommodif
        {
            get
            {
                return _nommodif;
            }
            set
            {
                if (_nommodif != value)
                {
                    _nommodif = value;
                    OnPropertyChanged("Nommodif");
                }
            }
        }
        
        private string _comment { get; set; }
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
    }
}
