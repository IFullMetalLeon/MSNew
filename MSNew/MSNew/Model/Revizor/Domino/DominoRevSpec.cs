using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Revizor.Domino
{
    public class DominoRevSpec : INotifyPropertyChanged
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
                    _prn = value;
                    OnPropertyChanged("Prn");
                }
            }
        }
        
        private string _barcode { get; set; }
        public string Barcode
        {
            get
            {
                return _barcode;
            }
            set
            {
                if (_barcode != value)
                {
                    _barcode = value;
                    OnPropertyChanged("Barcode");
                }
            }
        }
        
        private string _nomenName { get; set; }
        public string NomenName
        {
            get
            {
                return _nomenName;
            }
            set
            {
                if (_nomenName != value)
                {
                    _nomenName = value;
                    OnPropertyChanged("NomenName");
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
    }
}
