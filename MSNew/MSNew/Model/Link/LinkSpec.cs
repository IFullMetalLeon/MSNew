using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Link
{
    public class LinkSpec : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
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
        
        private string _shortBarcode { get; set; }
        public string ShortBarcode
        {
            get
            {
                return _shortBarcode;
            }
            set
            {
                if (_shortBarcode != value)
                {
                    _shortBarcode = value;
                    OnPropertyChanged("ShortBarcode");
                }
            }
        }
        
        private string _isLinked { get; set; }
        public string IsLinked
        {
            get
            {
                return _isLinked;
            }
            set
            {
                if (_isLinked != value)
                {
                    _isLinked = value;
                    OnPropertyChanged("IsLinked");
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
