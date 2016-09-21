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
        public static string InternalID { get; set; }
        public static string OTPKey { get; set; }
        public static bool IsUserEnrolled { get; set; }
        public static string UserEmail { get; set; }

        public App()
        {
            IsUserLoggedIn = false;
            UserEmail = Helpers.Settings.UserEmail;

            if(!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new Pages.TopPage());
                MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#ea002c"));
            } else
            {
                MainPage = new NavigationPage(new FrontPage());
                MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Green);
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            UserEmail = Helpers.Settings.UserEmail;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            //Helpers.Settings.UserEmail = UserEmail;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
