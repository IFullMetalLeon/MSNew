using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSNew.ViewModel.Info;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSNew.View.Info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        public InfoPageVM ipvm { get; set; }
        public InfoPage()
        {
            InitializeComponent();
            ipvm = new InfoPageVM();
            this.BindingContext = ipvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ipvm.startPage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ipvm.endPage();
        }
    }
}