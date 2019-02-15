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

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for frmUpdatePassword.xaml
    /// </summary>
    public partial class frmUpdatePassword : Window
    {
        private User _user;
        private UserManager _userManager;
        private bool isNewUser;
        public frmUpdatePassword(User user,
            UserManager userManager, bool isNew = false)
        {
            this._user = user;
            this._userManager = userManager;
            this.isNewUser = isNew;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isNewUser)
            {
                tbkMessage.Text = _user.FirstName +
                 " as a new user, you must "
                 + tbkMessage.Text;
            }
            else
            {
                tbkMessage.Text = "You may user this dialog to " + tbkMessage.Text;
            }
            txtEmail.Focus();

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // make sure they fill out all of the stuff correctly
            if (txtEmail.Text.Length < 7 || txtEmail.Text.Length > 250)
            {
                MessageBox.Show("Invalid Email");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return;
            }

            if (pwdOldPassword.Password.Length < 6)
            {
                MessageBox.Show("Invalid Current Password");
                pwdOldPassword.Focus();
                pwdOldPassword.SelectAll();
                return;
            }

            if (pwdNewPassword.Password.Length < 6)
            {
                MessageBox.Show("Invalid New Password");
                pwdNewPassword.Focus();
                pwdNewPassword.SelectAll();
                return;
            }

            if (string.Compare(pwdNewPassword.Password, pwdRetypePassword.Password) != 0)
            {
                MessageBox.Show("New Password and Retyped Passoword must match");
                pwdRetypePassword.Password = "";
                pwdRetypePassword.Focus();
                pwdRetypePassword.SelectAll();
                return;
            }

            // Whew! We made it past the user errors!
            string oldPassword = pwdOldPassword.Password;
            string newPassword = pwdNewPassword.Password;
            string userName = txtEmail.Text;

            try
            {
                if (_userManager.UpdatePassword(userName, oldPassword, newPassword))
                {
                    MessageBox.Show("Password Updated");
                    _userManager.RefreshRoles(_user, userName);
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
