using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Breda_Maps.Controller
{
    class GpsLocalizer
    {
        private Geolocator geo = new Geolocator();
        private Geoposition geoPosition;
        private Task mapTask;

        public GpsLocalizer()
        {
            mapTask = new Task(findPosition);
            mapTask.Start();
        }

        public async void findPosition()
        {
            while (true)
            {
                geoPosition = await geo.GetGeopositionAsync();
            }
        }

        public Geoposition getPosition()
        {
            if (geoPosition != null)
            {
                return geoPosition;
            }
            else
            {
                return null;
            }
        }
    }

}
