using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Breda_Maps.Controller.Enums;
using SQLite;

namespace Breda_Maps.Model
{
    public class Sight : IEnumerable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("description")]
        public string _description { get; set; }
        [Column("latitude")]
        public double latitude { get; set; }
        [Column("longitude")]
        public double longitude { get; set; }

        private EnumCat _category;

        private Geopoint _location;

        //private String _site;
        //private String _media;


        public Sight(String description, Geopoint location, EnumCat category)
        {
            _description = description;
            _category = category;
            _location = location;
            latitude = location.Position.Latitude;
            longitude = location.Position.Longitude;
	    }

        public EnumCat Category
        {
            get { return _category; }
        }

        public Sight()
        {

        }

        public Geopoint getLocation()
        {
            return this._location;
        }

        public String getDescription()
        {
            return _description;
        }

 	/*public String getMedia()
        {
            return _media;
	}*/

public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
