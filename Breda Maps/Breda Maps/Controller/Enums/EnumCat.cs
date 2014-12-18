using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breda_Maps.Controller.Enums
{
    public enum EnumCat
    {
        MUSEUM,
        BAR,
        CHURCH,
        CULTURE,
        PARK,
        FACILITY,
        ROUTEPOINT
    }

    public static class ErrorLevelExtensions
    {
        public static string ToFriendlyString(this EnumCat me)
        {
            switch (me)
            {
                case EnumCat.MUSEUM:
                    return "This is Museum";
                case EnumCat.BAR:
                    return "This is a Bar";
                case EnumCat.CHURCH:
                    return "This is a church";
                case EnumCat.CULTURE:
                    return "This is a culture";
                case EnumCat.PARK:
                    return "This is a Park";
                case EnumCat.FACILITY:
                    return "This is a Facility";
                case EnumCat.ROUTEPOINT:
                    return "This is a Routepoint";
                default:
                    return "WTF?";
            }
            
        }
    }
}

