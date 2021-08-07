using Xamarin.Forms;
using Xamarin.Forms.MVVM.ViewModel.Base;

namespace WeatherTestApp.View.Weather
{
    public partial class FavouriteTownsPage : ContentPage
    {
        public FavouriteTownsPage()
        {
            InitializeComponent();

            ((ViewModelBase)BindingContext).InitializeAsync(Navigation);
        }
    }
}