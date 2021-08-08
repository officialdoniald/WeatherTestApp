using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeatherTestApp.Database.Entities;
using Xamarin.Forms.MVVM.ViewModel.Base;

namespace WeatherTestApp.Models.UIs
{
    public class TownUI : ExtendedBindableObject
    {
        public int Id { get; set; }


        private string _name;

        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }


        public static TownUI MapTownEntityToTownUI(TownEntity townEntity)
        {
            return new TownUI()
            {
                Id = townEntity.Id,
                Name = townEntity.Town
            };
        }

        public static ObservableCollection<TownUI> MapTownListEntityToTownUIList(List<TownEntity> townEntities)
        {
            ObservableCollection<TownUI> list = new ObservableCollection<TownUI>();

            foreach (var townEntity in townEntities)
            {
                list.Add(MapTownEntityToTownUI(townEntity));
            }

            return list;
        }
    }
}