using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using DataObjects;

namespace Presentation
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/21
    /// 
    /// Interaction logic for FrmCheckoutPrompt.xaml
    /// </summary>
    public partial class FrmCheckoutPrompt : Window
    {
        private ResortVehicle _resortVehicle;
        private Employee _checkingOutEmployee;
        private FrmResortVehicleCheckout _caller;

        public FrmCheckoutPrompt() : this(new FrmResortVehicleCheckout(), null) { }

        public FrmCheckoutPrompt(FrmResortVehicleCheckout caller, ResortVehicle vehicle)
        {
            _resortVehicle = vehicle;

            _caller = caller;

            InitializeComponent();

            PopulatePrompt();  
        }

        #region Core Logic

        private void PopulatePrompt()
        {
            txtVehicleDetails.Text = $"VEHICLE ID: {_resortVehicle.Id}\n" +
                                     $"MAKE: {_resortVehicle.Make}\n" +
                                     $"MODEL: {_resortVehicle.Model}\n";
        }

        private void BtnEmployeeId_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidateFields(out string errorStr))
            {
                string employeeId = txtEmployeeId.Text;
                var promptTwo = new FrmCheckoutPromptTwo(caller: this, filterTxt: employeeId);
                promptTwo.ShowDialog();
            }
            else
            {
                MessageBox.Show(errorStr);
            }
        }

        private void BtnCheckout_OnClick(object sender, RoutedEventArgs e)
        {
            if (_caller == null)
            {
                MessageBox.Show("Unexpected usage: This form is to be " +
                                "primarily used by FrmResortCheckout");
                return;
            }   

            if (!ValidateDate()) return;

            ResortVehicleCheckout vehicleCheckout;

            try
            {
                vehicleCheckout = new ResortVehicleCheckout
                {
                    EmployeeId = _checkingOutEmployee.EmployeeID,
                    ResortVehicleId = _resortVehicle.Id,
                    DateCheckedOut = DateTime.Now,
                    DateExpectedBack = (DateTime)dtReturnDate.SelectedDate
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
                return;
            }        

            _caller.PerformVehicleCheckout(vehicleCheckout);

            this.Close();
        }

        private bool ValidateDate()
        {
            bool result = true;

            if (dtReturnDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a return date");
                result = false;
            }

            if (dtReturnDate.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Date cannot be in the past");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/23
        ///
        /// Primarily used by FrmCheckoutPromptTwo
        /// </summary>
        /// <param name="employee"></param>
        public void SetEmployee(Employee employee)
        {
            _checkingOutEmployee = employee;

            PopulatePrompt();

            PopulateEmployeeFields();

            EnableCheckoutButton();
        }

        public bool ValidateFields(out string errorStr)
        {
            errorStr = "";
            if (txtEmployeeId.Text.Length == 0) { /* this is acceptable */ }
            else if (txtEmployeeId.Text.Length > 8)
                errorStr = "Invalid Employee Id"; // check length to avoid integer overflow
            else if (!int.TryParse(txtEmployeeId.Text, out int n))
                errorStr = "Employee ID must be a number";
            else if (n < 0)
                errorStr = "Employee ID cannot be less than 0";

            bool success = string.IsNullOrEmpty(errorStr);

            return success;
        }

        #endregion

        #region Helpers

        private void PopulateEmployeeFields()
        {
            txtEmployeeId.Text = _checkingOutEmployee.EmployeeID.ToString();

            txtFirstName.Text = _checkingOutEmployee.FirstName;

            txtLastName.Text = _checkingOutEmployee.LastName;
        }

        private void EnableCheckoutButton()
        {
            btnCheckout.IsEnabled = true;
        }

        private void TxtEmployeeId_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            btnCheckout.IsEnabled = false;

            txtFirstName.Text = "";

            txtLastName.Text = "";
        }

        #endregion
    }
}
