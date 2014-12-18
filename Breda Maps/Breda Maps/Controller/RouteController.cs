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

        public RouteController()
        {
            
        }
        public void addRoute(Route route)
        {
            routes.Add(route);
        }

        public Route selectRoute(Route route)
        {
            //selecteer een route ?
            return route;
        }

        public List<Sight> getSights()
        {
            return sights;
        } 
    }
}
