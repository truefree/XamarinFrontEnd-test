using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFrontEnd
{
    public partial class FrontPage : ContentPage
    {
        public FrontPage()
        {
            InitializeComponent();
        }

        void OnLoginClicked(object sender, EventArgs evt)
        {
            //Navigation.PushAsync(new Page1());
        }

        void OnRegClicked(object sender, EventArgs evt)
        {
            Navigation.PushAsync(new Registration());
        }
    }
}
