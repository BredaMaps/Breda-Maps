using Breda_Maps.Controller.Enums;
using Breda_Maps.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Breda_Maps.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class RoutePage : GUI
    {
        List<Route> _routes;
        private string _routeNaam;
        private List<Sight> _facilities = new List<Sight>();
        private List<Sight> _bars = new List<Sight>();
        private List<Sight> _church = new List<Sight>();
        private List<Sight> _park = new List<Sight>();
        private List<Sight> _cultures = new List<Sight>();

        //ListView listView;
        public RoutePage()
        {
            this.InitializeComponent();
            //listView = new ListView();
            //listView.m
        }
        

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadRoutes();
        }

        private void LoadRoutes()
        {
            //listView.FontSize = 50;
            _routes = _rc.GetRoutes();
            //TextBox tb1 = new TextBox();
            //tb1.Text = "Test 1";
            //tb1.FontSize = 50;
            listView.FontSize = 50;
            foreach(Route r in _routes)
            {
                //tb1.Text = r.GetName();
                //listView
                listView.Items.Add(r.GetName());
            }
            //listView.FontSize = 50;
            //mainGrid.Children.Add(listView);
        }

        private void Bn_Cat_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.CategoryPage), e);
        }
        private void Bn_Sta_Click(object sender, RoutedEventArgs e)
        {
            foreach (Sight facility in this.getCategory(EnumCat.FACILITY))
            {
                // make a list and add all facility's with corresponding details
                _facilities.Add(facility);
            }
            foreach (Sight bar in this.getCategory(EnumCat.BAR))
            {
                _bars.Add(bar);
            }
            foreach (Sight church in this.getCategory(EnumCat.CHURCH))
            {
                _church.Add(church);
            }
            foreach (Sight park in this.getCategory(EnumCat.PARK))
            {
                _park.Add(park);
            }

            foreach (Sight culture in this.getCategory(EnumCat.CULTURE))
            {
                _cultures.Add(culture);
            }

            if(_routeNaam != null)
            {
                List<Sight> allSelections = _facilities.Concat(_bars)
                        .Concat(_church)
                        .Concat(_park)
                        .Concat(_cultures)
                        .ToList();

                _rc.setAllIconLocations(allSelections);
                this.Frame.Navigate(typeof(View.MainPage), _routeNaam);
            }
            else
            {
                WarningBlock.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WarningBlock.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _routeNaam = (string)listView.SelectedItems[0];
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
    }
}
