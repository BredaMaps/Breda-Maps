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
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Breda_Maps.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : GUI
    {
        BasicGeoposition StartPosition = new BasicGeoposition()
                {
                    Latitude = 51.5938D,
                    Longitude = 4.77963D            
                };
        BasicGeoposition CurrentPosition;
        Geolocator geo = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MapControl1.Center = new Geopoint(StartPosition);
            MapControl1.ZoomLevel = 18;
            MapControl1.LandmarksVisible = true;

            AddStartPositionIcon(StartPosition);
        }

        private void AddStartPositionIcon(BasicGeoposition CurrentStartPosition)
        {
            MapIcon MapIcon1 = new MapIcon();
            MapIcon1.Location = new Geopoint(CurrentStartPosition);
            MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MapIcon1.Title = "";
            MapControl1.MapElements.Add(MapIcon1);
            MapControl1.Center = new Geopoint(StartPosition);
        }

        private void AddCurrentPositionIcon(double CurrentPositionLati, double CurrentPositionLong)
        {
            MapIcon MapIcon1 = new MapIcon();
            CurrentPosition.Latitude = CurrentPositionLati;
            CurrentPosition.Longitude = CurrentPositionLong;
            MapIcon1.Location = new Geopoint(CurrentPosition);
            MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MapIcon1.Title = "";
            MapControl1.MapElements.Add(MapIcon1);
            MapControl1.Center = new Geopoint(CurrentPosition);
        }

        private void Bn_Menu_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GOTO Menu");
            this.Frame.Navigate(typeof(View.MenuPage), e);
        }

        private async void Bn_Loc_Click(
            object sender, RoutedEventArgs e)
        {
            
            Debug.WriteLine("GOTO own Location");
            if (geo == null)
            {
                geo = new Geolocator();
            }
            Geoposition pos = await geo.GetGeopositionAsync();
            AddCurrentPositionIcon(pos.Coordinate.Point.Position.Latitude, pos.Coordinate.Point.Position.Longitude);
            Debug.WriteLine("Latitude: " + pos.Coordinate.Point.Position.Latitude + " Longitude: " + pos.Coordinate.Point.Position.Longitude);
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
