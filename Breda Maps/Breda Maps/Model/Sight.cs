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
        private String _description;
        private EnumCat _category;
        private Geopoint _location;

        public Sight(String description, Geopoint location, EnumCat category)
        {
            this._description = description;
            this._category = category;
            this._location = location;
	}
        public EnumCat Category
        {
            get { return _category; }
        }

        public Geopoint getLocation()
        {
            return this._location;
        }

        public String getDescription()
        {
            return this._description;
        }
        
    }
}
