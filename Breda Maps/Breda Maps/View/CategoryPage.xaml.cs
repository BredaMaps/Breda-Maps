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
                // show the facility points to the map
                MapIcon MapIcon1 = new MapIcon();
                MapIcon1.Location = new Geopoint(facility.getLocation().Position);
                MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                MapIcon1.Title = facility.getDescription();
                //MapControl1.MapElements.Add(MapIcon1);
            }
        }

        private void Cat2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Cat3_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Cat4_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Cat5_Checked(object sender, RoutedEventArgs e)
        {

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
