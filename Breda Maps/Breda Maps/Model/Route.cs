using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Breda_Maps.Model
{
    class Route
    {
        private List<Geopoint> points = new List<Geopoint>();

        private Route()
        {
            addpoint();
        }

        private void addpoint()
        {

            Geopoint testpoint = new Geopoint(new BasicGeoposition() { Latitude = 51.59380, Longitude = 4.77963 }); // point van VVV Breda
            Geopoint testpoint2 = new Geopoint(new BasicGeoposition() { Latitude = 51.59307 , Longitude =4.77969 }); // point van Liefdeszuster

            points.Add(testpoint);
            points.Add(testpoint2);
        }

    }
}
