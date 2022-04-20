using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSNew.View;
using MSNew.View.Doc;
using MSNew.View.Info;
using MSNew.View.Link;
using MSNew.View.Revizor;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSNew.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
            //Detail = new NavigationPage(new DocPage());
            Detail = new NavigationPage(new DocQueuePage()) { BarBackgroundColor = Color.FromHex("#FFFFFF"), BarTextColor = Color.FromHex("#000000") };
        }

        private void Setting_Clicked(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new SettingPage());
        }

        private void Doc_Clicked(object sender, EventArgs e)
        {
            IsPresented = false;
            //Detail = new NavigationPage(new DocPage());
            Detail = new NavigationPage(new DocQueuePage());
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new InfoPage());
        }

        private void Link_Clicked(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new LinkPage());
        }

        private void ParusRevizor_Clicked(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new Revizor.Parus.DocPage());
        }

        private void DominoRevizor_Clicked(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new Revizor.Domino.DocPage());
        }
    }
}