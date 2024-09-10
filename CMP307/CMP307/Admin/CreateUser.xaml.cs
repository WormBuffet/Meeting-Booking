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
    public sealed partial class CreateUser : Page
    {
        AdminUser a;
        AdminDB request;

        public CreateUser()
        {
            this.InitializeComponent();
            request = new AdminDB();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            a = (AdminUser)e.Parameter;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Person p = new Person(txtUsername.Text, txtPassword.Password);

            if(txtUsername.Text.Length < 4 || txtPassword.Password.Length < 4)
            {
                txtErr.Text = "Username and/or Password too Short!";
                txtErr.Visibility = Visibility.Visible;
            }
            else if(txtUsername.Text.Length > 64 || txtPassword.Password.Length > 64)
            {
                txtErr.Text = "Username and/or Password too Long!";
                txtErr.Visibility = Visibility.Visible;
            }
            else if(request.CreatePerson(p))
            {
                ShowPopup();
                Frame.Navigate(typeof(AdminHub), a);
            }
            else
            {
                txtErr.Text = "Username Already Taken!";
                txtErr.Visibility = Visibility.Visible;
            }
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminUserHub), a);
        }

        private async void ShowPopup()
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = "User Created!",
                Content = "User Successfully Created!",
                CloseButtonText = "Ok"
            };

            await contentDialog.ShowAsync();
        }
    }
}
