using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherTestApp.Resources.Languages;
using Xamarin.Forms;

namespace WeatherTestApp.Helpers
{
    public class Util
    {
        public static Util Instance { get; } = new Util();

        public async Task<string> CRUD(string uri, object sendingObject, HttpMethod method)
        {
            try
            {
                HttpResponseMessage responseMessage;

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(uri);
                request.Method = method;
                request.Headers.Add("Accept", "aplication/json");

                if (sendingObject != null)
                {
                    string json = JsonConvert.SerializeObject(sendingObject);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;
                }

                var client = new HttpClient();

                var response = responseMessage = await client.SendAsync(request);

                var responseString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseString))
                {
                    responseString = responseMessage.StatusCode.ToString();
                }

                return responseString;
            }
            catch (Exception ex)
            {
#if DEBUG
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert(
                        ApplicationResource.Common_Warning,
                        ex.StackTrace,
                        ApplicationResource.Common_OK);
                });
#endif

                return null;
            }
        }
    }
}