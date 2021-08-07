using Xamarin.Forms.MVVM.ViewModel.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherTestApp.Resources.Languages;
using WeatherTestApp.Helpers;
using System;
using Newtonsoft.Json;
using WeatherTestApp.Models.DTOs;

namespace WeatherTestApp.ViewModel.Weather
{
    public class WeatherByTownViewModel : ViewModelBase
    {
        #region Properties

        private bool _isNoTownDataFound = false;

        public bool IsNoTownDataFound
        {
            get { return _isNoTownDataFound; }
            set
            {
                _isNoTownDataFound = value;
                RaisePropertyChanged(() => IsNoTownDataFound);
            }
        }


        private string _town = string.Empty;

        public string Town
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

        public ICommand SearchCommand => new Command<object>(Search);

        private async void Search(object obj)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsLoading = true;
            });

            if (string.IsNullOrEmpty(Town))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert(
                        ApplicationResource.Common_Warning,
                        ApplicationResource.Common_FillAllEntries,
                        ApplicationResource.Common_OK);

                    IsLoading = false;
                });

                return;
            }

            try
            {
                var townSearchUrl = string.Format(ApplicationResource.Common_WeatherApiTownSearchRequest, Town);

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

                    IsNoTownDataFound = true;

                    IsLoading = false;
                });

                return;
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                IsNoTownDataFound = false;

                IsLoading = false;
            });
        }

        #endregion

        public WeatherByTownViewModel()
        {

        }

        public override async Task InitializeAsync(INavigation navigation, params object[] navigationData)
        {
            Navigation = navigation;
        }
    }
}