using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Breda_Maps.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : GUI
    {
        public MainPage()
        {
            this.InitializeComponent();


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MapControl1.Center =
                new Geopoint(new BasicGeoposition()
                {
                    Latitude = 51.5938D,
                    Longitude = 4.77963D
                });
            MapControl1.ZoomLevel = 18;
            MapControl1.LandmarksVisible = true;
            AddMapIcon();
        }

        private void AddMapIcon()
        {
            MapIcon MapIcon1 = new MapIcon();
            MapIcon1.Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = 51.5938D,
                Longitude = 4.77963D
            });
            MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MapIcon1.Title = "Space Needle";
            MapControl1.MapElements.Add(MapIcon1);
        }

        private void Bn_Menu_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GOTO Menu");
            this.Frame.Navigate(typeof(View.MenuPage), e);
        }

        private void Bn_Loc_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GOTO own Location");
            //ToDo
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}
    }
}
