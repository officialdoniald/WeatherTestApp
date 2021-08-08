using WeatherTestApp.Models.UIs;
using Xamarin.Forms;
using Xamarin.Forms.MVVM.ViewModel.Base;

namespace WeatherTestApp.View.Weather
{
    public partial class TownDescriptionPage : ContentPage
    {
        public TownDescriptionPage(TownUI townUI)
        {
            InitializeComponent();

            ((ViewModelBase)BindingContext).InitializeAsync(Navigation, townUI);
        }
    }
}