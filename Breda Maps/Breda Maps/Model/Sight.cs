using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Breda_Maps.Model
{
    class Sight
    {
        private String description;
        private Geopoint location;

        public Sight(String description, Geopoint location)
        {
            this.description = description;
            this.location = location;
        }

        public Geopoint getLocation()
        {

            return location;
        }

        public String getDescription()
        {
            return this.description;
        }
    }
}
