using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breda_Maps.Model;
using Windows.Devices.Geolocation;

namespace Breda_Maps.database
{
    class DataBase
    {
        private TestDatabase tdb;

        public DataBase()
        {
            tdb = new TestDatabase();
        }

        public List<Sight> getSights()
        {
            return tdb.getAllSights();
        }

        public List<Route> getRoutes()
        {
            return tdb.getAllRoutes();
        }

        public Sight getsight(int ID)
        {
            return tdb.getSight(ID);
        }

        public Route getRoute(int ID)
        {
            return tdb.getRoute(ID);
        }

    }
}
