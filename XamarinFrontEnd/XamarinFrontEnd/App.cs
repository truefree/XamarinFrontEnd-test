using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinFrontEnd
{
    public class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        

        public App()
        {
            IsUserLoggedIn = false;

            // initializing values except email address
            Helpers.Settings.InternalID = string.Empty;
            Helpers.Settings.IsUserEnrolled = false;
            Helpers.Settings.OTPID = string.Empty;

            if(!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new Pages.TopPage());
                MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#ea002c"));
            } else
            {
                MainPage = new NavigationPage(new Pages.TopPage());
                MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Green);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
