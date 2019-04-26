using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// </summary>
    public partial class frmAccount : Window

    {
        
        IMemberManager _memberManager = new MemberManagerMSSQL();
        Member _oldMember;
        Member _newMember;
        Context _context;
        public frmAccount()
        {
            InitializeComponent();
            _context = Context.Create;
        }

        public frmAccount(Member member)
        {
            InitializeComponent();

           _context = Context.View;
            btnSave.Content = "Edit";

            _oldMember = member;

            loadOldMember();
            disableEditing();
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 03/1/2019
        /// </summary>
        private void loadOldMember()
        {
           
            txtFirstName.Text = _oldMember.FirstName;
            txtLastName.Text = _oldMember.LastName;
            txtPhoneNumber.Text = _oldMember.PhoneNumber;
            txtEmail.Text = _oldMember.Email;
            txtPassword.Password = _oldMember.Password;
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// Disable editing when the window loads
        /// </summary>
        private void disableEditing()
        {
            txtFirstName.IsReadOnly = true;
            txtLastName.IsReadOnly = true;
            txtPhoneNumber.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtPassword.IsEnabled = false;
           // txtActive.IsEnabled = false;
 
           
           
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On:01/30/2019
        /// Enabled editing when the user clicks edit
        /// </summary>
        private void enableEditing()
        {
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtPhoneNumber.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtActive.IsChecked = true;
            
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 03/1/2019
        /// create a new member
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(_context.Equals(Context.Create))
            {
                CreateMember();
                try
                {
                    //InsertMember();
                    _memberManager.CreateMember(_newMember);

                    MessageBox.Show("Member Created Successfully");
                    this.DialogResult = true;

                }
                
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                
            }
            else if(_context.Equals(Context.View))
            {
                enableEditing();
                _context = Context.Edit;
                btnSave.Content = "Save";
                
            }
            else if(_context.Equals(Context.Edit))
            {
                try
                {
                    
                    CreateMember();
                    _memberManager.UpdateMember (_newMember, _oldMember);
                    MessageBox.Show("Member Updated Successfully");
                    this.DialogResult = true;
                    
                }
                catch (Exception ex )
                {

                    MessageBox.Show(ex.Message + "\n" + ex.InnerException);
                }
                return;
            }
        }

       
        public bool ValidateEmailAddress()
        {
            bool emailValidate = false;

            

            if (txtEmail.Text.Length >= 1 && txtEmail.Text.Length <= 250 && txtEmail.Text.Contains("."))
            {

                
                if (txtEmail.Text.Contains("@"))
                {
                    if (txtEmail.Text.Contains("com"))
                    {
                        emailValidate = true;
                    }
                    else if (txtEmail.Text.Contains("edu"))
                    {
                        emailValidate = true;
                    }
                    else if (txtEmail.Text.Contains("org"))
                    {
                        emailValidate = true;
                    }

                    else
                    {
                        return false;
                    }
                }
            }
            return emailValidate;
        }


        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// Create new member and check for the validation 
        /// </summary>
        private void CreateMember()
        {
            
            
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();
            string active = txtActive.IsEnabled.ToString();


            if (txtFirstName.Text == "" ||
                txtLastName.Text == "" ||
                txtEmail.Text == "" ||
                txtPhoneNumber.Text == ""

                )
            {
                MessageBox.Show("Please Fill Out the Form");
            }

            


            // Validate the phone number
            else if (!Regex.IsMatch(txtPhoneNumber.Text, @"^\(?\d{3}\)?[\s\-]?\d{3}\-?\d{4}$"))
            {
                MessageBox.Show("Please Enter Valid Phone Number");
                txtPhoneNumber.Select(0, txtPhoneNumber.Text.Length);
                txtPhoneNumber.Focus();
            }

            else if (!ValidateEmailAddress())
            {
                MessageBox.Show("Please Enter Valid Email");
                txtEmail.Select(0, txtEmail.Text.Length);
                txtEmail.Focus();
            }
            else
            {
                MemberValidator.ValidateEmail(new Member() { Email = email });
                MemberValidator.ValidatePhoneNumber(new Member() { PhoneNumber = phoneNumber });

                // New member
                try
                {
                    _newMember = new Member
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhoneNumber = phoneNumber,
                        Email = email,
                        Password = password,
                        //Active = active,

                    

                    };

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }
                }

    


        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            switch(_context)
            {
                case Context.Create:
                    DialogResult = true;
                     break;

                case Context.View:
                    DialogResult = false;
                    break;

                case Context.Edit:
                    disableEditing();
                    _context = Context.View;
                    btnSave.Content = "Edit";
                    break;

            }
        }
        public enum Context
        {
            Create,
            View,
            Edit
        
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// View all MemberTabs for the current member.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewMemberTabs_Click(object sender, RoutedEventArgs e)
        {
            var tabForm = new MemberTabDetail(_oldMember.MemberID);
            var result = tabForm.ShowDialog();
        }
    }
}
    
    

