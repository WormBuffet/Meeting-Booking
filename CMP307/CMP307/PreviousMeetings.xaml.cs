using MeetingLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class PreviousMeetings : Page
    {
        Person p;
        DatabaseRequest request;
        ObservableCollection<Meeting> meetings;
        List<Meeting> meetingList;

        public PreviousMeetings()
        {
            this.InitializeComponent();
            request = new DatabaseRequest();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            p = (Person)e.Parameter;
            FillList();
        }

        private void FillList()
        {
            meetingList = request.GetActiveMeetingList(p.GetID());
            //Debug.WriteLine(meetingList.Count);

            foreach (Meeting m in meetingList.ToList())
            {
                if (m.GetStart() > DateTime.Now)
                {
                    meetingList.Remove(m);
                }
            }
            meetings = new ObservableCollection<Meeting>(meetingList);

            if (meetings.Count < 1)
            {
                txtNotif.Text = "No Previous Meetings! :(";
            }
            else
            {
                txtNotif.Text = "These Meetings Have Already Happened";
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Hub), p);
        }
    }
}
