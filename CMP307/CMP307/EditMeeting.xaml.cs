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

namespace CMP307
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditMeeting : Page
    {
        DatabaseRequest request;
        Person person;
        Meeting meeting;
        ObservableCollection<Room> rooms;
        ObservableCollection<Person> persons;

        public EditMeeting()
        {
            this.InitializeComponent();
            request = new DatabaseRequest();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MultipleParams multiple = (MultipleParams)e.Parameter; // get parameters passed in
            //Debug.WriteLine("Navigated!");

            // init objects
            person = multiple.GetPerson();
            meeting = multiple.GetMeeting();
            rooms = new ObservableCollection<Room>(request.GetRoomList());

            // set combobox to room the meeting is currently taking place in
            cmbEMRoom.ItemsSource = rooms;
            cmbEMRoom.SelectedItem = rooms[meeting.GetRoomID() - 1];

            // get list of people in the meeting
            persons = new ObservableCollection<Person>();
            List<Person> getPersonsID = request.GetMeetingUsers(meeting.GetID());
            foreach(Person p in getPersonsID)
            {
                persons.Add(p);
            }
            lstParticipants.ItemsSource = persons;

            // auto-fill fields
            txtEMName.Text = meeting.GetName();
            txtEMStartTime.Time = new TimeSpan(meeting.GetStart().Hour, meeting.GetStart().Minute, 0);
            txtEMEndTime.Time = new TimeSpan(meeting.GetEnd().Hour, meeting.GetEnd().Minute, 0);
        }

        // add input validation

        private void btnEMDelete_Click(object sender, RoutedEventArgs e)
        {
            if(request.DeleteMeeting(meeting.GetID()))
            {
                // code should never be reached, but is here as a precaution
                txtErr.Text = "Meeting Already Deleted!";
                txtErr.Visibility = Visibility.Visible;
            }
            else
            {
                ShowPopup("Meeting Deleted!", "Meeting Successfully Deleted!", "Ok");
                Frame.Navigate(typeof(Hub), person);
            }
        }

        private void btnEMCont_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEMName.Text) || cmbEMRoom.SelectedIndex <= -1)
            {
                // some fields have been left empty
                txtErr.Text = "Please Fill Out All Fields!";
                txtErr.Visibility = Visibility.Visible;
            }
            else
            {
                Room Selected = (Room)cmbEMRoom.SelectedItem;
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                    DateTime.Now.Day, txtEMStartTime.Time.Hours, txtEMStartTime.Time.Minutes, 0); // requested start time
                DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                    DateTime.Now.Day, txtEMEndTime.Time.Hours, txtEMEndTime.Time.Minutes, 0); // requested end time
                string Name = txtEMName.Text;

                InputValidation inputValidation = new InputValidation();
                string Validate = inputValidation.EditValidate(Name, Start, End, Selected, persons, person, meeting);

                if (string.IsNullOrEmpty(Validate))
                {
                    ShowPopup("Meeting Edited!", "Meeting Successfully Edited!", "Ok");
                    Frame.Navigate(typeof(Hub), person);
                }
                else
                {
                    txtErr.Text = Validate;
                    txtErr.Visibility = Visibility.Visible;
                }
            }
        }

        private async void ShowPopup(string pTitle, string pContent, string pButton)
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = pTitle,
                Content = pContent,
                CloseButtonText = pButton
            };

            await contentDialog.ShowAsync();
        }

        private void lstParticipants_ItemClick(object sender, ItemClickEventArgs e)
        {
            Person removeP = (Person)e.ClickedItem;
            persons.Remove(removeP);
        }

        private void btnEPInvite_Click(object sender, RoutedEventArgs e)
        {
            int getID = request.GetIdByUser(txtEPName.Text);
            if (getID == 0)
            {
                txtEPErr.Text = "Invalid Username!";
            }
            else if (persons.Count > 0 && persons.Any(p => p.GetID() == getID))
            {
                txtEPErr.Text = "User is Already Added!";
            }
            else if (getID == person.GetID())
            {
                txtEPErr.Text = "You Cannot Add Yourself!";
            }
            else
            {
                Person person = new Person(getID, txtEPName.Text);
                persons.Add(person);
                txtEPName.Text = String.Empty;
                txtEPErr.Text = "";
                txtEPHint.Text = "Click on Participant to Remove them";
            }
        }

        private void btnEMBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Hub), person);
        }
    }
}
