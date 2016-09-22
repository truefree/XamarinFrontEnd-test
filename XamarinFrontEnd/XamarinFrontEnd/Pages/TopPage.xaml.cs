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
            if(App.IsUserLoggedIn)
            {
                this.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Green);
            }
            else
            {
                this.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#ea002c"));
            }

            this.txtID.Text = Helpers.Settings.UserEmail;
        }

        public async void OnBtnRegisterClicked(object sender, EventArgs evt)
        {
            //Navigation.PushAsync(new Pages.Registration());
            //this.btnRegister.IsEnabled = false;
            this.IsEnabled = false;
            await this.FadeTo(0.5);

            string loginID = this.txtID.Text;
            if(string.IsNullOrEmpty(loginID)
                || Helpers.Parser.IsEmailString(loginID) == false)
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
                    HttpClientHandler c = new HttpClientHandler();
                    c.AllowAutoRedirect = false;
                    var client = new HttpClient(c, true);

                    string json = JsonConvert.SerializeObject(new { loginID = loginID });
                    byte[] byteData = Encoding.UTF8.GetBytes(json);
                    HttpResponseMessage response;
                    using(var content = new ByteArrayContent(byteData))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.PostAsync("http://localhost:5841/api/users/", content);

                        //response.
                        if(response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable
                                || response.StatusCode == System.Net.HttpStatusCode.InternalServerError
                            )
                        {
                            await DisplayAlert("Server Error", "서버가 죽었나 봅니다..", "OK");
                        }
                        else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest
                            || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            await DisplayAlert("뭔가 잘못 됐어", "메일 주소를 다시 확인해주세요.", "OK");
                        }
                        else if(response.StatusCode == System.Net.HttpStatusCode.Found)
                        {
                            if(response.Headers.Location.ToString().Contains("/mfa")) // init 성공 후 MFA면...
                            {
                                // internal ID 저장하고 Input Page로
                                Helpers.Settings.InternalID = Helpers.Parser.ParseGuidToString(response.Headers.Location.ToString());
                                await DisplayAlert("SMS 확인", "휴대폰에 수신된 6자리 숫자를 입력하세요", "OK");
                            }
                            else
                            {
                                // 이미 있음
                                Helpers.Settings.InternalID = Helpers.Parser.ParseGuidToString(response.Headers.Location.ToString());
                                await DisplayAlert("Already Existed", "이미 등록된 계정 입니다.", "OK");
                                await Navigation.PushAsync(new Pages.MFAChallenge());
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Server Error", "통신 오류 입니다. 잠시 후 다시 시도해주세요.", "OK");
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
