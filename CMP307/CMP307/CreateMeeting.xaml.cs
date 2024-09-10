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
    public sealed partial class CreateMeeting : Page
    {
        int ID;

        Person p;
        DatabaseRequest request;
        ObservableCollection<Room> rooms;
        ObservableCollection<Person> persons;
        //List<Room> rooms;

        public CreateMeeting()
        {
            this.InitializeComponent();

            request = new DatabaseRequest();
            rooms = new ObservableCollection<Room>(request.GetRoomList());
            persons = new ObservableCollection<Person>();
            
            /*
            rooms = request.GetRoomList();

            foreach(Room r in rooms)
            {
                cmbMRoom.Items.Add(r.GetName());
            }
            */

            txtMStartTime.Time = new TimeSpan(DateTime.Now.Hour + 1, 0, 0);
            txtMEndTime.Time = new TimeSpan(DateTime.Now.Hour + 2, 0, 0);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            p = (Person)e.Parameter;
            ID = p.GetID();
        }

        private void btnMCont_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMName.Text) || cmbMRoom.SelectedIndex <= -1)
            {
                // some fields have been left empty
                txtErr.Text = "Please Fill Out All Fields!";
                txtErr.Visibility = Visibility.Visible;
            }else
            {
                Room Selected = (Room)cmbMRoom.SelectedItem;
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                    DateTime.Now.Day, txtMStartTime.Time.Hours, txtMStartTime.Time.Minutes, 0); // requested start time
                DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                    DateTime.Now.Day, txtMEndTime.Time.Hours, txtMEndTime.Time.Minutes, 0); // requested end time
                string Name = txtMName.Text;

                InputValidation inputValidation = new InputValidation();
                string Validate = inputValidation.Validate(Name, Start, End, Selected, persons, p);

                if (string.IsNullOrEmpty(Validate))
                {
                    ShowPopup();
                    Frame.Navigate(typeof(Hub), p);
                }else
                {
                    txtErr.Text = Validate;
                    txtErr.Visibility = Visibility.Visible;
                }
            }

            /*
            if (InputValidation() == true)
            {
                ShowPopup();
                Frame.Navigate(typeof(Hub), p);
            }
            */
        }

        /*
        private bool InputValidation()
        {
            if(string.IsNullOrEmpty(txtMName.Text) || cmbMRoom.SelectedIndex <= -1)
            {
                // some fields have been left empty
                txtErr.Text = "Please Fill Out All Fields!";
                txtErr.Visibility = Visibility.Visible;
                return false;
            }else
            {
                Room Selected = (Room)cmbMRoom.SelectedItem;
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 
                    DateTime.Now.Day, txtMStartTime.Time.Hours, txtMStartTime.Time.Minutes, 0); // requested start time
                DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                    DateTime.Now.Day, txtMEndTime.Time.Hours, txtMEndTime.Time.Minutes, 0); // requested end time
                Meeting meeting = new Meeting(Selected.GetID(), txtMName.Text.ToString(), Start, End);
                DateTime Now = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                    DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0); // current time

                if (Start > End)
                {
                    // the start time must be earlier than the end time
                    txtErr.Text = "Start Time Cannot be After End Time!";
                    txtErr.Visibility = Visibility.Visible;
                    return false;
                }else if (Start == End) {
                    // the start and end time must be different
                    txtErr.Text = "Meeting Cannot Start and End at the Same Time!";
                    txtErr.Visibility = Visibility.Visible;
                    return false;
                }else if(Start < Now)
                {
                    // because these meetings are ad hoc, they cannot start at a time which is earlier than the current time
                    txtErr.Text = "Meeting Must Start Later in the Day!";
                    txtErr.Visibility = Visibility.Visible;
                    return false;
                }else if(Selected.GetCapacity() < persons.Count-1) // list is decremented by 1 to take into account the person making the meeting
                {
                    // meeting room capacity must be adhered to
                    Debug.WriteLine(Selected.GetCapacity());
                    Debug.WriteLine(persons.Count - 1);
                    txtErr.Text = "Too Many Participants!";
                    txtErr.Visibility = Visibility.Visible;
                    return false;
                }
                else if (!request.IsRoomVacant(Selected.GetID(), Start, End))
                {
                    // the meeting cannot happen unless the requested room is vacant
                    txtErr.Text = "Room Is Occupied During This Time!";
                    txtErr.Visibility = Visibility.Visible;
                    return false;
                }else if (txtMName.Text.Length < 8 || txtMName.Text.Length > 128)
                {
                    // the room name must be between 8 and 128 characters
                    txtErr.Text = "Please Enter A Valid Meeting Name!";
                    txtErr.Visibility = Visibility.Visible;
                    return false;
                }else if(persons.Count < 1)
                {
                    // the meeting must have more than 1 member attending
                    txtErr.Text = "Invite List Empty!";
                    txtErr.Visibility = Visibility.Visible;
                    return false;
                }
                else
                {
                    int meetingID = request.CreateMeeting(meeting, p);
                    if (meetingID <= -1)
                    {
                        // unexpected error; it is expected that this code should never be reached
                        txtErr.Text = "Something Went Wrong, Please Contact an Admin!";
                        txtErr.Visibility = Visibility.Visible;
                        return false;
                    }
                    else
                    {
                        // add each person in the collection to the meeting
                        foreach(Person p in persons)
                        {
                            request.AddToMeeting(p.GetID(), meetingID);
                        }
                        return true;
                    }
                }
            }
        }
        */

        private void btnPInvite_Click(object sender, RoutedEventArgs e)
        {
            int getID = request.GetIdByUser(txtPName.Text);
            Debug.WriteLine(getID);
            if (getID == 0)
            {
                txtPErr.Text = "Invalid Username!";
            }
            else if(persons.Count > 0 && persons.Any(p => p.GetID() == getID))
            {
                txtPErr.Text = "User is Already Added!";
            }
            else if(getID == ID)
            {
                txtPErr.Text = "You Cannot Add Yourself!";
            }else
            {
                Person person = new Person(getID, txtPName.Text);
                persons.Add(person);
                txtPName.Text = String.Empty;
                txtPErr.Text = "";
                txtPHint.Text = "Click on Participant to Remove them";
            }
        }

        private async void ShowPopup()
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = "Meeting Created!",
                Content = "Meeting Successfully Created!",
                CloseButtonText = "Ok"
            };

            await contentDialog.ShowAsync();
        }

        private void lstParticipants_ItemClick(object sender, ItemClickEventArgs e)
        {
            Person removeP = (Person)e.ClickedItem;
            persons.Remove(removeP);
        }

        private void btnMBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Hub), p);
        }
    }
}
