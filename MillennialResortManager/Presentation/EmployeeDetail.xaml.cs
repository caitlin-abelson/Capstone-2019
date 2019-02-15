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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class EmployeeDetail : Window
    {
        private List<Department> _departments;
        EmployeeManager _employeeManager;
        DepartmentManager _departmentManager;
        private Employee _newEmployee;
        private Employee _oldEmployee;


        // commented out the create constructor in order to test the update and read constructor

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// This is the constructor for creating an employee
        /// </summary>
        public EmployeeDetail()
        {
            InitializeComponent();
            _departmentManager = new DepartmentManager();
            _employeeManager = new EmployeeManager();

            try
            {
                _departments = _departmentManager.GetAllDepartments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbxDepartment.ItemsSource = _departments;
            chkActive.IsChecked = true;
            chkActive.Visibility = Visibility.Hidden;
            lblActive.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/3/19
        /// 
        /// This is the constructor for reading and updating 
        /// </summary>
        /// <param name="oldEmployee"></param>
        public EmployeeDetail(Employee oldEmployee)
        {
            InitializeComponent();
            _departmentManager = new DepartmentManager();
            _employeeManager = new EmployeeManager();

            try
            {
                _departments = _departmentManager.GetAllDepartments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbxDepartment.ItemsSource = _departments;
            _oldEmployee = oldEmployee;
            populateReadOnly();
            readOnlyForm();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/3/19
        /// 
        /// This method establishes what information is read only when someone is reading information about 
        /// and employee.
        /// </summary>
        private void readOnlyForm()
        {
            txtFirstName.Text = _oldEmployee.FirstName;
            txtLastName.Text = _oldEmployee.LastName;
            txtPhone.Text = _oldEmployee.PhoneNumber;
            txtEmail.Text = _oldEmployee.Email;
            cbxDepartment.SelectedItem = _oldEmployee.DepartmentID;
            chkActive.IsChecked = _oldEmployee.Active;

            txtFirstName.IsReadOnly = true;
            txtLastName.IsReadOnly = true;
            txtPhone.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            cbxDepartment.IsEnabled = false;
            chkActive.Visibility = Visibility.Hidden;
            lblActive.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/5/19
        /// 
        /// This sets up the information that can be edited on the form when the user
        /// clicks update
        /// </summary>
        private void editableForm()
        {
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtPhone.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            cbxDepartment.IsEnabled = true;
            btnSave.Content = "Submit";
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/5/19
        /// 
        /// This method fills in all of the information for the employee that was chosen in browse.
        /// </summary>
        private void populateReadOnly()
        {
            txtFirstName.Text = _oldEmployee.FirstName;
            txtLastName.Text = _oldEmployee.LastName;
            txtPhone.Text = _oldEmployee.PhoneNumber;
            txtEmail.Text = _oldEmployee.Email;
            cbxDepartment.SelectedItem = _departments.Find(d => d.DepartmentID ==_oldEmployee.DepartmentID);
            chkActive.IsChecked = _oldEmployee.Active;
            readOnlyForm();
            btnSave.Content = "Update";
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The btnSave_Click method is used for saving a new employee or updating 
        /// an existing employee in the system.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Content.Equals("Submit"))
            {
                if (!ValidateInput())
                {
                    return;
                }

                _newEmployee = new Employee()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text,
                    DepartmentID = cbxDepartment.SelectedItem.ToString()
                };

                try
                {
                    if(_oldEmployee == null)
                    {            
                        _employeeManager.InsertEmployee(_newEmployee);

                        MessageBox.Show("Employee Created: " +
                            "\nFirst Name: " + _newEmployee.FirstName +
                            "\nLast Name: " + _newEmployee.LastName +
                            "\nEmail: " + _newEmployee.Email +
                            "\nPhone Number: " + _newEmployee.PhoneNumber +
                            "\nDepartment: " + _newEmployee.DepartmentID);
                    }
                    else
                    {
                        _newEmployee.Active = (bool)chkActive.IsChecked;
                        _employeeManager.UpdateEmployee(_newEmployee, _oldEmployee);
                        SetError("");
                        MessageBox.Show("Employee update successful: " +
                            "\nNew First Name: " + _newEmployee.FirstName +
                            "\nNew Last Name: " + _newEmployee.LastName +
                            "\nNew Phone Number: " + _newEmployee.PhoneNumber +
                            "\nNew Email: " + _newEmployee.Email +
                            "\nNew DepartmentID: " + _newEmployee.DepartmentID +
                            "\nOld First Name: " + _oldEmployee.FirstName +
                            "\nOld Last Name: " + _oldEmployee.LastName +
                            "\nOld Phone Number: " + _oldEmployee.PhoneNumber +
                            "\nOld Email: " + _oldEmployee.Email +
                            "\nOld DepartmentID: " + _oldEmployee.DepartmentID);
                    }

                }
                catch (Exception ex)
                {
                    SetError(ex.Message);
                }

                Close();
            }
            else if (btnSave.Content.Equals("Update"))
            {
                editableForm();
            }



        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// This is a helper method in order to set error messages to print to the screen for the user to see.
        /// </summary>
        /// <param name="error"></param>
        private void SetError(string error)
        {
            lblError.Content = error;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateInput goes through every validation method to see if they pass. If they do, then true is returned.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            if (ValidateFirstName())
            {
                if (ValidateLastName())
                {
                    if (ValidatePhone())
                    {
                        if (ValidateEmail())
                        {
                            if (ValidateDepartmentID())
                            {
                                return true;
                            }
                            else
                            {
                                SetError("You must choose a department for this employee.");
                            }
                        }
                        else
                        {
                            SetError("Invalid email.");
                        }
                    }
                    else
                    {
                        SetError("Invalid phone number.");
                    }
                }
                else
                {
                    SetError("Invalid  last name.");
                }
            }
            else
            {
                SetError("Invalid first name.");
            }
            return false;
        }




        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateFirstName method makes sure that the FirstName has the correct amount of characters.
        /// </summary>
        /// <returns></returns>
        private bool ValidateFirstName()
        {
            // FirstName can't be a null or empty string
            if (txtFirstName.Text == null || txtFirstName.Text == "")
            {
                return false;
            }
            // FirstName must be no more than 50 characters long
            if (txtLastName.Text.Length >= 1 && txtFirstName.Text.Length <= 50)
            {
                return true;
            }

            // If FirstName is greater than 50 characters, then the method returns false
            return false;
            
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateLastName method makes sure that the LastName has the correct amount of characters.
        /// </summary>
        /// <returns></returns>
        private bool ValidateLastName()
        {
            // LastName can't be a null or empty string
            if (txtLastName.Text == null || txtLastName.Text == "")
            {
                return false;
            }
            // LastName must be no more than 100 characters long
            if (txtLastName.Text.Length >= 1 && txtLastName.Text.Length <= 100)
            {
                return true;
            }

            // If LastName is greater than 100 characters, then the method returns false
            return false;
            
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateEmail method makes sure that the Email has the correct amount of characters.
        /// </summary>
        /// <returns></returns>
        private bool ValidateEmail()
        {
            bool validExtension = false;

            // Email can't be a null or empty string
            if (txtEmail.Text == null || txtLastName.Text == "")
            {
                return false;
            }

            // Email must be no more than 11 characters long
            if (txtEmail.Text.Length >= 1 && txtEmail.Text.Length <= 250 && txtEmail.Text.Contains("."))
            {
                // Email must contain an @ and a .com in order to be an email
                if(txtEmail.Text.Contains("@"))
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
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidatePhone method makes sure that the Phone has the correct amount of characters.
        /// </summary>
        /// <returns></returns>
        private bool ValidatePhone()
        {
            // Phone can't be a null or empty string
            if (txtPhone.Text == null || txtLastName.Text == "")
            {
                return false;
            }
            // Phone must be no more than 11 characters long
            if (txtPhone.Text.Length == 11)
            {
                return true;
            }

            // If Phone is greater than 100 characters, then the method returns false
            return false;
            
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateDepartmentID checks to see if an item was selected from the Department drop down combo box
        /// and returns true if there was and false if there wasn't
        /// </summary>
        /// <returns></returns>
        private bool ValidateDepartmentID()
        {
            // The method will return false if nothing was selected.
            if (cbxDepartment.SelectedItem == null)
            {
                return false;
            }
            else
            {
                // If an item was selected from the Department drop down then method returns true
                return true;
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This button closes the details screeen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel?", "Leaving Employee detail screen.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
