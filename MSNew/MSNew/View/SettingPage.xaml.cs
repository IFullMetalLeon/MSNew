using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSNew.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSNew.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPageVM spvm { get; set; }
        public SettingPage()
        {
            InitializeComponent();
            spvm = new SettingPageVM();
            this.BindingContext = spvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            spvm.startPage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            spvm.endPage();
        }
    }
}