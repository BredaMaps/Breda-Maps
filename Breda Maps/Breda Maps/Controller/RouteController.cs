﻿using System;
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

        private RouteController()
        {
            
        }
        private Void addRoute(Route route)
        {
            routes.Add(route);
        }

        private Route selectRoute(Route route)
        {
            //selecteer een route ?
            return route;
        }
    }
}
