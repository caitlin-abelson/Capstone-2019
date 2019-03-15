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
    /// Author: Dalton Cleveland
    /// Created : 2/21/2019
    /// Interaction logic for CreateMaintenanceWorkOrder.xaml
    /// </summary>
    public partial class CreateMaintenanceWorkOrder : Window
    {
        IMaintenanceWorkOrderManager _maintenanceWorkOrderManager;
        IMaintenanceTypeManager _maintenanceTypeManager;
        IMaintenanceStatusTypeManager _maintenanceStatusTypeManager;
        MaintenanceWorkOrder _existingMaintenanceWorkOrder;
        private List<MaintenanceType> _maintenanceTypes;
        private List<MaintenanceStatusType> _maintenanceStatusTypes;


        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// This constructor is used for Creating a MaintenanceWorkOrder
        /// Initializes connections to our MaintenanceWorkOrderManager
        /// </summary>
        public CreateMaintenanceWorkOrder(IMaintenanceWorkOrderManager maintenanceWorkOrderManager)
        {
            InitializeComponent();
            _maintenanceTypeManager = new MaintenanceTypeManagerMSSQL();
            _maintenanceStatusTypeManager = new MaintenanceStatusTypeManagerMSSQL();
            _maintenanceWorkOrderManager = maintenanceWorkOrderManager;

            try
            {
                _maintenanceTypes = _maintenanceTypeManager.RetrieveAllMaintenanceTypes();
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
            List<string> list1 = new List<string>();
            foreach (var item in _maintenanceTypes)
            {
                list1.Add(item.MaintenanceTypeID);
            }
            cboMaintenanceTypeID.ItemsSource = list1;

            try
            {
                _maintenanceStatusTypes = _maintenanceStatusTypeManager.RetrieveAllMaintenanceStatusTypes();
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
            List<string> list2 = new List<string>();
            foreach (var item in _maintenanceStatusTypes)
            {
                list2.Add(item.MaintenanceStatusID);
            }
            cboStatus.ItemsSource = list2;

            chkComplete.IsEnabled = false;
            chkComplete.IsChecked = true;
            dpDateCompleted.IsEnabled = false;
            dpDateCompleted.Equals(null);
            dpDateRequested.IsEnabled = false;
            dpDateRequested.Equals(DateTime.Now);
            cboStatus.SelectedItem = "Open";
            cboStatus.IsEnabled = false;
            txtComments.IsEnabled = false;
            txtComments.Equals(null);
            _existingMaintenanceWorkOrder = null;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// This constructor is used for Reading and Updating a MaintenanceWorkOrder
        /// </summary>
        public CreateMaintenanceWorkOrder(MaintenanceWorkOrder existingMaintenanceWorkOrder, IMaintenanceWorkOrderManager maintenanceWorkOrderManager)
        {
            InitializeComponent();
            _maintenanceWorkOrderManager = maintenanceWorkOrderManager;
            _maintenanceTypeManager = new MaintenanceTypeManagerMSSQL();
            _maintenanceStatusTypeManager = new MaintenanceStatusTypeManagerMSSQL();


            try
            {
                _maintenanceTypes = _maintenanceTypeManager.RetrieveAllMaintenanceTypes();
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
            List<string> list1 = new List<string>();
            foreach (var item in _maintenanceTypes)
            {
                list1.Add(item.MaintenanceTypeID);
            }
            cboMaintenanceTypeID.ItemsSource = list1;

            try
            {
                _maintenanceStatusTypes = _maintenanceStatusTypeManager.RetrieveAllMaintenanceStatusTypes();
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }

            List<string> list2 = new List<string>();
            foreach (var item in _maintenanceStatusTypes)
            {
                list2.Add(item.MaintenanceStatusID);
            }
            cboStatus.ItemsSource = list2;
            dpDateCompleted.IsEnabled = false;
            dpDateCompleted.Equals(null);
            dpDateRequested.IsEnabled = false;
            dpDateRequested.Equals(DateTime.Now);
            _existingMaintenanceWorkOrder = existingMaintenanceWorkOrder;
            populateFormReadOnly();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// populates all the fields for the form 
        /// </summary>
        private void populateFormReadOnly()
        {
            cboMaintenanceTypeID.SelectedItem = _maintenanceTypes.Find(x => x.MaintenanceTypeID == _existingMaintenanceWorkOrder.MaintenanceTypeID);
            cboMaintenanceTypeID.SelectedItem = _maintenanceTypes.Find(x => x.MaintenanceTypeID == _existingMaintenanceWorkOrder.MaintenanceTypeID);
            dpDateRequested.Text = _existingMaintenanceWorkOrder.DateRequested.ToString("MM/dd/yyyy");

            if(_existingMaintenanceWorkOrder.DateCompleted != null)
            {
              dpDateCompleted.Text = _existingMaintenanceWorkOrder.DateRequested.ToString("MM/dd/yyyy");
            }
            
            txtRequestingEmployeeID.Text = _existingMaintenanceWorkOrder.RequestingEmployeeID.ToString();
            txtWorkingEmployeeID.Text = _existingMaintenanceWorkOrder.WorkingEmployeeID.ToString();
            txtResortPropertyID.Text = _existingMaintenanceWorkOrder.ResortPropertyID.ToString(); 
            txtComments.Text = _existingMaintenanceWorkOrder.Comments;
            txtDescription.Text = _existingMaintenanceWorkOrder.Description;
            chkComplete.IsChecked = _existingMaintenanceWorkOrder.Complete;
            setReadOnly();
            btnSave.Content = "Update";
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Sets the form to read only and hides the buttons which cannot be used in read only mode
        /// </summary>
        private void setReadOnly()
        {
            cboMaintenanceTypeID.IsEnabled = false;
            dpDateRequested.IsEnabled = false;
            dpDateCompleted.IsEnabled = false;
            txtRequestingEmployeeID.IsReadOnly = true;
            txtWorkingEmployeeID.IsReadOnly = true;
            txtResortPropertyID.IsReadOnly = true;
            cboStatus.IsEnabled = false;
            txtComments.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            chkComplete.IsEnabled = false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 1/31/2019
        /// Sets the form to editable and shows the buttons which may need to be used in create/edit mode
        /// </summary>
        private void setEditable()
        {
            cboMaintenanceTypeID.IsEnabled = true;
            dpDateRequested.IsEnabled = false;
            dpDateCompleted.IsEnabled = false;
            txtRequestingEmployeeID.IsReadOnly = false;
            txtWorkingEmployeeID.IsReadOnly = false;
            txtResortPropertyID.IsReadOnly = false;
            cboStatus.IsEnabled = true;
            txtComments.IsReadOnly = false;
            txtDescription.IsReadOnly = false;
            chkComplete.IsEnabled = true;
            btnSave.Content = "Submit";
        }

        /// <summary>
        /// Author: Dalton Cleveland
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
                MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder();
                newMaintenanceWorkOrder.MaintenanceTypeID = (cboMaintenanceTypeID.SelectedItem.ToString());
                newMaintenanceWorkOrder.DateRequested = DateTime.Now;
                newMaintenanceWorkOrder.RequestingEmployeeID = int.Parse(txtRequestingEmployeeID.Text) ;
                newMaintenanceWorkOrder.WorkingEmployeeID = int.Parse(txtWorkingEmployeeID.Text);
                newMaintenanceWorkOrder.ResortPropertyID = int.Parse(txtResortPropertyID.Text);
                newMaintenanceWorkOrder.MaintenanceStatusID = cboStatus.SelectedItem.ToString();
                newMaintenanceWorkOrder.Comments = txtComments.Text;
                newMaintenanceWorkOrder.Description = txtDescription.Text;
                try
                {
                    if (_existingMaintenanceWorkOrder == null)
                    {
                        _maintenanceWorkOrderManager.AddMaintenanceWorkOrder(newMaintenanceWorkOrder);
                        SetError("");
                        MessageBox.Show("Maintenance Work Order Created Successfully: " +
                        "\nMaintenanceTypeID: " + newMaintenanceWorkOrder.MaintenanceTypeID +
                        "\nRequestingEmployeeID: " + newMaintenanceWorkOrder.RequestingEmployeeID +
                        "\nWorkingEmployeeID: " + newMaintenanceWorkOrder.WorkingEmployeeID +
                        "\nResortPropertyID: " + newMaintenanceWorkOrder.ResortPropertyID +
                        "\nMaintenanceStatus: " + newMaintenanceWorkOrder.MaintenanceStatusID +
                        "\nDescription: " + newMaintenanceWorkOrder.Description);
                    }
                    else
                    {
                        newMaintenanceWorkOrder.Complete = (bool)chkComplete.IsChecked;
                        _maintenanceWorkOrderManager.EditMaintenanceWorkOrder(_existingMaintenanceWorkOrder, newMaintenanceWorkOrder);
                        newMaintenanceWorkOrder.DateCompleted = DateTime.Now;
                        SetError("");
                        MessageBox.Show("Maintenance Work Order Updated Successfully: " +
                        "\nOld MaintenanceTypeID: " + _existingMaintenanceWorkOrder.MaintenanceTypeID +
                        "\nOld DateRequested: " + _existingMaintenanceWorkOrder.DateRequested +
                        "\nOld RequestingEmployeeID: " + _existingMaintenanceWorkOrder.RequestingEmployeeID +
                        "\nOld WorkingEmployeeID: " + _existingMaintenanceWorkOrder.WorkingEmployeeID +
                        "\nOld ResortPropertyID: " + _existingMaintenanceWorkOrder.ResortPropertyID +
                        "\nOldMaintenanceStatusID: " + _existingMaintenanceWorkOrder.MaintenanceStatusID +
                        "\nOld Description: " + _existingMaintenanceWorkOrder.Description +
                        "\n" +
                        "\nNew MaintenanceTypeID: " + newMaintenanceWorkOrder.MaintenanceTypeID +
                        "\nNew DateCompleted: " + newMaintenanceWorkOrder.DateCompleted +
                        "\nNew RequestingEmployeeID: " + newMaintenanceWorkOrder.RequestingEmployeeID +
                        "\nNew WorkingEmployeeID: " + newMaintenanceWorkOrder.WorkingEmployeeID +
                        "\nNew ResortPropertyID: " + newMaintenanceWorkOrder.ResortPropertyID +
                        "\nNew MaintenanceStatusID: " + newMaintenanceWorkOrder.MaintenanceStatusID +
                        "\nNew Comments: " + newMaintenanceWorkOrder.Comments +
                        "\nNew Description: " + newMaintenanceWorkOrder.Description);
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
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Sets and displays the error to be showed on screen
        /// </summary>
        private void SetError(string error)
        {
            lblError.Content = error;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// A checklist which validates all the input in the form
        /// </summary>
        private bool ValidateInput()
        {
            if (ValidateMaintenanceTypeID())
            {
                if (ValidateResortPropertyID())
                {
                    if (ValidateRequestingEmployeeID())
                    {
                        if (ValidateMaintenanceStatus())
                        {
                            if (ValidateWorkingEmployeeID())
                            {
                                return true;
                            }
                            else
                            {
                                SetError("INVALID WORKING EMPLOYEE ID");
                            }
                        }
                        else
                        {
                            SetError("INVALID MAINTENANCE STATUS ID");
                        }
                    }
                    else
                    {
                        SetError("INVALID REQUESTING EMPLOYEE ID");
                    }
                }
                else
                {
                    SetError("INVALID RESORT PROPERTY ID");
                }
            }
            else
            {
                SetError("INVALID MAINTENANCE TYPE ID");
            }
            return false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Checks whether the MaintenanceTypeID is valid.
        /// </summary>
        private bool ValidateMaintenanceTypeID()
        {
            if (!(cboMaintenanceTypeID.SelectedIndex >= 0))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Checks whether the ResortPropertyID is valid.
        /// </summary>
        private bool ValidateResortPropertyID()
        {
            int n;
            if (txtResortPropertyID.Text == null || txtResortPropertyID.Text == "" || int.TryParse(txtResortPropertyID.Text, out n) == false)
            {
                return false;
            }

            //Chose a range of 1-999,999. Can be changed as needed
            if (int.Parse(txtResortPropertyID.Text) >= 1 && int.Parse(txtResortPropertyID.Text) <= 999999)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Checks whether the RequestingEmployeeID is valid.
        /// </summary>
        private bool ValidateRequestingEmployeeID()
        {
            int m;
            if (txtRequestingEmployeeID.Text == null || txtRequestingEmployeeID.Text == "" || int.TryParse(txtRequestingEmployeeID.Text, out m) == false)
            {
                return false;
            }

            //Chose a range of 1-999,999. Can be changed as needed
            if (int.Parse(txtRequestingEmployeeID.Text) >= 1 && int.Parse(txtRequestingEmployeeID.Text) <= 999999)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Checks whether the WorkingEmployeeID is valid.
        /// </summary>
        private bool ValidateWorkingEmployeeID()
        {
            int o;
            if (txtWorkingEmployeeID.Text == null || txtWorkingEmployeeID.Text == "" || int.TryParse(txtWorkingEmployeeID.Text, out o) == false)
            {
                return false;
            }

            //Chose a range of 1-999,999. Can be changed as needed
            if (int.Parse(txtWorkingEmployeeID.Text) >= 1 && int.Parse(txtWorkingEmployeeID.Text) <= 999999)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Checks whether the Status is valid.
        /// </summary>
        private bool ValidateMaintenanceStatus()
        {
            if (!(cboStatus.SelectedIndex >= 0))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Cancel is clicked
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
