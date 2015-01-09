using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breda_Maps.Model;
using Breda_Maps.View;
using Windows.Devices.Geolocation;

namespace Breda_Maps.Controller
{
    public class RouteController
    {
        private List<Route> routes = new List<Route>(); 
        private List<Sight> sights = new List<Sight>();
        private List<Sight> _sightsToShow = new List<Sight>();
        private List<Sight> _iconToShow = new List<Sight>();
        private Route _currentRoute;
        private database.DataBase db;

        private Task sendLocation;
        GpsLocalizer gpsLoc = new GpsLocalizer();
        MainPage map;
        public RouteController()
        {
            init();
  

        }

        public RouteController(MainPage mp)
        {
            map = mp;
            init();
            initLocation();
        }

        public int getIdFromDescription(String itemDescription)
        {
            int temp = db.getIdByDescription(itemDescription);
            return temp;
        }

        public string[] getImagesFromId(int id)
        {
            string[] temp = db.getImages(id);
            return temp;
        }

        public string getInfoFromId(int id)
        {
            string temp = db.getInfo(id);
            return temp;
        }

        public string getSiteFromId(int id)
        {
            string temp = db.getSite(id);
            return temp;
        }
        public void SetMap(MainPage mp)
        {
            map = mp;
            initLocation();
        }

        private void init()
        {
            db = new database.DataBase("Breda_Maps.s3db", true);
            sights = db.getSights();
            Route route = new Route("Breda Maps Tour");
            foreach (Sight s in sights)
            {
                route.addpoint(s);
            }
            routes.Add(route);
            //DEBUG
            //TestFillSights();
            //TestFillRoutes();
        }

        private void initLocation()
        {
            sendLocation = new Task(sendNewLocation);
            sendLocation.Start();
        }

        public void setAllIconLocations(List<Sight> list)
        {
            _iconToShow = list;
        }

        public List<Sight> GetIconLocations()
        {
            return _iconToShow;
        }

        public void SetCategories(List<Sight> list)
        {
            _sightsToShow = list;
        }

        public List<Sight> GetCategories()
        {
            return _sightsToShow;
        }

        public void addRoute(Route route)
        {
            routes.Add(route);
        }

        private void sendNewLocation()
        {
            while(true)
            {
                if (gpsLoc.getPosition() != null)
                {
                    map.SetNewPosition(gpsLoc.getPosition());
                    sendLocation.Wait(500);
                }
            }
        }

        public Route selectRoute(Route route)
        {
            //selecteer een route ?
            return route;
        }
        public void selectRoute(String routeNaam)
        {
            //selecteer een route ?
            foreach (Route r in routes)
            {
                if(r.GetName() == routeNaam && r != null)
                {
                    _currentRoute = r;
                }
            }
        }

        public Route GetCurrentRoute()
        {
            if(_currentRoute != null)
            {
                return _currentRoute;
            }
            else
            {
                return null;
            }
        }


        public List<Route> GetRoutes() 
        {
            return routes;
        }

        public List<Sight> getSights()
        {
            return sights;
        }

#region TEST
        //private void TestFillRoutes()
        //{
        //    Route testRoute = new Route("Real Test");
        //    foreach(Sight s in sights)
        //    {
        //        testRoute.addpoint(s);
        //    }
        //    routes.Add(testRoute);
        //    routes.Add(new Route("Test 1"));
        //    routes.Add(new Route("Test 2"));
        //    routes.Add(new Route("Test 3"));
        //    routes.Add(new Route("Test 4"));
        //}

        //private void TestFillSights()
        //{
        //    sights.Add(new Sight("VVV Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59380, Longitude = 4.77963 }), Enums.EnumCat.FACILITY));
        //   // sights.Add(new Sight("Liefdeszuster", new Geopoint(new BasicGeoposition() { Latitude = 51.59307, Longitude = 4.77969 }), Enums.EnumCat.CULTURE));
        //    sights.Add(new Sight("Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), Enums.EnumCat.PARK));
        //   // sights.Add(new Sight("Nassau Baronie Monument", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), Enums.EnumCat.CULTURE));
        //    sights.Add(new Sight("The Light House", new Geopoint(new BasicGeoposition() { Latitude = 51.59256, Longitude = 4.77889 }), Enums.EnumCat.CULTURE));
        //    sights.Add(new Sight("1e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59265, Longitude = 4.77844 }), Enums.EnumCat.ROUTEPOINT));
        //    sights.Add(new Sight("2e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59258, Longitude = 4.77806 }), Enums.EnumCat.ROUTEPOINT));
        //    sights.Add(new Sight("Einde park", new Geopoint(new BasicGeoposition() { Latitude = 51.59059, Longitude = 4.77707 }), Enums.EnumCat.ROUTEPOINT));
        //}
#endregion
    }
}
