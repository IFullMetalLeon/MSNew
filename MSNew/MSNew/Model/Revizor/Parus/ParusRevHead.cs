using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Revizor.Parus
{
    public class ParusRevHead : INotifyPropertyChanged
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
        
        private string _docNum { get; set; }
        public string DocNum
        {
            get
            {
                return _docNum;
            }
            set
            {
                if (_docNum != value)
                {
                    _docNum = value;
                    OnPropertyChanged("DocNum");
                }
            }
        }

        private string _note { get; set; }
        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                if (_note != value)
                {
                    _note = value;
                    OnPropertyChanged("Note");
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
                }
            }
        }
    }
}
