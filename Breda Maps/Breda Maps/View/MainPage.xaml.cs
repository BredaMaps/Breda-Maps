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
using System.Threading.Tasks;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Breda_Maps.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : GUI
    {
        private MapIcon currentPosIcon;
        private MapRouteView currentRouteView;
        private Boolean initRouteDone = false;

        BasicGeoposition StartPosition = new BasicGeoposition()
                {
                    Latitude = 51.5938D,
                    Longitude = 4.77963D            
                };
        BasicGeoposition CurrentPosition = new BasicGeoposition(){
            Latitude = 51.5938D,
            Longitude = 4.77963D  
        };
        private bool _scrolled = false;
        private bool _doneMoving = true;
        private int _currentAmountOfPoints;

        public MainPage()
        {
            this.InitializeComponent();
            _rc.SetMap(this);

            
                CreateGeofence(StartPosition.Latitude, StartPosition.Longitude, 3);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string routeName = e.Parameter as string;
            _rc.selectRoute(routeName);
            MapControl1.Center = new Geopoint(StartPosition);
            MapControl1.ZoomLevel = 18;
            MapControl1.LandmarksVisible = true;
            
            AddCurrentPositionIcon();
            addCategoriePoints();
            Bn_Loc.Background = new SolidColorBrush(Colors.Blue);
            InitRoute();
        }

        public void addCategoriePoints()
        {
            foreach (Sight points in _rc.GetCategories())
            {
                MapIcon categorieIcon = new MapIcon();
                categorieIcon.Location = points.getLocation();
                categorieIcon.NormalizedAnchorPoint = new Point(0.5,1.0);
                categorieIcon.Title = points.getDescription();
                MapControl1.MapElements.Add(categorieIcon);
            } 
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

        public async void InitStartToRoute()
        {
            Debug.WriteLine("Current amount of points: " + _currentAmountOfPoints);
            if (_rc.GetCurrentRoute() != null)
            {
                Geopoint endpoint = _rc.GetCurrentRoute().getRoute()[0].getLocation();
                Geopoint currentPoint = new Geopoint(CurrentPosition);
                MapRouteFinderResult routeResult = await MapRouteFinder.GetWalkingRouteAsync(
                    currentPoint,
                    endpoint
                    );
                if (initRouteDone && MapControl1.Routes.Count == _currentAmountOfPoints)
                {
                    MapControl1.Routes.RemoveAt(MapControl1.Routes.Count - 2);
                }
                initRouteDone = true;
                currentRouteView = new MapRouteView(routeResult.Route);
                currentRouteView.RouteColor = Colors.Blue;
                currentRouteView.OutlineColor = Colors.Blue;

                MapControl1.Routes.Add(currentRouteView);
            }
        }

        public async void InitRoute()
        {
            _currentAmountOfPoints = _rc.GetCurrentRoute().getRoute().Count;
            Geopoint startpoint;
            Geopoint endpoint;
            if (_rc.GetCurrentRoute() != null)
            {
                for (int i = 0; i < _rc.GetCurrentRoute().getRoute().Count - 2; i++)
                {

                        startpoint = _rc.GetCurrentRoute().getRoute()[i].getLocation();
                        endpoint = _rc.GetCurrentRoute().getRoute()[i + 1].getLocation();
                        MapRouteFinderResult routeResult = await MapRouteFinder.GetWalkingRouteAsync(
                            startpoint,
                            endpoint
                            );
                        DisplayRoute(routeResult);
                }
            }
            InitStartToRoute();
        }

        public async void DisplayRoute(MapRouteFinderResult routeResult)
        {
            MapRouteView routeView = new MapRouteView(routeResult.Route);
            routeView.RouteColor = Colors.Blue;
            routeView.OutlineColor = Colors.Blue;

            MapControl1.Routes.Add(routeView);
            //await MapControl1.TrySetViewBoundsAsync(routeResult.Route.BoundingBox,
            //    null, MapAnimationKind.None);
        }

        public void SetNewPosition(Geoposition geoPosition)
        {
            CurrentPosition.Latitude = geoPosition.Coordinate.Point.Position.Latitude;
            CurrentPosition.Longitude = geoPosition.Coordinate.Point.Position.Longitude;
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>{
            currentPosIcon.Location = new Geopoint(CurrentPosition);
            if (!_scrolled && _doneMoving)
            {
                SmoothSetPosition(currentPosIcon.Location);
                _doneMoving = !_doneMoving;
                if (MapControl1.Routes.Count != 0 && initRouteDone)
                {
                    InitStartToRoute();
                  //  MapControl1.UpdateLayout();  
                }
                //MapControl1.Center = new Geopoint(CurrentPosition);
            }
            });
            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
        }

        private async Task SmoothSetPosition(Geopoint pos)
        {
            await MapControl1.TrySetViewAsync(pos, MapControl1.ZoomLevel);
            _doneMoving = !_doneMoving;
        }

        private void Bn_Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.MenuPage), e);
        }

        private void Bn_Loc_Click(
            object sender, RoutedEventArgs e)
        {
            _scrolled = !_scrolled;
            
            if(_scrolled)
            {
                Bn_Loc.Background = new SolidColorBrush(Colors.White);
                mapDisable.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                Bn_Loc.Background = new SolidColorBrush(Colors.Blue);
                mapDisable.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            //if (geo == null)
            //{
            //    geo = new Geolocator();
            //}
            //Geoposition pos = await geo.GetGeopositionAsync();
            //AddCurrentPositionIcon(pos.Coordinate.Point.Position.Latitude, pos.Coordinate.Point.Position.Longitude);
        }   

        private void MapScrolled(object sender, RoutedEventArgs e)
        {
            _scrolled = true;
        }

        private void MapScrolled(MapControl sender, object args)
        {
            _scrolled = true;
        }

        private void MapScrolled(object sender, PointerRoutedEventArgs e)
        {
            _scrolled = true;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}

        private void CreateGeofence(double latitude, double longitude, double radius)
        {
                var id = string.Format("Posisition: {0}, {1}", latitude, longitude);
            // Sets the center of the Geofence.
            var position = new BasicGeoposition
            {
                Latitude = latitude,
                Longitude = longitude
            };

            // The Geofence is a circular area centered at (latitude, longitude) point, with the
            // radius in meter.
            var geocircle = new Geocircle(position, radius);

            // Sets the events that we want to handle: in this case, the entrace and the exit
            // from an area of intereset.
            var mask = MonitoredGeofenceStates.Entered | MonitoredGeofenceStates.Exited;

            // Specifies for how much time the user must have entered/exited the area before 
            // receiving the notification.
            var dwellTime = TimeSpan.FromSeconds(5);

            // Creates the Geofence and adds it to the GeofenceMonitor.
            var geofence = new Geofence(id, geocircle, mask, false, dwellTime);
            GeofenceMonitor.Current.Geofences.Clear();
                GeofenceMonitor.Current.Geofences.Add(geofence);
            
        }

        private async void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
        {
            var reports = sender.ReadReports();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                foreach (var report in reports)
                {
                    var state = report.NewState;
                    var geofence = report.Geofence;

                    if (state == GeofenceState.Entered)
                    {
                        // User has entered the area.
                        ShowMessage("you have entered the geofence, deleting point " + (MapControl1.Routes.Count-1));
                        MapControl1.Routes.RemoveAt(0);
                        _rc.GetCurrentRoute().getRoute().RemoveAt(0);
                        _currentAmountOfPoints--;
                    }
                    else if (state == GeofenceState.Exited)
                    {
                        // User has exited from the area.
                        //ShowMessage("you have exited the geofence");
                    }
                }
            });
        }
        async Task ShowMessage(string message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

    }
}
