using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFrontEnd.Pages
{
    public partial class MobileOTP : ContentPage
    {
        static ViewModels.MobileOTPViewModel mm;
        public MobileOTP()
        {
            InitializeComponent();

            this.lblLoginID.Text = Helpers.Settings.UserEmail;

            mm = new ViewModels.MobileOTPViewModel();
            this.BindingContext = mm;

            MessagingCenter.Subscribe<ViewModels.MobileOTPViewModel>(this, "ProgressFire", (args) =>
            {
                this.prgBar.Progress = 1;
                int ss = DateTime.Now.Second;
                int mod = 30 - (ss % 30);
                this.prgBar.ProgressTo(0.0, Convert.ToUInt32(mod * 1000), Easing.Linear);
            });
        }
        

        async void OnBtnLogoutClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            NavigationPage page = new NavigationPage(new Pages.TopPage());
            App.Current.MainPage = page;
            page.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#ea002c"));
            await Navigation.PopToRootAsync();
        }
    }
}
