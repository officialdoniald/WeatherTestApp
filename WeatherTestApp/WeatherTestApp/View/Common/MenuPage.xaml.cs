using Xamarin.Forms;
using Xamarin.Forms.MVVM.ViewModel.Base;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace WeatherTestApp.View.Common
{
    public partial class MenuPage : TabbedPage
    {
        public MenuPage()
        {
            InitializeComponent();

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSwipePagingEnabled(false);

            NavigationPage.SetHasNavigationBar(this, false);

            ((ViewModelBase)BindingContext).InitializeAsync(Navigation);
        }
    }
}