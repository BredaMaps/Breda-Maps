using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using Breda_Maps.Controller;
using Breda_Maps.Controller.Enums;
using Breda_Maps.Model;

namespace Breda_Maps.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : GUI
    {
        private List<Sight> _facilities = new List<Sight>();
        private List<Sight> _bars = new List<Sight>();
        private List<Sight> _church = new List<Sight>();
        private List<Sight> _park = new List<Sight>();
        private List<Sight> _cultures = new List<Sight>();

        public CategoryPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Cat1_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Sight facility in this.getCategory(EnumCat.FACILITY))
            {
                // make a list and add all facility's with corresponding details
                _facilities.Add(facility);
            }
        }

        private void Cat2_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Sight bar in this.getCategory(EnumCat.BAR))
            {
                _bars.Add(bar);
            }
        }

        private void Cat3_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Sight church in this.getCategory(EnumCat.CHURCH))
            {
                _church.Add(church);
            }
        }
        private void Cat4_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Sight park in this.getCategory(EnumCat.PARK))
            {
                _park.Add(park);
            }
        }
        private void Cat5_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Sight culture in this.getCategory(EnumCat.CULTURE))
            {
                _cultures.Add(culture);
            }
        }

        private void cat1_unchecked(object sender, RoutedEventArgs e)
        {
            _facilities.Clear();
        }
        private void cat2_unchecked(object sender, RoutedEventArgs e)
        {
            _bars.Clear();
        }
        private void cat3_unchecked(object sender, RoutedEventArgs e)
        {
            _church.Clear();
        }
        private void cat4_unchecked(object sender, RoutedEventArgs e)
        {
            _park.Clear();
        }
        private void cat5_unchecked(object sender, RoutedEventArgs e)
        {
            _cultures.Clear();
        }

        public IOrderedEnumerable<Sight> getCategory(EnumCat type)
        {
            var temp = _rc.getSights();

            var sight =
                from cat in temp
                where cat.Category == type
                orderby cat.Category
                ascending
                select cat;

            return sight;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //send the selected categorie positions and corresponding names
        }
    }
}
