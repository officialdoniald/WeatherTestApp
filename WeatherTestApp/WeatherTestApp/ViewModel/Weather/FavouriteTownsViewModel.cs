using Xamarin.Forms.MVVM.ViewModel.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Linq;

namespace WeatherTestApp.ViewModel.Weather
{
    public class FavouriteTownsViewModel : ViewModelBase
    {
        #region Properties

        //private ObservableCollection<SizeUI> _sizes;

        //public ObservableCollection<SizeUI> Sizes
        //{
        //    get { return _sizes; }
        //    set
        //    {
        //        _sizes = value;
        //        RaisePropertyChanged(() => Sizes);
        //    }
        //}

        #endregion

        #region Commands

        public ICommand SaveCommand => new Command<object>(Save);

        private void Save(object obj)
        {

        }

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