using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFrontEnd.Pages
{
    public partial class TopPage : ContentPage
    {
        public TopPage()
        {
            InitializeComponent();
            if (App.IsUserLoggedIn)
            {
                
            }
        }
    }
}
