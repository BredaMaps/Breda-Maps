using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Breda_Maps.Controller.Enums;

namespace Breda_Maps.Model
{
    public class Sight : IEnumerable
    {
        public string id { get; set; }
        public string description { get; set; }
        public double latitude { get; set; }

        private EnumCat _category;

        private Geopoint location;

        private String site;
        private String media;


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
            return this.description;
        }

 	public String getMedia()
        {
            return this.media;
	}

public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
