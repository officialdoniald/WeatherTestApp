using Xamarin.Forms.MVVM.ViewModel.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using WeatherTestApp.Helpers;
using System.Collections.ObjectModel;
using WeatherTestApp.Models.UIs;
using WeatherTestApp.View.Weather;
using WeatherTestApp.Database;
using System.Linq;
using WeatherTestApp.Configurations;

namespace WeatherTestApp.ViewModel.Weather
{
    public class FavouriteTownsViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<TownUI> _favouriteTownList = new ObservableCollection<TownUI>();

        public ObservableCollection<TownUI> FavouriteTownList
        {
            get { return _favouriteTownList; }
            set
            {
                _favouriteTownList = value;
                RaisePropertyChanged(() => FavouriteTownList);
            }
        }


        private bool _isFavouriteListEmpty = false;

        public bool IsFavouriteListEmpty
        {
            get { return _isFavouriteListEmpty; }
            set
            {
                _isFavouriteListEmpty = value;
                RaisePropertyChanged(() => IsFavouriteListEmpty);
            }
        }

        #endregion

        #region Commands

        public ICommand TownClickedCommand => new Command<TownUI>(TownClicked);

        private async void TownClicked(TownUI townUI)
        {
            await Navigation.PushAsync(new TownDescriptionPage(townUI));
        }


        public ICommand DeleteCommand => new Command<TownUI>(Delete);

        private async void Delete(TownUI townUI)
        {
            await DeleteTown(townUI);
        }

        #endregion

        public FavouriteTownsViewModel()
        {
            GlobalEvents.OnTownDeleted += OnTownDeleted;
            GlobalEvents.OnTownAdded += OnTownAdded;
        }

        public override async Task InitializeAsync(INavigation navigation, params object[] navigationData)
        {
            Navigation = navigation;

            var list = await AppConfig.Instance.LocalDatabase.GetTowns();

            Device.BeginInvokeOnMainThread(() =>
            {
                FavouriteTownList = TownUI.MapTownListEntityToTownUIList(list);

                CheckTownListEmpty();
            });
        }

        private void OnTownAdded(object sender, TownUI e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                FavouriteTownList.Add(e);

                CheckTownListEmpty();
            });
        }

        private async void OnTownDeleted(object sender, TownUI e)
        {
            await DeleteTown(e);
        }

        private async Task DeleteTown(TownUI town)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsLoading = true;
            });

            if (FavouriteTownList.Contains(town))
            {
                await AppConfig.Instance.LocalDatabase.DeleteTown(town.Id);

                Device.BeginInvokeOnMainThread(() =>
                {
                    FavouriteTownList.Remove(town);

                    CheckTownListEmpty();
                });
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                IsLoading = false;
            });
        }

        private void CheckTownListEmpty()
        {
            if (FavouriteTownList.Any())
            {
                IsFavouriteListEmpty = false;
            }
            else
            {
                IsFavouriteListEmpty = true;
            }
        }
    }
}