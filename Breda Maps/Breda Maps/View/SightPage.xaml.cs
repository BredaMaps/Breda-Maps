using System;
using System.Collections.Generic;
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
    public sealed partial class SightPage : GUI
    {
        public SightPage()
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
            ListView1.Items.Add("1");
            
        }

        private void Cat2_Checked(object sender, RoutedEventArgs e)
        {
            ListView1.Items.Add("23");
        }

        private void Cat3_Checked(object sender, RoutedEventArgs e)
        {
            ListView1.Items.Add("2");
            ListView1.Items.Add("4");
        }

        private void cat1_unchecked(object sender, RoutedEventArgs e)
        {
            ListView1.Items.Clear();
            // clear the list view corresponding with this checkbox
        }
        private void cat2_unchecked(object sender, RoutedEventArgs e)
        {
            ListView1.Items.Clear();
            
        }
        private void cat3_unchecked(object sender, RoutedEventArgs e)
        {
            ListView1.Items.Clear();
            //ListView1.Items.Remove();
        }
    }
}
