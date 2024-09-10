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
    public sealed partial class EditRoom : Page
    {
        AdminUser a;
        Room r;
        AdminDB request;
        DatabaseRequest uRequest;
        int parsedValue;

        public EditRoom()
        {
            this.InitializeComponent();
            request = new AdminDB();
            uRequest = new DatabaseRequest();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MultipleParams multiple = (MultipleParams)e.Parameter; // get parameters passed in
            //Debug.WriteLine("Navigated!");

            // init objects
            a = multiple.GetAdmin();
            r = multiple.GetRoom();
            txtCurrentName.Text = r.GetName();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtNewName.Text) && string.IsNullOrEmpty(txtCapacity.Text))
            {
                txtErr.Text = "One of the Two Update Fields Must Have Input!";
                txtErr.Visibility = Visibility.Visible;
            }else if(string.IsNullOrEmpty(r.GetName()))
            {
                txtErr.Text = "There Must Be a Room to Update!";
                txtErr.Visibility = Visibility.Visible;
            }else if(txtNewName.Text.Equals(r.GetName()))
            {
                txtErr.Text = "New Room Name and Current Room Name Cannot be the Same!";
                txtErr.Visibility = Visibility.Visible;
            }
            else
            {
                if(!string.IsNullOrEmpty(txtNewName.Text) && !string.IsNullOrEmpty(txtCapacity.Text))
                {
                    if (ValidateCapacity(txtCapacity.Text) && ValidateUsername(txtNewName.Text))
                    {
                        if(request.UpdateRoomBoth(txtNewName.Text, parsedValue, r.GetName()))
                        {
                            ShowPopup();
                            Frame.Navigate(typeof(AdminHub), a);
                        }
                        else
                        {
                            txtErr.Text = "Something Has Gone Wrong. Please Contact an Admin!";
                            txtErr.Visibility = Visibility.Visible;
                        }
                    }
                }else if(!string.IsNullOrEmpty(txtNewName.Text))
                {
                    if (ValidateUsername(txtNewName.Text))
                    {
                        if(request.UpdateRoomName(txtNewName.Text, txtCurrentName.Text))
                        {
                            ShowPopup();
                            Frame.Navigate(typeof(AdminHub), a);
                        }
                        else
                        {
                            txtErr.Text = "Something Has Gone Wrong. Please Contact an Admin!";
                            txtErr.Visibility = Visibility.Visible;
                        }
                    }
                }
                else
                {
                    if (ValidateCapacity(txtCapacity.Text))
                    {
                        if(request.UpdateRoomCap(parsedValue, txtCurrentName.Text))
                        {
                            ShowPopup();
                            Frame.Navigate(typeof(AdminHub), a);
                        }
                        else
                        {
                            txtErr.Text = "Something Has Gone Wrong. Please Contact an Admin!";
                            txtErr.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private bool ValidateUsername(string username)
        {
            if (txtNewName.Text.Length < 4)
            {
                txtErr.Text = "Room Name too Short!";
                txtErr.Visibility = Visibility.Visible;
                return false;
            }
            else if (txtNewName.Text.Length > 128)
            {
                txtErr.Text = "Room Name too Long!";
                txtErr.Visibility = Visibility.Visible;
                return false;
            }else
            {
                return true;
            }
        }

        private bool ValidateCapacity(string capacity)
        {
            if (!int.TryParse(txtCapacity.Text, out parsedValue))
            {
                txtErr.Text = "Capacity Field May only Contain Integers!";
                txtErr.Visibility = Visibility.Visible;
                return false;
            }
            else if (parsedValue <= 1)
            {
                txtErr.Text = "Capacity Field Too Small!";
                txtErr.Visibility = Visibility.Visible;
                return false;
            }
            else if (parsedValue > 64) // arbitrary number
            {
                txtErr.Text = "Capacity Field Too Large!";
                txtErr.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                return true;
            }
        }

        private async void ShowPopup()
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = "Room Updated!",
                Content = "Room Successfully Updated!",
                CloseButtonText = "Ok"
            };

            await contentDialog.ShowAsync();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditRoomList), a);
        }
    }
}
