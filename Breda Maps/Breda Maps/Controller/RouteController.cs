using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breda_Maps.Model;
using Breda_Maps.View;

namespace Breda_Maps.Controller
{
    public class RouteController
    {
        private List<Route> routes = new List<Route>(); 
        private List<Sight> sights = new List<Sight>();

        private Task sendLocation;
        GpsLocalizer gpsLoc = new GpsLocalizer();
        MainPage map;
        public RouteController()
        {
            init();
           
            routes.Add(new Route("Test 1"));
            routes.Add(new Route("Test 2"));
            routes.Add(new Route("Test 3"));
            routes.Add(new Route("Test 4"));
        }

        public RouteController(MainPage mp)
        {
            map = mp;
            init();
            initLocation();
        }

        public void SetMap(MainPage mp)
        {
            map = mp;
            initLocation();
        }

        private void init()
        {
            //DEBUG
            TestFillSights();
            TestFillRoutes();
        }

        private void initLocation()
        {
            sendLocation = new Task(sendNewLocation);
            sendLocation.Start();
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
                }
            }
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
            // create and fill routes
        }

        private void TestFillSights()
        {
            // create and fill sights
        }
#endregion
    }
}
