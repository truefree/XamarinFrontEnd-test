using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.ComponentModel;

namespace XamarinFrontEnd.Pages
{
    public partial class MFAChallenge : ContentPage
    {
        static ViewModels.MFAViewModel vm;
        public MFAChallenge()
        {
            InitializeComponent();
            vm = new ViewModels.MFAViewModel();
            this.BindingContext = vm;
            this.sendRequest();
            StartTimer();

            this.txtID.TextChanged += (sender, args) =>
            {
                string _text = txtID.Text;      //Get Current Text
                if(_text.Length > 5)       //If it is more than your character restriction
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtID.Text = _text;        //Set the Old value
                    txtID.Unfocus();
                }
            };
        }

        public void StartTimer()
        {
            vm.RemainSeconds = 60;
            this.btnRequest.IsEnabled = false;
            Xamarin.Forms.Device.StartTimer(
            TimeSpan.FromSeconds(1),
            () =>
            {

                vm.RemainSeconds--;
                if(vm.RemainSeconds <= 0)
                {
                    this.btnRequest.IsEnabled = true;
                    return false;
                }
                else
                {
                    return true;
                }
            }

            );
        }
        
        public void OnBtnRequestClicked(object sender, EventArgs e)
        {
            this.btnRequest.IsEnabled = false;
            this.sendRequest();
            StartTimer();
        }

        public async void sendRequest()
        {
            HttpClientHandler c = new HttpClientHandler();
            c.AllowAutoRedirect = false;
            var client = new HttpClient(c, true);

            string json = JsonConvert.SerializeObject(new { loginID = Helpers.Settings.UserEmail, internalID = Helpers.Settings.InternalID });
            byte[] byteData = Encoding.UTF8.GetBytes(json);
            HttpResponseMessage response;

            using(var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.GetAsync("http://localhost:5841/api/users/" + Helpers.Settings.InternalID + "/mfa?loginID=" + Helpers.Settings.UserEmail);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("SMS 확인", "휴대폰에 수신된 6자리 숫자를 입력하세요", "OK");
                }
                else
                {
                    await DisplayAlert("오류 발생", "잠시 후 다시 시도하세요", "OK");
                }
            }

            
        }

        protected override bool OnBackButtonPressed()
        {
            vm.RemainSeconds = 0;
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            vm.RemainSeconds = 0;
            base.OnDisappearing();
        }

        public async void OnBtnSubmitClicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await this.FadeTo(0.5);

            string MFAToken = this.txtID.Text;
            string userEmail = Helpers.Settings.UserEmail;
            string internalID = Helpers.Settings.InternalID;

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
                    MFAToken = MFAToken.Trim();

                    HttpClientHandler c = new HttpClientHandler();
                    c.AllowAutoRedirect = false;
                    var client = new HttpClient(c, true);

                    string json = JsonConvert.SerializeObject(new { loginID = userEmail, internalID = internalID });
                    byte[] byteData = Encoding.UTF8.GetBytes(json);
                    HttpResponseMessage response;
                    using(var content = new ByteArrayContent(byteData))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.PostAsync("http://localhost:5841/api/users/" + internalID + "/mfa?code=" + MFAToken, content);

                        if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            await DisplayAlert("서버 오류", "서버가 죽었나 봅니다...", "OK");
                        } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            await DisplayAlert("Wrong Number", "인증 번호가 틀렸습니다", "OK");
                            this.txtID.Text = string.Empty;
                        } else if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            await DisplayAlert("YEAH!", "인증 완료", "OK");
                            await Navigation.PopAsync();
                        } else
                        {
                            await DisplayAlert("something went wrong", "미안.. 뭐가 잘못인지 모르겠어", "OK");
                        }
                    }
                }
                catch(Exception ex)
                {
                    await DisplayAlert("something went wrong", "미안.. 뭐가 잘못인지 모르겠어", "OK");
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
