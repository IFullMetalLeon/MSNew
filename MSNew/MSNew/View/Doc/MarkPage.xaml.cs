using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSNew.ViewModel.Doc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSNew.View.Doc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkPage : ContentPage
    {
        public MarkPageVM mpvm { get; set; }
        public MarkPage(string _box_rn, string _box_name, string _del_sign)
        {
            InitializeComponent();
            mpvm = new MarkPageVM(_box_rn, _box_name, _del_sign);
            this.BindingContext = mpvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mpvm.startPage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            mpvm.endPage();
        }
    }
}