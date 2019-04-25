using System;
using System.Collections;
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
    /// Francis Mingomba
    /// Created: 2019/04/23
    /// 
    /// Interaction logic for FrmCheckoutPromptTwo.xaml
    /// </summary>
    public partial class FrmCheckoutPromptTwo : Window
    {
        private IEmployeeManager _employeeManager;
        private FrmCheckoutPrompt _checkoutPromptOne;
        private List<Employee> _employees;
        private string _filterTxt;

        public FrmCheckoutPromptTwo() : this(null, "") { }

        public FrmCheckoutPromptTwo(FrmCheckoutPrompt caller, string filterTxt)
        {
            _filterTxt = filterTxt;

            _employeeManager = new EmployeeManager();

            _checkoutPromptOne = caller;

            InitializeComponent();

            PopulateWindow();
        }

        #region Setup Logic

        private void PopulateWindow()
        {
            RefreshEmployeeDataGrid();

            txtEmployeeFilter.Text = _filterTxt;
        }

        private void RefreshEmployeeDataGrid()
        {
            _employees = GetEmployees();

            dtgEmployees.ItemsSource = _employees;
        }

        #endregion

        #region Core Logic

        private void BtnSelect_OnClick(object sender, RoutedEventArgs e)
        {
            var employee = (Employee) dtgEmployees.SelectedItem;

            // ... check usage
            if (_checkoutPromptOne == null)
            {
                MessageBox.Show("Unexpected Usage: This is a child window for " +
                                "FrmCheckoutPrompt");
                return;
            }

            // ... make sure employee is not null
            if (employee == null)
            {
                MessageBox.Show("Please select an employee");
                return;
            }

            _checkoutPromptOne.SetEmployee(employee);

            this.Close();
        }

        private void TxtEmployeeFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _filterTxt = txtEmployeeFilter.Text;

            if (_employees != null)
                dtgEmployees.ItemsSource = _employees.Where(x =>
                    x.EmployeeID.ToString().ToLower().Contains(_filterTxt)
                    || x.FirstName.ToLower().Contains(_filterTxt)
                    || x.LastName.ToLower().Contains(_filterTxt)
                    || x.Email.ToLower().Contains(_filterTxt)
                    || x.PhoneNumber.ToLower().Contains(_filterTxt));
        }

        #endregion

        #region External Resource Facing Functions

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = null;

            try
            {
                employees = _employeeManager.SelectAllActiveEmployees();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to retrieve employees\n{e.Message}");
            }

            return employees;
        }

        #endregion
    }
}
