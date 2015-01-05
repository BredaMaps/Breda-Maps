using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Breda_Maps.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : GUI
    {
        public MenuPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            try
            {
                Frame.GoBack();
                e.Handled = true;
            }
            catch
            {
                Application.Current.Exit();
            }
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Bn_Rou_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GOTO MainPage");
            this.Frame.Navigate(typeof(View.RoutePage), e);
        }

        private void Bn_Bez_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GOTO SightPage");
            this.Frame.Navigate(typeof(View.SightPage), e);
        }

        private void Bn_Ins_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GOTO SettingsPage");
            this.Frame.Navigate(typeof(View.SettingsPage), e);
        }

        private void Bn_Abt_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GOTO AboutPage");
            this.Frame.Navigate(typeof(View.AboutPage), e);
        }

        private void Bn_Afs_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("EXIT");
            Application.Current.Exit();
        }
    }
}
