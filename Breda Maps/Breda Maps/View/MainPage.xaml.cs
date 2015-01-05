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
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.Services.Maps;
using Breda_Maps.Model;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Breda_Maps.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : GUI
    {
        private MapIcon currentPosIcon;
        private Color[] colors = new Color[]{Colors.Blue,Colors.Red, Colors.Green, Colors.Yellow, Colors.Orange};
        BasicGeoposition StartPosition = new BasicGeoposition()
                {
                    Latitude = 51.5938D,
                    Longitude = 4.77963D            
                };
        BasicGeoposition CurrentPosition = new BasicGeoposition(){
            Latitude = 51.5938D,
            Longitude = 4.77963D  
        };
        private Boolean scrolled = true;

        public MainPage()
        {
            this.InitializeComponent();
            _rc.SetMap(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string routeName = e.Parameter as string;
            _rc.selectRoute(routeName);
            Debug.WriteLine("Navigated to mainpage");
            MapControl1.Center = new Geopoint(StartPosition);
            MapControl1.ZoomLevel = 18;
            MapControl1.LandmarksVisible = true;
            AddCurrentPositionIcon();
            InitRoute();
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

        private void AddCurrentPositionIcon()
        {
            currentPosIcon = new MapIcon();
            currentPosIcon.Location = new Geopoint(StartPosition);
            currentPosIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            currentPosIcon.Title = "";
            MapControl1.MapElements.Add(currentPosIcon);
            MapControl1.Center = new Geopoint(StartPosition);
        }

        public async void InitRoute()
        {
            //Debug.WriteLine(_rc.GetCurrentRoute().getRoute()[0].getLocation().Position.Latitude);
            Geopoint startpoint;
            Geopoint endpoint;
            int colorChoice = 0;

            for (int i = 0; i < _rc.GetCurrentRoute().getRoute().Count - 2; i++ )
            {
                startpoint = _rc.GetCurrentRoute().getRoute()[i].getLocation();
                endpoint = _rc.GetCurrentRoute().getRoute()[i+1].getLocation();
                MapRouteFinderResult routeResult = await MapRouteFinder.GetWalkingRouteAsync(
                    startpoint,
                    endpoint
                  );
                DisplayRoute(routeResult, colorChoice);
                colorChoice++;
                if (colorChoice == colors.Length)
                {
                    colorChoice = 0;
                }
            }
        }

        public async void DisplayRoute(MapRouteFinderResult routeResult, int colorChoice)
        {
            MapRouteView routeView = new MapRouteView(routeResult.Route);
            routeView.RouteColor = colors[colorChoice];
            routeView.OutlineColor = colors[colorChoice];

            MapControl1.Routes.Add(routeView);
            //await MapControl1.TrySetViewBoundsAsync(routeResult.Route.BoundingBox,
            //    null, MapAnimationKind.None);
        }

        public void SetNewPosition(Geoposition geoPosition)
        {
            CurrentPosition.Latitude = geoPosition.Coordinate.Point.Position.Latitude;
            CurrentPosition.Longitude = geoPosition.Coordinate.Point.Position.Longitude;
            Debug.WriteLine(CurrentPosition.Latitude + " en " + CurrentPosition.Longitude);
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>{
            currentPosIcon.Location = new Geopoint(CurrentPosition);
            if (!scrolled)
            {
                MapControl1.Center = new Geopoint(CurrentPosition);
            }
            });
        }

        private void Bn_Menu_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GOTO Menu");
            this.Frame.Navigate(typeof(View.MenuPage), e);
        }

        private void Bn_Loc_Click(
            object sender, RoutedEventArgs e)
        {
            scrolled = false;            
            //Debug.WriteLine("GOTO own Location");
            //if (geo == null)
            //{
            //    geo = new Geolocator();
            //}
            //Geoposition pos = await geo.GetGeopositionAsync();
            //AddCurrentPositionIcon(pos.Coordinate.Point.Position.Latitude, pos.Coordinate.Point.Position.Longitude);
            //Debug.WriteLine("Latitude: " + pos.Coordinate.Point.Position.Latitude + " Longitude: " + pos.Coordinate.Point.Position.Longitude);
        }   

        private void MapScrolled(object sender, RoutedEventArgs e)
        {
            scrolled = true;
        }

        private void MapScrolled(MapControl sender, object args)
        {
            scrolled = true;
        }

        private void MapScrolled(object sender, PointerRoutedEventArgs e)
        {
            scrolled = true;
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
