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

namespace CMP307.Admin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAdmin : Page
    {
        AdminDB Connection;

        public CreateAdmin()
        {
            this.InitializeComponent();
            Connection = new AdminDB();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminUser admin = new AdminUser(txtUsername.Text, txtPassword.Password);

            if(Connection.CreateAdmin(admin))
            {
                txtErr.Text = "Admin Account Created! Please Restart Application.";
                txtErr.Visibility = Visibility.Visible;
            }else
            {
                txtErr.Text = "Admin with this Name Account Already Exists!";
                txtErr.Visibility = Visibility.Visible;
            }
        }
    }
}
