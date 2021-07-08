using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSNew.ViewModel.Revizor.Parus;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSNew.View.Revizor.Parus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocPage : ContentPage
    {
        public DocPageVM dpvm { get; set; }
        public DocPage()
        {
            InitializeComponent();
            dpvm = new DocPageVM();
            this.BindingContext = dpvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dpvm.startPage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            dpvm.endPage();
        }
    }
}