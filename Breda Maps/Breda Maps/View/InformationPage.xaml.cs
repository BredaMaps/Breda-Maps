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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Breda_Maps.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InformationPage : GUI
    {
        public InformationPage()
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
            string itemName = e.Parameter as string;
            ItemName.Text = itemName;
            setImage("Assets/Logo.scale-240.png");
        }

        public void setImage(String imagepath) //Assets/Logo.scale-240.png"
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("ms-appx:///" + imagepath));
            mainImage.Source = img.Source;
            //mainImage.Source = image;
        }
        public void SetInformation(String information)
        {
            mainInformation.Text = information;
        }

        private void Sight_Sel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.SightPage), e);
        }
    }
}
