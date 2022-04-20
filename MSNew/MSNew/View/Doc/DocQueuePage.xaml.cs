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
    public partial class DocQueuePage : ContentPage
    {
        public DocQueueVM dqvm { get; set; }
        public DocQueuePage()
        {
            InitializeComponent();
            dqvm = new DocQueueVM() { Navigation = this.Navigation};
            this.BindingContext = dqvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dqvm.beginPage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            dqvm.endPage();
        }

    }
}