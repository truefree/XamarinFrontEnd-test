using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFrontEnd.Pages
{
    public partial class MFAChallenge : ContentPage
    {
        public MFAChallenge()
        {
            InitializeComponent();
        }

        public async void OnBtnSubmitClicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await this.FadeTo(0.5);

            string MFAToken = this.txtID.Text;
            int i = 0;
            if(string.IsNullOrEmpty(MFAToken)
                || int.TryParse(MFAToken, out i) == false)
            {
                await DisplayAlert("Token 오류", "SMS로 전달 받은 6자리 숫자를 입력하세요.", "OK");
                await this.FadeTo(1);
                this.IsEnabled = true;
            }
            else
            {
                try
                {

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    await this.FadeTo(1);
                    this.IsEnabled = true;
                }
            }
        }
    }
}
