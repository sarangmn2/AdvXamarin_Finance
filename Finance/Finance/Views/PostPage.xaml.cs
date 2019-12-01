using Finance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Finance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostPage : ContentPage
    {
        public PostPage()
        {
            InitializeComponent();
            // for ios
            /*
             * This iOS platform-specific is used to ensure that page content is 
             * positioned on an area of the screen that is safe for all devices 
             * that use iOS 11 and greater. Specifically, it will help to make sure 
             * that content isn't clipped by rounded device corners, the home indicator, 
             * or the sensor housing on an iPhone X. It's consumed in XAML by 
             * setting the Page.UseSafeArea attached property to a boolean value:
             */
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
        }
        public PostPage(Item item)
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
            webView.Source = item.ItemLink;
        }
    }
}