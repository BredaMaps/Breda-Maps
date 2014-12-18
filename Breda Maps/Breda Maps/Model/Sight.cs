using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Breda_Maps.Controller.Enums;

namespace Breda_Maps.Model
{
    public class Sight
    {
        private String description;
        private EnumCat category;
        private Geoposition location;

        private Sight(String description, Geoposition location, EnumCat category)
        {
            this.description = description;
            this.category = category;
            this.location = location;
        }
        public EnumCat Category
        {
            get { return category; }
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
