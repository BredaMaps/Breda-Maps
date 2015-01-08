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
        [Column("info")]
        public string _info { get; set; }
        [Column("latitude")]
        public double latitude { get; set; }
        [Column("longitude")]
        public double longitude { get; set; }
        [Column("image")]
        public string _image { get; set; }
        [Column("video")]
        public string _video { get; set; }
        [Column("sound")]
        public string _sound { get; set; }
        [Column("site")]
        public string _site { get; set; }

        private EnumCat _category;


        //private String _site;
        //private String _media;


        public Sight(String description, Geopoint location, EnumCat category)
        {
            _description = description;
            _category = category;
            latitude = location.Position.Latitude;
            longitude = location.Position.Longitude;
	    }

        public Sight(String description, Geopoint location, string image)
        {
            _description = description;
            _image = image;
            latitude = location.Position.Latitude;
            longitude = location.Position.Longitude;
        }

        public Sight()
        {

        }

        public EnumCat Category
        {
            get { return _category; }
        }

        public void addImagesPath(string foto)
        {
            if (_image==null)
                _image += ";";
            _image += foto;
        }

        public void addVideoPath(string video)
        {
            if (_video == null)
                _video += ";";
            _video += video;
        }

        public void addGeluidPath(string geluid)
        {
            if (_sound == null)
                _sound += ";";
            _sound += geluid;
        }

        public Geopoint getLocation()
        {
            return new Geopoint(new BasicGeoposition() { Latitude = latitude, Longitude = longitude });
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
