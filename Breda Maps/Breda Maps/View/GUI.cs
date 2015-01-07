using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Breda_Maps.Controller;
using Windows.UI.Xaml;

namespace Breda_Maps.View
{
    /*
     * Hoofdverantwoordelijke:  Corné Derijck
     * Beschrijving:            Hoofdklasse waar alle schermen van overerven.
     * Bevat:                   Methodes en attributen die andere schermen nodig hebben
     * Extra:                   
     */

    public class GUI : Page
    {
        internal RouteController _rc;

        public GUI()
        {
            _rc = ((App)Application.Current)._rc;
        }

    }
}
