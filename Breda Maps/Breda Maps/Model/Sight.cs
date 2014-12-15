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
        private Geoposition location;

        private Sight(String description, Geoposition location)
        {
            this.description = description;
            this.location = location;
        }

        private Geoposition getLocation()
        {

            return location;
        }

        private String getDescription()
        {
            return this.description;
        }
    }
}
