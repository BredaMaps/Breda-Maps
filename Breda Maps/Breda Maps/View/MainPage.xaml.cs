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
using Breda_Maps.Controller.Enums;
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
        BasicGeoposition CurrentPosition = new BasicGeoposition()
        {
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
            //init();
            //CreateGeofence(StartPosition.Latitude, StartPosition.Longitude, 3, "me");
        }

        private void init()
        {
            Debug.WriteLine("init()");
            MapControl1.Center = new Geopoint(StartPosition);
            MapControl1.ZoomLevel = 18;
            MapControl1.LandmarksVisible = true;

            AddCurrentPositionIcon();
            addAllIconPoints();
            addCategoriePoints();
            Bn_Loc.Background = new SolidColorBrush(Colors.Blue);
            if (GetDistanceTo(CurrentPosition, 51.5938D, 4.77963D) > 7500)
            {
                ShowMessage("U bevind zich buiten Breda");
            }
            InitRoute();
        }

        public static int GetDistanceTo(BasicGeoposition one, Double CoordinateOne, Double CoordinateTwo)
        {
            BasicGeoposition two = new BasicGeoposition();
            two.Latitude = CoordinateOne;
            two.Longitude = CoordinateTwo;
            if (double.IsNaN(one.Latitude) || double.IsNaN(one.Longitude) || double.IsNaN(two.Latitude) || double.IsNaN(two.Longitude))
            {
                throw new ArgumentException(("Argument_LatitudeOrLongitudeIsNotANumber"));
            }
            else
            {
                double latitude = one.Latitude * 0.0174532925199433;
                double longitude = one.Longitude * 0.0174532925199433;
                double num = two.Latitude * 0.0174532925199433;
                double longitude1 = two.Longitude * 0.0174532925199433;
                double num1 = longitude1 - longitude;
                double num2 = num - latitude;
                double num3 = Math.Pow(Math.Sin(num2 / 2), 2) + Math.Cos(latitude) * Math.Cos(num) * Math.Pow(Math.Sin(num1 / 2), 2);
                double num4 = 2 * Math.Atan2(Math.Sqrt(num3), Math.Sqrt(1 - num3));
                double num5 = 6376500 * num4;
                int intdist = Convert.ToInt16(num5);
                return (RoundNum(intdist));
            }
        }

        public static int RoundNum(int num)
        {
            num = num * 2;
            int rem = num % 10;
            return (rem >= 5 ? (num - rem + 10) : (num - rem)) / 2;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            if (_rc.GetCurrentRoute() == null)
            {
                Debug.WriteLine("route geselecteerd");
                string routeName = e.Parameter as string;
                _rc.selectRoute(routeName);
                init();
            }
            else
            {
                //_rc.GetMap().Frame.Navigate();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }
        public void addCategoriePoints()
        {
            if (_rc.GetCategories() != null)
            {
                foreach (Sight points in _rc.GetCategories())
                {
                    MapIcon categorieIcon = new MapIcon();
                    categorieIcon.Location = points.getLocation();
                    categorieIcon.NormalizedAnchorPoint = new Point(1.5, 1.5);
                    categorieIcon.Title = points.getDescription();
                    MapControl1.MapElements.Add(categorieIcon);
                }
            }
        }

        public void addAllIconPoints()
        {
            GeofenceMonitor.Current.Geofences.Clear();
            if (_rc.GetIconLocations() != null)
            {
                foreach (Sight points in _rc.GetIconLocations())
                {
                    MapIcon Icon = new MapIcon();
                    Icon.Location = points.getLocation();
                    Icon.NormalizedAnchorPoint = new Point(1.5, 1.5);
                    Icon.Title = points.getDescription();
                    MapControl1.MapElements.Add(Icon);
                    CreateGeofence(points.latitude, points.longitude, 15, points._description);
                }
                CreateGeofence(51.5938D, 4.77963D, 7500, "bredaCheck");
                Debug.WriteLine("Geofences added");
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
            if (_rc.GetCurrentRoute() != null)
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
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
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

            if (_scrolled)
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

        private void CreateGeofence(double latitude, double longitude, double radius, string name)
        {
            var id = string.Format(name);
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
            //GeofenceMonitor.Current.Geofences.Clear();
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
                   // GeofenceMonitor.Current.Geofences.Remove(geofence);
                    Debug.WriteLine("geofenceid = " + geofence.Id);

                    if (geofence.Id.Equals("bredaCheck"))
                    {
                        Debug.WriteLine("werkt");
                        if (state == GeofenceState.Exited)
                        {
                            await ShowMessage("U begeeft zich buiten Breda");
                        }
                        else if (state == GeofenceState.Entered)
                        {
                            await ShowMessage("U bevind zich in breda");
                        }
                    }

                    if (state == GeofenceState.Entered && !geofence.Id.Equals("bredaCheck"))
                    {
                        // User has entered the area.
                        //ShowMessage("you have entered the geofence, deleting point " + (MapControl1.Routes.Count-1));
                        MapControl1.Routes.RemoveAt(0);
                        _rc.GetCurrentRoute().getRoute().RemoveAt(0);
                        _currentAmountOfPoints--;
                        this.Frame.Navigate(typeof(View.InformationPage), geofence.Id);
                        GeofenceMonitor.Current.Geofences.Remove(geofence);
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
