using MeetingLib;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CMP307
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Hub : Page
    {
        Person p;

        public Hub()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            p = (Person)e.Parameter;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateMeeting), p);
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddedMeetings), p);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewMeetings), p);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PreviousMeetings), p);
        }
    }
}
