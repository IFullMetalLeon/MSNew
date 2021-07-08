using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSNew.ViewModel.Link;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSNew.View.Link
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkPage : ContentPage
    {
        public LinkPageVM lpvm { get; set; }
        public LinkPage()
        {
            InitializeComponent();
            lpvm = new LinkPageVM();
            this.BindingContext = lpvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lpvm.startPage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            lpvm.endPage();
        }
    }
}