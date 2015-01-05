using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Breda_Maps.Model
{
    public class Route
    {
        private List<Sight> points = new List<Sight>();
        private string _name;
        public Route(string name)
        {
            _name = name;
        }

        public Route()
        {

        }

        public void addpoint(Sight sight)
        {
            points.Add(sight);
        }

        public string GetName()
        {
            return _name;
        }

        public List<Sight> getRoute()
        {
            return points;
        }
    }
}
