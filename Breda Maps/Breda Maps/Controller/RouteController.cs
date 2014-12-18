using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breda_Maps.Model;

namespace Breda_Maps.Controller
{
    class RouteController
    {
        private List<Route> routes = new List<Route>(); 
        private List<Sight> sights = new List<Sight>();
        private Task sendLocation;
        GpsLocalizer gpsLoc = new GpsLocalizer();
        View.MainPage map = new View.MainPage();
        public RouteController()
        {
            sendLocation = new Task(sendNewLocation);
            sendLocation.Start();
        }
        private void addRoute(Route route)
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

        private Route selectRoute(Route route)
        {
            //selecteer een route ?
            return route;
        }
    }
}
