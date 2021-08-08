using Xamarin.Forms.MVVM.ViewModel.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using WeatherTestApp.Helpers;
using WeatherTestApp.Models.UIs;
using WeatherTestApp.Resources.Languages;
using System;
using Newtonsoft.Json;
using WeatherTestApp.Models.DTOs;
using System.Net.Http;
using System.Linq;

namespace WeatherTestApp.ViewModel.Weather
{
    public class TownDescriptionViewModel : ViewModelBase
    {
        #region Properties

        private TownUI _town = null;

        public TownUI Town
        {
            get { return _town; }
            set
            {
                _town = value;
                RaisePropertyChanged(() => Town);
            }
        }


        private string _skyInfo = ApplicationResource.Common_Empty;

        public string SkyInfo
        {
            get { return _skyInfo; }
            set
            {
                _skyInfo = value;
                RaisePropertyChanged(() => SkyInfo);
            }
        }


        private string _temperature = ApplicationResource.Common_Empty;

        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                RaisePropertyChanged(() => Temperature);
            }
        }

        #endregion

        #region Commands

        public ICommand DeleteCommand => new Command<object>(Delete);

        private async void Delete(object obj)
        {
            GlobalEvents.OnTownDeleted_Event(this, Town);

            await Navigation.PopAsync();
        }

        #endregion

        public TownDescriptionViewModel() { }

        public override async Task InitializeAsync(INavigation navigation, params object[] navigationData)
        {
            Navigation = navigation;

            Town = (TownUI)navigationData[0];

            Device.BeginInvokeOnMainThread(() =>
            {
                IsLoading = true;
            });

            try
            {
                var townSearchUrl = string.Format(ApplicationResource.Common_WeatherApiTownSearchRequest, Town.Name);

                var townWeatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(await Util.Instance.CRUD(townSearchUrl, null, HttpMethod.Get)).Data.FirstOrDefault();

                Device.BeginInvokeOnMainThread(() =>
                {
                    SkyInfo = townWeatherResponse.Weather.Description;

                    Temperature = string.Format(ApplicationResource.Common_Celsius, Math.Round(townWeatherResponse.Temp, 0));
                });
            }
            catch (Exception)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SkyInfo = ApplicationResource.Common_Empty;

                    Temperature = ApplicationResource.Common_Empty;

                    IsLoading = false;
                });

                return;
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                IsLoading = false;
            });
        }
    }
}