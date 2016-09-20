using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace XamarinFrontEnd
{
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }

        public async void OnRegClicked(object sender, EventArgs e)
        {
            string loginID = this.Entry1.Text.Trim();
            if (string.IsNullOrEmpty(loginID))
            {
                await DisplayAlert("사번이 없음", "사번을 넣으라고..", "OK");
            }



            var client = new HttpClient();

            string json = JsonConvert.SerializeObject(new { loginID = loginID });
            byte[] byteData = Encoding.UTF8.GetBytes(json);
            HttpResponseMessage response;
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync("http://localhost:5841/api/users/", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK //200
                        && response.StatusCode != System.Net.HttpStatusCode.Created // 201
                        && response.StatusCode != System.Net.HttpStatusCode.Redirect // 304
                    )
                {
                    await DisplayAlert("Server Error", "오류가 발생했다니 다시 시도..", "OK");
                }
                else
                {
                    // get user information?
                    await DisplayAlert("Created", "음성 등록을 진행하쇼", "OK");
                    await Navigation.PopAsync();
                }
            }
        }
    }
}
