using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breda_Maps.Model;

namespace Breda_Maps.Controller
{
    public class RouteController
    {
        private List<Route> routes = new List<Route>(); 
        private List<Sight> sights = new List<Sight>();

        private Task sendLocation;
        GpsLocalizer gpsLoc = new GpsLocalizer();
        View.MainPage map;
        public RouteController()
        {
            sendLocation = new Task(sendNewLocation);
            sendLocation.Start();

            //DEBUG
            TestFillSights();
            TestFillRoutes();
            routes.Add(new Route("Test 1"));
            routes.Add(new Route("Test 2"));
            routes.Add(new Route("Test 3"));
            routes.Add(new Route("Test 4"));
        }

        public void addRoute(Route route)
        {
            routes.Add(route);
        }

        private async void sendNewLocation()
        {
            //later gps doorsturen.
        }

        public Route selectRoute(Route route)
        {
            //selecteer een route ?
            return route;
        }
        public Route selectRoute(String routeNaam)
        {
            //selecteer een route ?
            foreach (Route r in routes)
            {
                if(r.GetName() == routeNaam)
                {
                    return r;
                }
            }
            return null;
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
        private void TestFillRoutes()
        {
            throw new NotImplementedException();
        }

        private void TestFillSights()
        {
            throw new NotImplementedException();
        }
#endregion
    }
}
