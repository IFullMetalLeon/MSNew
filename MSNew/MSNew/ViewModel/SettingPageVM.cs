using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Plugin.Settings;

namespace MSNew.ViewModel
{
    public class SettingPageVM : INotifyPropertyChanged
    {

        public SettingPageVM()
        {

        }

        public void startPage()
        {
            TerminalNumber = CrossSettings.Current.GetValueOrDefault("TerminalNumber", "");
            IsRevizor = CrossSettings.Current.GetValueOrDefault("IsRevizor", "False");
            BarcodeEvent = CrossSettings.Current.GetValueOrDefault("BarcodeEvent", "");
            BarcodeString = CrossSettings.Current.GetValueOrDefault("BarcodeString", "");
        }

        public void endPage()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string _terminalNumber { get; set; }
        public string TerminalNumber
        {
            get
            {
                return _terminalNumber;
            }
            set
            {
                if (_terminalNumber != value)
                {
                    _terminalNumber = value;
                    CrossSettings.Current.AddOrUpdateValue("TerminalNumber", _terminalNumber);
                    OnPropertyChanged("TerminalNumber");
                }
            }
        }

        private string _isRevizor { get; set;}
        public string IsRevizor
        {
            get
            {
                return _isRevizor;
            }
            set
            {
                if (_isRevizor != value)
                {
                    _isRevizor = value;
                    CrossSettings.Current.AddOrUpdateValue("IsRevizor", _isRevizor);
                    OnPropertyChanged("IsRevizor");
                }
            }
        }

        private string _barcodeEvent { get; set; }
        public string BarcodeEvent
        {
            get
            {
                return _barcodeEvent;
            }
            set
            {
                if (_barcodeEvent != value)
                {
                    _barcodeEvent = value;
                    CrossSettings.Current.AddOrUpdateValue("BarcodeEvent", _barcodeEvent);
                    OnPropertyChanged("BarcodeEvent");
                }
            }
        }

        private string _barcodeString { get; set; }
        public string BarcodeString
        {
            get
            {
                return _barcodeString;
            }
            set
            {
                if (_barcodeString != value)
                {
                    _barcodeString = value;
                    CrossSettings.Current.AddOrUpdateValue("BarcodeString", _barcodeString);
                    OnPropertyChanged("BarcodeString");
                }
            }
        }
    }
}
