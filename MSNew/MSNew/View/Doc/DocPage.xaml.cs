using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSNew.ViewModel.Doc;
using MSNew.Model.Doc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSNew.View.Doc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocPage : ContentPage
    {
        public DocPageVM dpvm { get; set; }
        public DocPage()
        {
            InitializeComponent();
            dpvm = new DocPageVM() {Navigation = this.Navigation };
            this.BindingContext = dpvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dpvm.startPage();
            MessagingCenter.Subscribe<string, DocSpec>("DocSpecPageXaml", "Scroll", (sender, arg) => {
                myScroll(arg);
            });           
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            dpvm.endPage();
            MessagingCenter.Unsubscribe<string, object>("DocSpecPageXaml", "Scroll");
        }

        public void myScroll(DocSpec item)
        {
            foreach (DocSpec q in this.docSpecList.ItemsSource)
            {
                if (q.Rn == item.Rn)
                {
                    this.docSpecList.SelectedItem = q;
                    this.docSpecList.ScrollTo(q, ScrollToPosition.MakeVisible, false);
                }
            }
        }

    }
}