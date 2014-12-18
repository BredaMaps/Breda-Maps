﻿using Breda_Maps.Model;
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
    public sealed partial class RoutePage : GUI
    {
        List<Route> _routes;
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
            _routes = new List<Route>();
            //TextBox tb1 = new TextBox();
            //tb1.Text = "Test 1";
            //tb1.FontSize = 50;
            _routes.Add(new Route("Test 1"));
            _routes.Add(new Route("Test 2"));
            _routes.Add(new Route("Test 3"));
            _routes.Add(new Route("Test 4"));
            listView.FontSize = 50;
            foreach(Route r in _routes)
            {
                //tb1.Text = r.GetName();
                //listView
                listView.Items.Add(r.GetName());
            }
            //listView.FontSize = 50;
            Debug.WriteLine(listView.FontSize);
            //mainGrid.Children.Add(listView);
        }

        private void Bn_Cat_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.CategoryPage), e);
        }
        private void Bn_Sta_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.MainPage), e);
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Selection changed to: " + listView.SelectedItems[0]);
        }
    }
}
