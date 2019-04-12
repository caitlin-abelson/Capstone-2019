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

        private List<Member> _currentMembers;
        Member _selectedMember = new Member();



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
            //txtMembers.ItemsSource = _members;
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
            //txtMembers.ItemsSource = _members;
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
            txtNumGuests.Text = "" + _existingReservation.NumberOfGuests;
            txtNumPets.Text = "" + _existingReservation.NumberOfPets;
            txtNotes.Text = _existingReservation.Notes;
            dtpArrivalDate.Text = _existingReservation.ArrivalDate.ToString("MM/dd/yyyy");
            dtpDepartureDate.Text = _existingReservation.DepartureDate.ToString("MM/dd/yyyy");
            //txtMembers.SelectedItem = _members.Find(x=>x.MemberID == _existingReservation.MemberID);
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
            txtMembers.IsEnabled = false;
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
            txtMembers.IsReadOnly = false;
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
                    //MemberID = ((Member)txtMembers.SelectedItem).MemberID,      // Validated
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
            else if (((string)btnSave.Content) == "Update")
            {
                setEditable();
            }
            else
            {
                MessageBox.Show(btnSave.Content.GetType() + " " + btnSave.Content);
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
            // if (!(txtMembers.SelectedIndex >= 0))
            // {
            // return false;
            //}
            //return true;
            if (txtMembers.Text == null || txtMembers.Text == "")
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
        /// <summary>
        /// @Author: Phillip Hansen
		/// Modification : Gunardi Saputra
        /// Modified: 04/11/2019
        /// Button grabs text from the text box, displays a window that:
        /// 1) Opens a window with a grid for Members
        /// 2) Grid is filtered with the input from the textbox and that only
        /// 3) If the requested member exists, or is created if not, the member should be selected and make the fields valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNewMember_Click(object sender, RoutedEventArgs e)
        {

            if (txtMembers != null)
            {
                 FilterMembers();

                var form = new ViewAccount(txtMembers.Text);
                var result = form.ShowDialog();

                if(result == true)
                {
                    var member = form._selectedMember;
                    //populate fields on this form
                }

            }

        }// end buttonAdd

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created: 04/11/2019
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMembers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BrowseMemberDoOnStart();
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>
        private void dgMember_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {


            string headerName = e.Column.Header.ToString();

            if (headerName == "FirstName")
            {
                e.Cancel = true;
            }
            if (headerName == "LastName")
            {
                e.Cancel = true;
            }
            if (headerName == "PhoneNumber")
            {
                e.Cancel = true;
            }
            if (headerName == "Email")
            {
                e.Cancel = true;
            }
            if (headerName == "Password")
            {
                e.Cancel = true;
            }



        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Modification: Gunardi Saputra
        /// Created: 04/11/2019
        /// </summary>
        private void BrowseMemberDoOnStart()
        {
            _memberManager = new MemberManagerMSSQL();
            _selectedMember = new Member();
            populateMembers();
            var window = new ViewAccount();
            window.ShowDialog();
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// When user clicks cancel reload the grids
        /// </summary>
        private void btnFilterBrowseMembers_Click(object sender, RoutedEventArgs e)
        {
            FilterMembers();
        }


        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// Populate the members
        /// </summary>
        private void populateMembers()
        {
            try
            {

                _members = _memberManager.RetrieveAllMembers();

                if (_currentMembers == null)
                {
                    _currentMembers = _members;
                }
                dgMember.ItemsSource = _currentMembers;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// when click on add member a new empty form will displays
        /// </summary>


        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// Filters search for the first name of the member and displays the result
        /// </summary>
        public void FilterMembers()
        {
            try
            {
                _currentMembers = _members;

                if (txtMembers.Text.ToString() != "")
                {
                    _currentMembers = _currentMembers.FindAll(s => s.FirstName.ToLower().Contains(txtMembers.Text.ToString().ToLower()));

                }


                if (btnActive.IsChecked == true)
                {
                    _currentMembers = _currentMembers.FindAll(s => s.Active.Equals(btnInActive.IsChecked));

                }
                else if (btnInActive.IsChecked == true)
                {
                    _currentMembers = _currentMembers.FindAll(s => s.Active.Equals(btnActive.IsChecked));
                }


                //_currentMembers = _currentMembers.FindAll(s => s.Active.Equals(btnActive.IsChecked));





                dgMember.ItemsSource = _currentMembers;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>
        /// 
        private void btnClearBrowseMembers_Click(object sender, RoutedEventArgs e)
        {
            _currentMembers = _members;
            dgMember.ItemsSource = _currentMembers;
        }

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {

            var createMemberForm = new frmAccount();
            var formResult = createMemberForm.ShowDialog();

            if (formResult == true)
            {

                try
                {
                    _currentMembers = null;
                    _members = _memberManager.RetrieveAllMembers();

                    if (_currentMembers == null)
                    {
                        _currentMembers = _members;
                    }
                    dgMember.ItemsSource = _currentMembers;

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created: 04/10/2019
        /// Open the member browser directly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgMember_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ViewSelectedRecord();

        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created: 04/11/2019
        /// Display data for selected member
        /// </summary>
        public void ViewSelectedRecord()
        {
            var member = (Member)dgMember.SelectedItem;
            var viewMemberForm = new frmAccount(member);
            var result = viewMemberForm.ShowDialog();
            if (result == true)
            {

                try
                {
                    _currentMembers = null;
                    _members = _memberManager.RetrieveAllMembers();

                    if (_currentMembers == null)
                    {
                        _currentMembers = _members;
                    }
                    dgMember.ItemsSource = _currentMembers;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

    }
}
