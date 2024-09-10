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
    public sealed partial class AddRoom : Page
    {
        AdminUser a;
        AdminDB request;

        public AddRoom()
        {
            this.InitializeComponent();
            request = new AdminDB();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            a = (AdminUser)e.Parameter;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminRoomHub), a);
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            int parsedValue;
            Room r = new Room(txtName.Text);

            if (!int.TryParse(txtCapacity.Text, out parsedValue))
            {
                txtErr.Text = "Capacity Field May only Contain Integers!";
                txtErr.Visibility = Visibility.Visible;
            }else if(parsedValue <= 1) 
            {
                txtErr.Text = "Capacity Field Too Small!";
                txtErr.Visibility = Visibility.Visible;
            }
            else if (parsedValue > 64) // arbitrary number
            {
                txtErr.Text = "Capacity Field Too Large!";
                txtErr.Visibility = Visibility.Visible;
            }
            else if (txtName.Text.Length < 4)
            {
                txtErr.Text = "Room Name too Short!";
                txtErr.Visibility = Visibility.Visible;
            }
            else if (txtName.Text.Length > 128)
            {
                txtErr.Text = "Room Name too Long!";
                txtErr.Visibility = Visibility.Visible;
            }
            else
            {
                Room room = new Room(parsedValue, txtName.Text);

                if(request.CreateRoom(room))
                {
                    ShowPopup();
                    Frame.Navigate(typeof(AdminHub), a);
                }else
                {
                    txtErr.Text = "Room Name Already Taken!";
                    txtErr.Visibility = Visibility.Visible;
                }
            }
        }

        private async void ShowPopup()
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = "Room Created!",
                Content = "Room Successfully Created!",
                CloseButtonText = "Ok"
            };

            await contentDialog.ShowAsync();
        }
    }
}
