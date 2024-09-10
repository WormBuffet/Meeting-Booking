using MeetingLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CMP307.Admin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditRoomList : Page
    {
        AdminUser a;
        AdminDB request;
        DatabaseRequest uRequest;
        ObservableCollection<Room> rooms;

        public EditRoomList()
        {
            this.InitializeComponent();
            request = new AdminDB();
            uRequest = new DatabaseRequest();
            rooms = new ObservableCollection<Room>(uRequest.GetRoomList());
            lstEdit.ItemsSource = rooms;

            Debug.WriteLine(rooms[0].GetID());
            Debug.WriteLine(rooms[1].GetID());
            Debug.WriteLine(rooms[2].GetID());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            a = (AdminUser)e.Parameter;
            FillList();
        }

        private void FillList()
        {
            if (rooms.Count < 1)
            {
                txtNotif.Text = "There are Currently no Meeting Rooms! :(";
            }
            else
            {
                txtNotif.Text = "Tap on a Meeting to Edit It!";
            }
        }

        private void lstEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Room clicked = (Room)e.ClickedItem;
            MultipleParams m = new MultipleParams(a, clicked);
            this.Frame.Navigate(typeof(EditRoom), m);
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminHub), a);
        }
    }
}
