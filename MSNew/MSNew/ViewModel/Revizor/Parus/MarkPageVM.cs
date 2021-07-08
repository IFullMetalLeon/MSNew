using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSNew.ViewModel.Revizor.Parus
{
    public class MarkPageVM : INotifyPropertyChanged
    {
        public MarkPageVM()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
