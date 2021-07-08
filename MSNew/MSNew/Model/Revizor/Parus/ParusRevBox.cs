using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MSNew.Model.Revizor.Parus
{
    public class ParusRevBox : INotifyPropertyChanged
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

        private string _boxNum { get; set; }
        public string BoxNum
        {
            get
            {
                return _boxNum;
            }
            set
            {
                if (_boxNum != value)
                {
                    _boxNum = value;
                    OnPropertyChanged("BoxNum");
                }
            }
        }
        private string _prodName { get; set; }
        public string ProdName
        {
            get
            {
                return _prodName;
            }
            set
            {
                if (_prodName != value)
                {
                    _prodName = value;
                    OnPropertyChanged("ProdName");
                }
            }
        }
        
        private string _capacity { get; set; }
        public string Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                if (_capacity != value)
                {
                    _capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }
        
        private string _quantity { get; set; }
        public string Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
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
