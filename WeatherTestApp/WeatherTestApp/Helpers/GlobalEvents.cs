using System;
using WeatherTestApp.Models.UIs;

namespace WeatherTestApp.Helpers
{
    public static class GlobalEvents
    {
        public static event EventHandler<TownUI> OnTownDeleted;

        public static void OnTownDeleted_Event(object sender, TownUI townUI)
        {
            OnTownDeleted?.Invoke(sender, townUI);
        }


        public static event EventHandler<TownUI> OnTownAdded;

        public static void OnTownAdded_Event(object sender, TownUI townUI)
        {
            OnTownAdded?.Invoke(sender, townUI);
        }
    }
}