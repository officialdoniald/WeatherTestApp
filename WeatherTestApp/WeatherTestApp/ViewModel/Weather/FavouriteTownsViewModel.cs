using Xamarin.Forms.MVVM.ViewModel.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using WeatherTestApp.Helpers;
using WeatherTestApp.Resources.Languages;
using System;

namespace WeatherTestApp.ViewModel.Weather
{
    public class FavouriteTownsViewModel : ViewModelBase
    {
        #region Properties

        #endregion

        #region Commands

        #endregion

        public FavouriteTownsViewModel()
        {

        }

        public override async Task InitializeAsync(INavigation navigation, params object[] navigationData)
        {
            Navigation = navigation;
        }
    }
}