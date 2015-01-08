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
    public sealed partial class AboutPage : GUI
    {
        private string aboutString =
            "Breda Maps\n" +
            "Versie 1.0\n\n" +
            "In opdracht van:\n" +
            "Adaptive Guiding Systems (AGS) voor VVV Breda\n\n" +
            "Geprogrammeerd door:\n" +
            "Rudy Tjin-Kon-Koen\n" +
            "Sander Nagtzaam\n" +
            "Bart Groffen\n" +
            "Kevin van den Akkerveken\n" +
            "Gerjan Holsappel\n" +
            "Corné Derijck\n\n" +
            "Systeem gedefinieerd door:\n" +
            "Joris Martens\n" +
            "Jeroen van den Bergh\n" +
            "Joost Mutsaers\n" +
            "David Sterkenburg\n" +
            "Stefan Antonissen\n" +
            "Dennis Ping\n\n" +
            "Innovatie door:\n" +
            "Thomas Zaman\n" +
            "Jerry Lotens\n" +
            "Justin Brouwer\n" +
            "Jasper van Megroot\n" +
            "Jeroen den Hollander\n" +
            "Remco van der Aa\n\n";


        public AboutPage()
        {
            this.InitializeComponent();
            this.aboutTextBlock.Text = aboutString;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
