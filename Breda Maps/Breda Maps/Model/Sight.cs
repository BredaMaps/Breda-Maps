using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Breda_Maps.Model
{
    public class Sight
    {
        public string id { get; set; }
        public string description { get; set; }
        public double latitude { get; set; }

        private Geopoint location;
        private String site;
        private String media;

        public Sight()
        {
        }

        public Sight(String description, Geopoint location)
        {
            this.description = description;
            this.location = location;
        }

        public Geopoint getLocation()
        {

            return location;
        }

        public String getSite()
        {
            return this.site;
        }

        public String getMedia()
        {
            return this.media;
        }
    }
}
