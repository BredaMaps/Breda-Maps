using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breda_Maps.Model;
using Windows.Devices.Geolocation;

namespace Breda_Maps.database
{
    class TestDatabase
    {
        private List<Sight> sights;
        private List<Route> routes;
        public TestDatabase()
        {
            sights.Add(new Sight("VVV Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59380,Longitude = 4.77963})));
            sights.Add(new Sight("Liefdeszuster", new Geopoint(new BasicGeoposition() { Latitude = 51.59307, Longitude = 4.77969 })));
            sights.Add(new Sight("Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 })));
            sights.Add(new Sight("Nassau Baronie Monument", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 })));
            sights.Add(new Sight("The Light House", new Geopoint(new BasicGeoposition() { Latitude = 51.59256, Longitude = 4.77889 })));
            sights.Add(new Sight("1e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59265, Longitude = 4.77844 })));
            sights.Add(new Sight("2e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59258, Longitude = 4.77806 })));
            sights.Add(new Sight("Einde park", new Geopoint(new BasicGeoposition() { Latitude = 51.59059, Longitude = 4.77707 })));

            Route temp = new Route();
            temp.addpoint(sights[0]);
            temp.addpoint(sights[1]);
            routes.Add(temp);
            temp = new Route();
            temp.addpoint(sights[2]);
            temp.addpoint(sights[3]);
            routes.Add(temp);
        }

        public List<Sight> getAllSights()
        {
            return sights;
        }

        public List<Route> getAllRoutes()
        {
            return routes;
        }

        public Route getRoute(int routeId)
        {
            if (routeId < routes.Count)
                return routes[routeId];
            else return null;
        }

        public Sight getSight(int sightId)
        {
            if (sightId < sights.Count)
                return sights[sightId];
            else return null;
        }
    }
}
