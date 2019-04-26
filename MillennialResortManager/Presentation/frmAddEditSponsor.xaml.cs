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
    /// Author: Gunardi Saputra
    /// Created : 2/20/2019
    /// Updated : 
    /// Interaction logic for frmSponsor.xaml
    /// </summary>
    public partial class FrmSponsor : Window
    {
        private List<Sponsor> _sponsors;
        SponsorManager _sponsorManager;
        Sponsor _existingSponsor;
        private Sponsor _newSponsor;
        private Sponsor _oldSponsor;

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created : 2/20/2019
        /// This constructor is used for Creating a sponsor
        /// Initializes connections to our SponsorManager and
        /// </summary>
        public FrmSponsor()
        {
            InitializeComponent();
            _sponsorManager = new SponsorManager();
            try
            {
                _sponsors = _sponsorManager.SelectAllSponsors();
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
            chkActive.Visibility = Visibility.Hidden;
            //txtSponsorID.Visibility = Visibility.Hidden;
            //dtpDateAdded.Visibility = Visibility.Hidden;
            chkActive.IsChecked = true;
            _existingSponsor = null;
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created : 2/22/2019
        /// This constructor is used for Reading and Updating a Sponsor
        /// </summary>
        /// 
        public FrmSponsor(Sponsor existingSponsor)
        {
            InitializeComponent();
            _sponsorManager = new SponsorManager();
            try
            {
                _sponsors = _sponsorManager.SelectAllSponsors();
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
            _existingSponsor = existingSponsor;
            populateFormReadOnly();
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// This method fills in all of the information for the sponsor that was chosen in browse.
        /// </summary>
        private void populateFormReadOnly()
        {
            //txtSponsorID.Text = "" + _existingSponsor.SponsorID;
            txtName.Text = "" + _existingSponsor.Name;
            txtAddress.Text = "" + _existingSponsor.Address;
            txtCity.Text = "" + _existingSponsor.City;
            cboState.SelectedItem = "" + _existingSponsor.State;
            txtPhoneNumber.Text = "" + _existingSponsor.PhoneNumber;
            txtEmail.Text = "" + _existingSponsor.Email;
            txtContactFirstName.Text = "" + _existingSponsor.ContactFirstName;
            txtContactLastName.Text = "" + _existingSponsor.ContactLastName;
            //cboStatusID.SelectedItem = "" + _existingSponsor.StatusID;
            dtpDateAdded.SelectedDate = _existingSponsor.DateAdded;
            chkActive.IsChecked = _existingSponsor.Active;
            setReadOnly();
            btnSave.Content = "Update";
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 02/20/2019
        /// 
        /// It will loaded every it's called.
        /// 
        /// Updated: Gunardi Saputra
        /// Date: 04/19/2019
        /// remove statusID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboState.ItemsSource = _sponsorManager.RetrieveAllStates();
                //cboStatusID.ItemsSource = _sponsorManager.RetrieveAllSponsorStatus();


            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 02/20/2019
        /// 
        /// This sets up for the read only information
        /// 
        /// Updated: Gunardi Saputra
        /// Date: 04/19/2019
        /// </summary>
        private void setReadOnly()
        {
            //txtSponsorID.IsReadOnly = true;
            txtName.IsReadOnly = true;
            txtAddress.IsReadOnly = true;
            txtCity.IsReadOnly = true;
            cboState.IsEnabled = false;
            txtPhoneNumber.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtContactFirstName.IsReadOnly = true;
            txtContactLastName.IsReadOnly = true;
            //cboStatusID.IsEnabled = false;
            dtpDateAdded.IsEnabled = false;
            chkActive.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 02/20/2019
        /// 
        /// This sets up the information that can be edited on the form when the user
        /// clicks update
        /// 
        /// Updated: Gunardi Saputra
        /// Date: 04/19/2019
        /// </summary>
        private void setEditable()
        {
            txtName.IsReadOnly = false;
            txtAddress.IsReadOnly = false;
            txtCity.IsReadOnly = false;
            cboState.IsEnabled = true;
            txtPhoneNumber.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtContactFirstName.IsReadOnly = false;
            txtContactLastName.IsReadOnly = false;
            //cboStatusID.IsEnabled = !false;
            dtpDateAdded.IsEnabled = true;

            btnSave.Content = "Submit";
            chkActive.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 02/20/2019
        /// 
        /// The btnSave_Click method is used for saving a new sponsor or updating 
        /// an existing sponsor in the system.
        /// 
        /// Update: Gunardi Saputra
        /// Date: 04/19/2019
        /// remove statusID
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            //if (((string)btnSave.Content) == "Update" || ((string)btnSave.Content) == "Submit")

            if (((string)btnSave.Content) == "Submit" )
            {
                if (!ValidateInput())
                {
                    return;
                }
                Sponsor newSponsor = new Sponsor
                {
                    //SponsorID = int.Parse(txtSponsorID.Text),
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    City = txtCity.Text,
                    State = (string)cboState.SelectedItem,
                    PhoneNumber = txtPhoneNumber.Text,
                    Email = txtEmail.Text,
                    ContactFirstName = txtContactFirstName.Text,
                    ContactLastName = txtContactLastName.Text,
                    //StatusID = (string)cboStatusID.SelectedItem,
                    DateAdded = DateTime.Parse(dtpDateAdded.Text)
                };
                try
                {
                    if (_existingSponsor == null)
                    {

                        _sponsorManager.InsertSponsor(newSponsor);
                        SetError("");
                        MessageBox.Show("Sponsor Was Created Successfully: " +
                        "\nSponsorID: " + newSponsor.SponsorID +
                        "\nName: " + newSponsor.Name +
                        "\nDateAdded: " + newSponsor.DateAdded.ToString("MM/dd/yyyy") +
                        "\nAddress: " + newSponsor.Address);
                    }
                    else
                    {
                        newSponsor.Active = (bool)chkActive.IsChecked;
                        if (_sponsorManager.UpdateSponsor(_existingSponsor, newSponsor))
                        {

                            SetError("");
                            MessageBox.Show("Sponsor Updated Successfully: " +
                            "\nOld SponsorID: " + _existingSponsor.SponsorID +
                            "\nOld Name: " + _existingSponsor.Name +
                            "\nOld DateAdded: " + _existingSponsor.DateAdded.ToString("MM/dd/yyyy") +
                            "\nOld Address: " + _existingSponsor.Address +

                            "\nNew SponsorID: " + newSponsor.SponsorID +
                            "\nNew Name: " + newSponsor.Name +
                            "\nNew DateAdded: " + newSponsor.DateAdded.ToString("MM/dd/yyyy") +
                            "\nNew Address: " + newSponsor.Address);
                        }
                        else
                        {
                            MessageBox.Show("Failed to Update Sponsor");
                        }
                    }
                }
                catch (Exception ex)
                {
                    SetError(ex.Message);
                }

                Close();
            }
            else if(((string)btnSave.Content) == "Save"){

                if (!ValidateInput())
                {
                    return;
                }
                Sponsor newSponsor = new Sponsor
                {
                    //SponsorID = int.Parse(txtSponsorID.Text),
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    City = txtCity.Text,
                    State = (string)cboState.SelectedItem,
                    PhoneNumber = txtPhoneNumber.Text,
                    Email = txtEmail.Text,
                    ContactFirstName = txtContactFirstName.Text,
                    ContactLastName = txtContactLastName.Text,
                    //StatusID = (string)cboStatusID.SelectedItem,
                    //DateAdded = DateTime.Parse(dtpDateAdded.Text)
                };
                try
                {
                    if (_existingSponsor == null)
                    {

                        _sponsorManager.InsertSponsor(newSponsor);
                        SetError("");
                        MessageBox.Show("Sponsor Was Created Successfully: " +
                        "\nSponsorID: " + newSponsor.SponsorID +
                        "\nName: " + newSponsor.Name +
                        "\nDateAdded: " + newSponsor.DateAdded.ToString("MM/dd/yyyy") +
                        "\nAddress: " + newSponsor.Address);
                    }
                    else
                    {
                        newSponsor.Active = (bool)chkActive.IsChecked;
                        if (_sponsorManager.UpdateSponsor(_existingSponsor, newSponsor))
                        {

                            SetError("");
                            MessageBox.Show("Sponsor Updated Successfully: " +
                            "\nOld SponsorID: " + _existingSponsor.SponsorID +
                            "\nOld Name: " + _existingSponsor.Name +
                            "\nOld DateAdded: " + _existingSponsor.DateAdded.ToString("MM/dd/yyyy") +
                            "\nOld Address: " + _existingSponsor.Address +

                            "\nNew SponsorID: " + newSponsor.SponsorID +
                            "\nNew Name: " + newSponsor.Name +
                            "\nNew DateAdded: " + newSponsor.DateAdded.ToString("MM/dd/yyyy") +
                            "\nNew Address: " + newSponsor.Address);
                        }
                        else
                        {
                            MessageBox.Show("Failed to Update Sponsor");
                        }
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
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// The ValidateInput goes through every validation method to see if they pass. 
        /// </summary>
        private bool ValidateInput()
        {
            if (ValidateName())
            {
                if (ValidateAddress())
                {
                    if (ValidateCity())
                    {
                        //if (ValidateStatusID())
                        //{

                        if (ValidateState())
                        {
                            if (ValidatePhoneNumber())
                            {
                                if (ValidateEmail())
                                {


                                    if (ValidateContactFirstName())
                                    {
                                        if (ValidateContactLastName())
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            SetError("INVALID LAST NAME");
                                        }
                                    }
                                    else
                                    {
                                        SetError("INVALID FIRST NAME");
                                    }
                                }
                                else
                                {
                                    SetError("INVALID Email");
                                }
                            }
                            else
                            {
                                SetError("INVALID PHONE NUMBER");
                            }

                        }
                        else
                        {
                            SetError("INVALID STATE");
                        }
                        //}


                        //else
                        //{
                        //SetError("INVALID STATUS ID");
                        //}

                    }
                    else
                    {
                        SetError("INVALID CITY");
                    }
                }
                else
                {
                    SetError("INVALID ADDRESS");
                }
            }
            else
            {
                SetError("INVALID NAME");
            }


            return false;
        }



        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// The SetError method 
        /// give an error message
        /// </summary>
        private void SetError(string error)
        {
            lblError.Content = error;
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// The ValidateDateAdded tracks to see if an item was selected from the DateAdded box
        /// and returns true if there was and false if there wasn't
        /// It's not used at the moment. Date is provided automaticly
        /// </summary>
        private bool ValidateDateAdded()
        {
            //Date Added cannot be null
            if (dtpDateAdded.Text == null || dtpDateAdded.Text == "")
            {
                //Date is invalid
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/07
        /// 
        /// The ValidateAddress method makes sure that 
        /// the sponsor Name has the correct amount of characters.
        /// </summary>
        private bool ValidateAddress()
        {
            // Address can't be a null or empty string
            if (txtAddress.Text == null || txtAddress.Text == "")
            {
                return false;
            }
            // Address no more than 50 characters long

            if (txtAddress.Text.Length >= 7 && txtAddress.Text.Length < 50)
            {
                return true;
            }
            // Less than 2 characters and more than 50 characters return false
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/07
        /// 
        /// The ValidateSponsorID method makes sure that 
        /// the SponsorID  has the correct amount of characters.
        /// </summary>
        //private bool ValidateSponsorID()
        //{
        //    if (txtSponsorID.Text == null || txtSponsorID.Text == "")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/07
        /// 
        /// The ValidateName method makes sure that 
        /// the sponsor Name has the correct amount of characters.
        /// </summary>
        private bool ValidateName()
        {
            // Name can't be a null or empty string
            if (txtName.Text == null || txtName.Text == "")
            {
                return false;
            }
            // Name no more than 50 characters long

            if (txtName.Text.Length >= 2 && txtName.Text.Length <= 50)
            {
                return true;
            }
            // Less than 2 characters and more than 50 characters return false
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/07
        /// 
        /// The ValidateCity method makes sure that 
        /// the txtCity has the correct amount of characters.
        /// </summary>
        private bool ValidateCity()
        {
            // City can't be a null or empty string
            if (txtCity.Text == null || txtCity.Text == "")
            {
                return false;
            }
            // City no more than 50 characters long

            if (txtCity.Text.Length >= 2 && txtCity.Text.Length <= 50)
            {
                return true;
            }
            // Less than 2 characters and more than 50 characters return false
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// The State tracks to see if an item was selected from the State drop down combo box
        /// and returns true if there was and false if there wasn't
        /// </summary>
        private bool ValidateState()
        {
            if (cboState.Text == null || cboState.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 1/30/19
        /// 
        /// The ValidatePhone method makes sure that the Phone has the correct amount of characters.
        /// </summary>
        private bool ValidatePhoneNumber()
        {
            // Phone can't be a null or empty string
            if (txtPhoneNumber.Text == null || txtPhoneNumber.Text == "")
            {
                return false;
            }
            // Phone must be 11 characters long
            if (txtPhoneNumber.Text.Length == 11)
            {
                return true;
            }

            // If PhoneNumber is greater or less than 11 characters, then the method returns false
            return false;

        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// The ValidateEmail method makes sure that the Email has the correct amount of characters.
        /// </summary>
        private bool ValidateEmail()
        {
            bool validExtension = false;

            // Email can't be a null or empty string
            if (txtEmail.Text == null || txtEmail.Text == "")
            {
                return false;
            }

            // Email must be 7 to 250 characters long
            if (txtEmail.Text.Length >= 7 && txtEmail.Text.Length <= 250 && txtEmail.Text.Contains("."))
            {
                // Email must contain an @ and a .com in order to be an email
                if (txtEmail.Text.Contains("@"))
                {
                    if (txtEmail.Text.Contains("com"))
                    {
                        validExtension = true;
                    }
                    else if (txtEmail.Text.Contains("edu"))
                    {
                        validExtension = true;
                    }
                    else
                    {
                        validExtension = false;
                    }
                }
            }

            // If Email is greater than 250 characters, then the method returns false
            return validExtension;

        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/07
        /// 
        /// The ValidateContactFirstName method makes sure that 
        /// the ContactFirstName has the correct amount of characters.
        /// </summary>
        private bool ValidateContactFirstName()
        {
            // ContactFirstName can't be a null or empty string
            if (txtContactFirstName.Text == null || txtContactFirstName.Text == "")
            {
                return false;
            }
            // ContactFirstName no more than 50 characters long

            if (txtContactFirstName.Text.Length >= 2 && txtContactFirstName.Text.Length <= 50)
            {
                return true;
            }
            // Less than 2 characters and more than 50 characters return false
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/07
        /// 
        /// The ValidateContactLastName method makes sure that 
        /// the ContactLastName has the correct amount of characters.
        /// </summary>
        private bool ValidateContactLastName()

        {
            // ContactFirstName can't be a null or empty string
            if (txtContactLastName.Text == null || txtContactLastName.Text == "")
            {
                return false;
            }
            // ContactFirstName no more than 50 characters long

            if (txtContactLastName.Text.Length >= 2 && txtContactLastName.Text.Length <= 50)
            {
                return true;
            }
            // Less than 2 characters and more than 50 characters return false
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 02/20/2019
        /// 
        /// The StatusID tracks to see if an item was selected from the StatusID drop down combo box
        /// and returns true if there was and false if there wasn't
        /// 
        /// Updated: Gunardi Saputra
        /// Date: 04/19/2019
        /// remove statusID
        /// </summary>
        //private bool ValidateStatusID()
        //{
        //    if (cboStatusID.Text == null || cboStatusID.Text == "")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// This button closes the details screeen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel?",
                "Leaving Sponsor form detail screen.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
