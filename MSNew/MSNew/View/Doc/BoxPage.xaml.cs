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
    public partial class BoxPage : ContentPage
    {
        public BoxPageVM bpvm { get; set; }
        public BoxPage(string _doc_rn, string _nm_rn, string _nm_name, string _del_sign)
        {
            InitializeComponent();
            bpvm = new BoxPageVM(_doc_rn, _nm_rn, _nm_name, _del_sign) {Navigation = this.Navigation };
            this.BindingContext = bpvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            bpvm.startPage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            bpvm.endPage();
        }
    }
}