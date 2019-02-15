using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// Interaction logic for CreateReservation.xaml
    /// This implementation is currently designed towards an employee creating a Reservation as opposed to a prospective Member creating a Reservation
    /// </summary>
    public partial class CreateReservation : Window
    {
        private List<Member> _members;
        IReservationManager _reservationManager;
        IMemberManager _memberManager;
        Reservation _existingReservation;

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// This constructor is used for Creating a Reservation
        /// Initializes connections to our ReservationManager and MemberManager
        /// Populates Member Combobox and displays any errors
        /// </summary>
        public CreateReservation(IReservationManager reservationManager)
        {
            InitializeComponent();
            _reservationManager = reservationManager;
            _memberManager = new MemberManagerMSSQL();
            try
            {
                _members = _memberManager.RetrieveAllMembers();
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
            cboMembers.ItemsSource = _members;
            chkActive.Visibility = Visibility.Hidden;
            chkActive.IsChecked = true;
            _existingReservation = null;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/26/2019
        /// This constructor is used for Reading and Updating a Reservation
        /// </summary>
        /// <param name="existingReservation">
        /// A Reservation which already exists, presumably obtained from a list of Reservations
        /// </param>
        public CreateReservation(Reservation existingReservation, IReservationManager reservationManager)
        {
            InitializeComponent();
            _reservationManager = reservationManager;
            _memberManager = new MemberManagerMSSQL();
            try
            {
                _members = _memberManager.RetrieveAllMembers();
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
            cboMembers.ItemsSource = _members;
            _existingReservation = existingReservation;
            populateFormReadOnly();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// populates all the fields for the form 
        /// </summary>
        private void populateFormReadOnly()
        {
            txtNumGuests.Text = ""+ _existingReservation.NumberOfGuests;
            txtNumPets.Text = ""+_existingReservation.NumberOfPets;
            txtNotes.Text = _existingReservation.Notes;
            dtpArrivalDate.Text = _existingReservation.ArrivalDate.ToString("MM/dd/yyyy");
            dtpDepartureDate.Text = _existingReservation.DepartureDate.ToString("MM/dd/yyyy");
            cboMembers.SelectedItem = _members.Find(x=>x.MemberID == _existingReservation.MemberID);
            chkActive.IsChecked = _existingReservation.Active;
            setReadOnly();
            btnSave.Content = "Update";
                    }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Sets the form to read only and hides the buttons which cannot be used in read only mode
        /// </summary>
        private void setReadOnly()
        {
            txtNumGuests.IsReadOnly = true;
            txtNumPets.IsReadOnly = true;
            txtNotes.IsReadOnly = true;
            dtpArrivalDate.IsEnabled = false;
            dtpDepartureDate.IsEnabled = false;
            cboMembers.IsEnabled = false;
            btnAddNewMember.Visibility = Visibility.Hidden;
            chkActive.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Sets the form to editable and shows the buttons which may need to be used in create/edit mode
        /// </summary>
        private void setEditable()
        {
            txtNumGuests.IsReadOnly = false;
            txtNumPets.IsReadOnly = false;
            txtNotes.IsReadOnly = false;
            dtpArrivalDate.IsEnabled = true;
            dtpDepartureDate.IsEnabled = true;
            cboMembers.IsEnabled = true;
            btnSave.Content = "Submit";
            chkActive.Visibility = Visibility.Visible;
            btnAddNewMember.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when Save is clicked
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (((string)btnSave.Content) == "Submit")
            {
                if (!ValidateInput())
                {
                    return;
                }
                Reservation newReservation = new Reservation
                {
                    MemberID = ((Member)cboMembers.SelectedItem).MemberID,      // Validated
                    NumberOfGuests = int.Parse(txtNumGuests.Text),              // Validated
                    NumberOfPets = int.Parse(txtNumPets.Text),                  // Validated
                    ArrivalDate = DateTime.Parse(dtpArrivalDate.Text),          // Validated
                    DepartureDate = DateTime.Parse(dtpDepartureDate.Text),      // Validated
                    Notes = txtNotes.Text                                       // Optional
                };
                try
                {
                    if (_existingReservation == null)
                    {
                        _reservationManager.AddReservation(newReservation);
                        SetError("");
                        MessageBox.Show("Reservation Created Successfully: " +
                        "\nMemberID: " + newReservation.MemberID +
                        "\nNumberOfGuests: " + newReservation.NumberOfGuests +
                        "\nNumberOfPets: " + newReservation.NumberOfPets +
                        "\nArrivalDate: " + newReservation.ArrivalDate.ToString("MM/dd/yyyy") +
                        "\nDepartureDate: " + newReservation.DepartureDate.ToString("MM/dd/yyyy") +
                        "\nNotes: " + newReservation.Notes);
                    }
                    else
                    {
                        newReservation.Active = (bool)chkActive.IsChecked;
                        _reservationManager.EditReservation(_existingReservation, newReservation);
                        SetError("");
                        MessageBox.Show("Reservation Updated Successfully: " +
                        "\nOld MemberID: " + _existingReservation.MemberID +
                        "\nOld NumberOfGuests: " + _existingReservation.NumberOfGuests +
                        "\nOld NumberOfPets: " + _existingReservation.NumberOfPets +
                        "\nOld ArrivalDate: " + _existingReservation.ArrivalDate.ToString("MM/dd/yyyy") +
                        "\nOld DepartureDate: " + _existingReservation.DepartureDate.ToString("MM/dd/yyyy") +
                        "\nOld Notes: " + _existingReservation.Notes +
                        "\nNew MemberID: " + newReservation.MemberID +
                        "\nNew NumberOfGuests: " + newReservation.NumberOfGuests +
                        "\nNew NumberOfPets: " + newReservation.NumberOfPets +
                        "\nNew ArrivalDate: " + newReservation.ArrivalDate.ToString("MM/dd/yyyy") +
                        "\nNew DepartureDate: " + newReservation.DepartureDate.ToString("MM/dd/yyyy") +
                        "\nNew Notes: " + newReservation.Notes);
                    }
                }
                catch (Exception ex)
                {
                    SetError(ex.Message);
                }

                Close();
            }
            else if(((string)btnSave.Content)=="Update")
            {
                setEditable();
            }
            else
            {
                MessageBox.Show(btnSave.Content.GetType() +" "+btnSave.Content);
            }
            
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Sets and displays the error to be showed on screen
        /// </summary>
        /// <param name="error">error is a string containing an error message to be displayed</param>
        private void SetError(string error)
        {
            lblError.Content = error;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// A checklist which validates all the input in the form
        /// </summary>
        /// <returns>returns true if all the input is valid. false otherwise</returns>
        private bool ValidateInput()
        {
            if (ValidateMember())
            {
                if (ValidateNumberOfGuests())
                {
                    if (ValidateNumberOfPets())
                    {
                        if (ValidateArrivalDate())
                        {
                            if (ValidateDepartureDate()) //Test Departure Date after Arrival Date since Departure Date relies on Arrival Date
                            {
                                return true;
                            }
                            else
                            {
                                SetError("INVALID DEPARTURE DATE");
                            }
                        }
                        else
                        {
                            SetError("INVALID ARRIVAL DATE");
                        }
                    }
                    else
                    {
                        SetError("PETS MUST BE AN INTEGER BETWEEN 0 AND 100");
                    }
                }
                else
                {
                    SetError("GUESTS MUST BE AN INTEGER BETWEEN 1 AND 100");
                }
            }
            else
            {
                SetError("INVALID MEMBER");
            }
            return false;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Checks whether the departure date is valid
        /// </summary>
        /// <returns>returns true if the departure date is valid. false otherwise</returns>
        private bool ValidateDepartureDate()
        {
            //Departure Date cannot be null
            if (dtpDepartureDate.Text == null || dtpDepartureDate.Text == "")
            {
                //Date is invalid
                return false;
            }
            //Departure Date cannot be prior to or equal to Arrival Date
            DateTime ArrivalDate = DateTime.Parse(dtpArrivalDate.Text);
            DateTime DepartureDate = DateTime.Parse(dtpDepartureDate.Text);
            //Departure Date must be after today ?????
            if (ArrivalDate.Date < DepartureDate.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Checks whether the arrival date is valid
        /// </summary>
        /// <returns>returns true if the arrival date is valid. false otherwise</returns>
        private bool ValidateArrivalDate()
        {
            //Arrival Date cannot be null
            if (dtpArrivalDate.Text == null || dtpArrivalDate.Text == "")
            {
                //Date is invalid
                return false;
            }
            /*
            DateTime ArrivalDate = DateTime.Parse(dtpArrivalDate.Text);

            
            DateTime today = DateTime.Today;
            //Arrival Date cannot be prior to current Day
            if (ArrivalDate.Date < today)
            {
                //It is earlier than today
                return false;
            }
            else
            {
                //It is equal or later than today
                return true;
            }*/
            return true;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Checks whether the Member is valid. At the moment it only returns true
        /// </summary>
        /// <returns>returns true if the member is valid. false otherwise</returns>
        private bool ValidateMember()
        {
            //Call IMemberManager.ValidateMember(MemberID)
            //throw new NotImplementedException();
            if (!(cboMembers.SelectedIndex >= 0))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Checks whether the Number Of Guests is valid. At the moment it only returns true
        /// </summary>
        /// <returns>returns true if the Number Of Guests is valid. false otherwise</returns>
        private bool ValidateNumberOfGuests()
        {
            if (txtNumGuests.Text == null || txtNumGuests.Text == "")
            {
                return false;
            }

            //Chose a range of 1-100 Guests. Can be changed as needed
            if (int.Parse(txtNumGuests.Text) >= 1 && int.Parse(txtNumGuests.Text) <= 100)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Checks whether the Number Of Pets is valid. At the moment it only returns true
        /// </summary>
        /// <returns>returns true if the Number Of Pets is valid. false otherwise</returns>
        private bool ValidateNumberOfPets()
        {
            if (txtNumPets.Text == null || txtNumPets.Text == "")
            {
                return false;
            }

            //Chose a range of 0-100 Pets. Can be changed as needed
            if (int.Parse(txtNumPets.Text) >= 0 && int.Parse(txtNumPets.Text) <= 100)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when Cancel is clicked
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when Add Member is clicked
        /// </summary>
        private void btnAddNewMember_Click(object sender, RoutedEventArgs e)
        {
            //Launch the createNewMember window when it is done
            MessageBox.Show("This feature is still in production");
        }
    }
}
