using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
                this.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Green);
            } else
            {
                this.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#ea002c"));
            }

            this.txtID.Text = App.UserEmail;
        }

        public async void OnBtnRegisterClicked(object sender, EventArgs evt)
        {
            //Navigation.PushAsync(new Pages.Registration());
            //this.btnRegister.IsEnabled = false;
            this.IsEnabled = false;
            await this.FadeTo(0.5);

            string loginID = this.txtID.Text;
            if(string.IsNullOrEmpty(loginID))
            {
                await DisplayAlert("메일 주소 필요", "Email Address에 사용 중인 @sk.com / @partner.sk.com 메일 주소를 입력하세요.", "OK");
                await this.FadeTo(1);
                this.IsEnabled = true;
            }
            else
            {

                loginID = loginID.Trim();
                Helpers.Settings.UserEmail = loginID;

                try
                {
                    var client = new HttpClient();

                    string json = JsonConvert.SerializeObject(new { loginID = loginID });
                    byte[] byteData = Encoding.UTF8.GetBytes(json);
                    HttpResponseMessage response;
                    using(var content = new ByteArrayContent(byteData))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.PostAsync("http://localhost:5841/api/users/", content);

                        if(response.StatusCode != System.Net.HttpStatusCode.OK //200
                                && response.StatusCode != System.Net.HttpStatusCode.Created // 201
                                && response.StatusCode != System.Net.HttpStatusCode.Redirect // 304
                            )
                        {
                            await DisplayAlert("Server Error", "서버가 죽었나 봅니다..", "OK");
                        }
                        else
                        {
                            // get user information?
                            await DisplayAlert("Created", "계정 등록이 완료 되었습니다.", "OK");
                            //await Navigation.PopAsync();
                        }
                    }
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Server Error", "통신 오류 입니다. 짜증나겠지만 잠시 후 다시 시도해주세요.", "OK");
                }
                finally
                {
                    await this.FadeTo(1);
                    this.IsEnabled = true;
                }

            }
        }

        void OnBtnLoginClicked(object sender, EventArgs evt)
        {
            //Navigation.PushAsync(new Pages.Registration());
        }

    }
}
