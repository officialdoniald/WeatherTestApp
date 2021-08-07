using Xamarin.Forms;
using Xamarin.Forms.MVVM.ViewModel.Base;

namespace WeatherTestApp.View.Common
{
    public partial class MenuPage : TabbedPage
    {
        public MenuPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            ((ViewModelBase)BindingContext).InitializeAsync(Navigation);
        }
    }
}