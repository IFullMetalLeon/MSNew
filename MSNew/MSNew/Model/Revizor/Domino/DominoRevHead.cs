using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Revizor.Domino
{
    public class DominoRevHead : INotifyPropertyChanged
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
        
        private string _allQuant { get; set; }
        public string AllQuant
        {
            get
            {
                return _allQuant;
            }
            set
            {
                if (_allQuant != value)
                {
                    _allQuant = value;
                    OnPropertyChanged("AllQuant");
                }
            }
        }        
    }
}
