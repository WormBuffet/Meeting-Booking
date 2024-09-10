using MeetingLib;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CMP307
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DatabaseRequest db = new DatabaseRequest();

            if (db.UserLogin(txtUsername.Text, txtPassword.Password))
            {
                int id = db.GetIdByUser(txtUsername.Text);
                //Frame.Navigate(typeof(CreateMeeting), new Person(id, txtUsername.Text));
                Frame.Navigate(typeof(Hub), new Person(id, txtUsername.Text));
            }
            else
            {
                txtErr.Visibility = Visibility.Visible;
            }
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Admin.AdminLogin));
            //Frame.Navigate(typeof(Admin.CreateAdmin));
        }
    }
}
