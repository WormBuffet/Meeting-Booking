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
using MeetingLib;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CMP307.Admin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminHub : Page
    {
        AdminUser a;

        public AdminHub()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            a = (AdminUser)e.Parameter;
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminUserHub), a);
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminRoomHub), a);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminLogin));
        }
    }
}
