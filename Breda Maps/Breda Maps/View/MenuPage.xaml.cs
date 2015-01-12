using Breda_Maps.Common;
using Breda_Maps.Model;
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
            //HardwareButtons.BackPressed += HardwareButtons_BackPressed;
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

        private void Bn_Rou_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.RoutePage), e);
        }

        private void Bn_Bez_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.SightPage), e);
        }

        private void Bn_Abt_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.AboutPage), e);
        }

        private void Bn_Afs_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
