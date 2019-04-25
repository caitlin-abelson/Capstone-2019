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
        private GuestManager _guestManager;
        private MemberManagerMSSQL _member;
        private List<Member> _members;
        private Guest _newGuest;
        private Guest _oldGuest;

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/25
        /// 
        /// Constructor for new guest 
        /// <summary>
        /// Updated By: Caitlin Abelson
        /// Date: 2019/04/11
        /// 
        /// Added the combobox for the existing members.
        /// 
        /// </remarks>
        public frmAddEditGuest()
        {
            InitializeComponent();
            _guestManager = new GuestManager();
            _member = new MemberManagerMSSQL();


            try
            {
                _members = _member.RetrieveAllMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            cbxMemberName.Items.Clear();
            if (_members != null)
            {
                foreach (var item in _members)
                {
                    cbxMemberName.Items.Add(item.FirstName + " " + item.LastName);
                }
            }


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
            _guestManager = new GuestManager();
            _member = new MemberManagerMSSQL();

            try
            {
                _members = _member.RetrieveAllMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            cbxMemberName.Items.Clear();
            foreach (var item in _members)
            {
                cbxMemberName.Items.Add(item.FirstName + " " + item.LastName);

                if (item.MemberID == oldGuest.MemberID)
                {
                    cbxMemberName.SelectedItem = item.FirstName + " " + item.LastName;
                }
            }

            _oldGuest = oldGuest;

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
        /// 
        /// Updated By: Caitlin Abelson
        /// Date: 2019/04/10 
        /// 
        /// Changed the memberID so that is now reflected the new combobox that needed to be added.
        /// The combobox will have the memberID if it's an update and the user will able to choose 
        /// from a list of members if it's a new guest being made.
        /// 
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
                    _newGuest = new Guest();

                    _newGuest.MemberID = _members.ElementAt(cbxMemberName.SelectedIndex).MemberID;
                    _newGuest.GuestTypeID = (string)cboGuestType.SelectedValue;
                    _newGuest.FirstName = txtFirstName.Text;
                    _newGuest.LastName = txtLastName.Text;
                    _newGuest.PhoneNumber = phoneNumber;
                    _newGuest.Email = txtEmail.Text;
                    _newGuest.Minor = (bool)chkMinor.IsChecked;
                    _newGuest.ReceiveTexts = (bool)chkTexting.IsChecked;
                    _newGuest.EmergencyFirstName = txtEmerFirst.Text;
                    _newGuest.EmergencyLastName = txtEmerLast.Text;
                    _newGuest.EmergencyPhoneNumber = EMphoneNumber;
                    _newGuest.EmergencyRelation = txtEmerRelat.Text;

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
            else
            {
                if (btnGuestAction.Content == "Edit")
                {
                    // change from read only to edit
                    setEditable();
                    cbxMemberName.IsEnabled = false;
                    btnGuestAction.Content = "Save";
                }
                else if (btnGuestAction.Content == "Save")
                {
                    if (ValidateInfo())
                    {
                        _newGuest = new Guest()
                        {
                            GuestID = int.Parse(Title),
                            MemberID = _members.ElementAt(cbxMemberName.SelectedIndex).MemberID,
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
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/25
        /// 
        /// Used for validating form information. 
        /// </summary>
        /// <remarks>
        /// Updated By: Caitlin Abelson
        /// Date: 2019/04/10 
        /// 
        /// Changed the memberID to a combobox so the only validation is a member
        /// needs to be selected in order to be associated with a guest.
        /// 
        /// Alisa Roehr
        /// Updated: 2019/04/16 
        /// fix: made it so that other extentions can be used for email.
        /// </remarks>
        /// <returns>bool for if validates out</returns>
        private bool ValidateInfo()
        {
            string phoneNumber = txtPhoneNumber.Text;
            phoneNumber.Trim('-', '(', ')', ' ');
            string EMphoneNumber = txtEmerPhone.Text;
            EMphoneNumber.Trim('-', '(', ')', ' ');
            int aNumber;

            if (cbxMemberName.SelectedItem == null)
            {
                MessageBox.Show("Must select a member when creating a new Guest.");
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
            else if (txtEmail.Text.ToString().Length > 250 || txtEmail.Text == null || txtEmail.Text.ToString().Length == 0 || !txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
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
        /// setting the form for Read Only, aka View Guest. 
        /// 
        /// Updated By: Caitlin Abelson
        /// Date: 2019/04/10 
        /// 
        /// Changed MemberID to a comboBox.
        /// Set all of the information for the textboxes in this method
        /// as it is easier to handle.
        /// 
        private void setReadOnly()
        {
            cbxMemberName.IsEnabled = false;
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

            foreach (var item in _members)
            {
                if ((item.FirstName + " " + item.LastName) == (string)cbxMemberName.SelectedItem)
                {
                    _oldGuest.MemberID = item.MemberID;
                }
            }


            Title = _oldGuest.GuestID.ToString();
            cbxMemberName.SelectedItem = _oldGuest.MemberID;
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
        /// setting the form for Editing. 
        /// 
        /// Updated By: Caitlin Abelson
        /// Date: 2019/04/10 
        /// 
        /// Changed MemberID to a comboBox.
        /// 
        private void setEditable()
        {
            cbxMemberName.IsEnabled = true;
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtPhoneNumber.IsReadOnly = false;
            cboGuestType.IsEnabled = true;
            chkMinor.IsEnabled = true;
            chkTexting.IsEnabled = true;
            txtEmerFirst.IsReadOnly = false;
            txtEmerLast.IsReadOnly = false;
            txtEmerPhone.IsReadOnly = false;
            txtEmerRelat.IsReadOnly = false;

        }

        private void BtnGuestActionCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}

