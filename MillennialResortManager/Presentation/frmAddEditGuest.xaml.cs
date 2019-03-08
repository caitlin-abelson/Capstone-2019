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
using LogicLayer;
using DataObjects;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for frmAddEditGuest.xaml
    /// </summary>
    public partial class frmAddEditGuest : Window
    {
        private GuestManager _guestManager = new GuestManager();
        private Guest _newGuest;
        private Guest _oldGuest;

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/25
        /// 
        /// Constructor for new guest 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public frmAddEditGuest() 
        {
            InitializeComponent();
            setEditable();
            btnGuestAction.Content = "Add";
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/31
        /// Modified: 2019/03/01
        /// 
        /// Constructor for view guest 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="oldGuest">Guest that will be shown on the details screen</param>
        public frmAddEditGuest(Guest oldGuest)
        {
            InitializeComponent();
            _oldGuest = _guestManager.ReadGuestByGuestID(oldGuest.GuestID);

            setOldGuest();
            setReadOnly();
            btnGuestAction.Content = "Edit";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboGuestType.ItemsSource = _guestManager.RetrieveGuestTypes();
            }
            catch (Exception)
            {
                MessageBox.Show("Guest Types not found.");
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/01
        /// Modified: 2019/03/01
        /// 
        /// for finishing with the form.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuestAction_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumber = txtPhoneNumber.Text;
            phoneNumber.Trim('-', '(', ')', ' ');
            string EMphoneNumber = txtEmerPhone.Text;
            EMphoneNumber.Trim('-', '(', ')', ' ');
            if (btnGuestAction.Content == "Add")
            {
                if (ValidateInfo())
                {
                    _newGuest = new Guest()
                    {
                        MemberID = int.Parse(txtMemberId.Text),
                        GuestTypeID = (string)cboGuestType.SelectedValue,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        PhoneNumber = phoneNumber,
                        Email = txtEmail.Text,
                        Minor = (bool)chkMinor.IsChecked,
                        ReceiveTexts = (bool)chkTexting.IsChecked,
                        EmergencyFirstName = txtEmerFirst.Text,
                        EmergencyLastName = txtEmerLast.Text,
                        EmergencyPhoneNumber = EMphoneNumber,
                        EmergencyRelation = txtEmerRelat.Text
                    };
                    try
                    {
                        _guestManager.CreateGuest(_newGuest);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Add Guest Failed!");
                    }
                }
                /*else
                {
                    MessageBox.Show("Not filled out fully. Fill out boxes next to the X's and try again.");
                }*/
            }
            else if (btnGuestAction.Content == "Edit")
            {
                // change from read only to edit
                setEditable();
                btnGuestAction.Content = "Save";
            }
            else if (btnGuestAction.Content == "Save")
            {
                if (ValidateInfo())
                {
                    _newGuest = new Guest()
                    {
                        GuestID = int.Parse(Title),
                        MemberID = int.Parse(txtMemberId.Text),
                        GuestTypeID = (string)cboGuestType.SelectedValue,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        PhoneNumber = phoneNumber,
                        Email = txtEmail.Text,
                        Minor = (bool)chkMinor.IsChecked,
                        Active = _oldGuest.Active,
                        ReceiveTexts = (bool)chkTexting.IsChecked,
                        EmergencyFirstName = txtEmerFirst.Text,
                        EmergencyLastName = txtEmerLast.Text,
                        EmergencyPhoneNumber = EMphoneNumber,
                        EmergencyRelation = txtEmerRelat.Text,
                        CheckedIn = _oldGuest.CheckedIn
                    };
                    try
                    {
                        _guestManager.EditGuest(_newGuest, _oldGuest);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Save Guest Failed!");
                    }
                }
                /* else
                 {
                     MessageBox.Show("Not filled out fully. Fill out boxes next to the X's and try again.");
                 }*/
            }
            else
            {
                this.DialogResult = true;
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/25
        /// 
        /// Used for validating form information. 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns>bool for if validates out</returns>
        private bool ValidateInfo()
        {
            string phoneNumber = txtPhoneNumber.Text;
            phoneNumber.Trim('-', '(', ')', ' ');
            string EMphoneNumber = txtEmerPhone.Text;
            EMphoneNumber.Trim('-', '(', ')', ' ');
            int aNumber;

            if (txtMemberId.Text.ToString().Length > 11 || txtMemberId.Text == null || txtMemberId.Text.ToString() == "0" || !int.TryParse(txtMemberId.Text, out aNumber) )
            {
                MessageBox.Show("Member ID must be filled out correctly");
                return false;// for member id
            }
            else if (cboGuestType.SelectedItem.ToString() == "" || cboGuestType.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Guest Type");
                return false; // for guest type
            }
            else if (txtFirstName.Text.ToString().Length > 50 || txtFirstName.Text == null || txtFirstName.Text.ToString().Length == 0 || txtFirstName.Text.ToString().Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Fill out first name correctly");
                return false; // for first name, only letters
            }
            else if (txtLastName.Text.ToString().Length > 100 || txtLastName.Text == null || txtLastName.Text.ToString().Length == 0 || txtLastName.Text.ToString().Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Fill out last name correctly");
                return false; // for last name, only letters
            }
            else if (phoneNumber.Length > 11 || phoneNumber.Length < 11 || phoneNumber == null || int.TryParse(phoneNumber, out aNumber))
            {
                MessageBox.Show("Fill out phone number correctly");
                return false;  // for phone number
            }
            else if (txtEmail.Text.ToString().Length > 250 || txtEmail.Text == null || txtEmail.Text.ToString().Length == 0 || !txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".") || !(txtEmail.Text.EndsWith("com") || txtEmail.Text.EndsWith("edu") || txtEmail.Text.EndsWith("gov")))
            {
                MessageBox.Show("Fill out email correctly");
                return false;  // for email, need greater email validation
            }
            else if (txtEmerFirst.Text.Length > 50 || txtEmerFirst.Text == null || txtEmerFirst.Text.Length == 0 || txtEmerFirst.Text.ToString().Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Fill out emergency contacts first name correctly");
                return false; // for EmergencyFirstName, only letters
            }
            else if (txtEmerLast.Text.Length > 100 || txtEmerLast.Text == null || txtEmerLast.Text.Length == 0 || txtEmerLast.Text.ToString().Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Fill out emergency contacts last name correctly");
                return false; // for EmergencyLastName, no numbers
            }
            else if (EMphoneNumber.Length != 11 || EMphoneNumber == null || int.TryParse(EMphoneNumber, out aNumber))
            {
                MessageBox.Show("Fill out emergency contacts phone number correctly");
                return false; // for EmergencyPhoneNumber
            }
            else if (txtEmerRelat.Text.Length > 25 || txtEmerRelat.Text == null || txtEmerRelat.Text.Length == 0)
            {
                MessageBox.Show("Fill out emergency contacts relation correctly");
                return false; // for EmergencyRelation
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/31
        /// Modified: 2019/03/01
        /// 
        /// setting the form up for viewing or editing. 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        private void setOldGuest()
        {
            Title = _oldGuest.GuestID.ToString();
            txtMemberId.Text = _oldGuest.MemberID.ToString();
            txtFirstName.Text = _oldGuest.FirstName;
            txtLastName.Text = _oldGuest.LastName;
            txtEmail.Text = _oldGuest.Email;
            txtPhoneNumber.Text = _oldGuest.PhoneNumber;
            cboGuestType.SelectedItem = _oldGuest.GuestTypeID;
            chkMinor.IsChecked = _oldGuest.Minor;
            chkTexting.IsChecked = _oldGuest.ReceiveTexts;
            txtEmerFirst.Text = _oldGuest.EmergencyFirstName;
            txtEmerLast.Text = _oldGuest.EmergencyLastName;
            txtEmerPhone.Text = _oldGuest.EmergencyPhoneNumber;
            txtEmerRelat.Text = _oldGuest.EmergencyRelation;
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/31
        /// Modified: 2019/03/01
        /// 
        /// setting the form for Read Only, aka View Guest. 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        private void setReadOnly()
        {
            txtMemberId.IsReadOnly = true;
            txtFirstName.IsReadOnly = true;
            txtLastName.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtPhoneNumber.IsReadOnly = true;
            cboGuestType.IsEnabled = false;
            chkMinor.IsEnabled = false;
            chkTexting.IsEnabled = false;
            txtEmerFirst.IsReadOnly = true;
            txtEmerLast.IsReadOnly = true;
            txtEmerPhone.IsReadOnly = true;
            txtEmerRelat.IsReadOnly = true;
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/31
        /// Modified: 2019/03/01
        /// 
        /// setting the form for Editing. 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        private void setEditable()
        {
            txtMemberId.IsReadOnly = !true;
            txtFirstName.IsReadOnly = !true;
            txtLastName.IsReadOnly = !true;
            txtEmail.IsReadOnly = !true;
            txtPhoneNumber.IsReadOnly = !true;
            cboGuestType.IsEnabled = !false;
            chkMinor.IsEnabled = !false;
            chkTexting.IsEnabled = !false;
            txtEmerFirst.IsReadOnly = !true;
            txtEmerLast.IsReadOnly = !true;
            txtEmerPhone.IsReadOnly = !true;
            txtEmerRelat.IsReadOnly = !true;
        }

        private void BtnGuestActionCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
        
    }

