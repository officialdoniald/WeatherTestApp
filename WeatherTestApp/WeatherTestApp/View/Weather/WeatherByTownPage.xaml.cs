using Xamarin.Forms;
using Xamarin.Forms.MVVM.ViewModel.Base;

namespace WeatherTestApp.View.Weather
{
    public partial class WeatherByTownPage : ContentPage
    {
        public WeatherByTownPage()
        {
            InitializeComponent();

            ((ViewModelBase)BindingContext).InitializeAsync(Navigation);
        }
    }
}