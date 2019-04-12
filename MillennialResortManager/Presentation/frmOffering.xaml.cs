using System;
using System.Collections.Generic;
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
using LogicLayer;
using DataObjects;
using WpfPresentation;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for frmOffering.xaml
    /// </summary>
    public partial class frmOffering : Window
    {
        private Offering _offering;
        private IOfferingManager _offeringManager;
        private Employee _employee;
        private LogicLayer.EventManager _eventManager;
        public frmOffering(Employee employee)
        {
            InitializeComponent();
            _employee = employee;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Construtcts the window to do it's crud function
        /// </summary>
        public frmOffering(DataObjects.CrudFunction type, Offering offering, Employee employee)
        {
            InitializeComponent();
            _offering = offering;
            _offeringManager = new OfferingManager();
            _employee = employee;
            _eventManager = new LogicLayer.EventManager();
            try
            {
                List<string> types = _offeringManager.RetrieveAllOfferingTypes();
                cboOfferingType.Items.Clear();
                foreach (var item in types)
                {
                    cboOfferingType.Items.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load Offering Types.");
            }
            if (type == CrudFunction.Retrieve)
            {
                setupReadOnly();
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Sets up read only
        /// </summary>
        private void setupReadOnly()
        {
            txtOfferingDescription.IsEnabled = false;
            txtOfferingPrice.IsEnabled = false;
            cboOfferingType.IsEnabled = false;
            chkOfferingActive.IsEnabled = false;
            btnDelete.Visibility = Visibility.Collapsed;
            btnSubmitOffering.Visibility = Visibility.Collapsed;
            txtOfferingDescription.Text = _offering.Description;
            txtOfferingPrice.Text = _offering.Price.ToString("c");
            cboOfferingType.SelectedItem = _offering.OfferingTypeID;
            chkOfferingActive.IsChecked = _offering.Active;

            if (_offering.Active)
            {
                btnDeactivate.Content = "Deactivate";
            }
            else
            {
                btnDeactivate.Content = "Reactivate";
            }
            if (_employee.EmployeeRoles.Find(x => x.RoleID == "Admin") != null && _offering.Active == false)
            {
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 04/10/2019
        /// Sets up edit
        /// </summary>
        private void setupEdit()
        {
            txtOfferingDescription.IsEnabled = true;
            txtOfferingPrice.IsEnabled = true;
            cboOfferingType.IsEnabled = false;
            chkOfferingActive.IsEnabled = false;
            if (_employee.EmployeeRoles.Find(x => x.RoleID == "Admin") != null && _offering.Active == false)
            {
                btnDelete.Visibility = Visibility.Visible;
            }
            txtOfferingDescription.Text = _offering.Description;
            txtOfferingPrice.Text = _offering.Price.ToString("c");
            cboOfferingType.SelectedItem = _offering.OfferingTypeID;
            chkOfferingActive.IsChecked = _offering.Active;
            btnEditOffering.Visibility = Visibility.Collapsed;
            btnSubmitOffering.Visibility = Visibility.Visible;
            btnSubmitOffering.Content = "Submit";

            if (_offering.Active)
            {
                btnDeactivate.Content = "Deactivate";
            }
            else
            {
                btnDeactivate.Content = "Reactivate";
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Exit page
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Deletes the reccord.
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var form = new frmConfirmAction(CrudFunction.Delete);
            var permission = form.ShowDialog();
            try
            {
                if (permission == true)
                {
                    bool result = _offeringManager.DeleteOfferingByID(_offering.OfferingID);
                    if (result == true)
                    {
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("This " + _offering.OfferingTypeID + " could not be deleted. Services and Events cannot be removed.");
            }
            
        }
        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Deactivates or reactivates the Offering.
        /// </summary>
        private void btnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            if (btnDeactivate.Content.ToString() == "Deactivate")
            {
                try
                {
                    bool result = _offeringManager.DeactivateOfferingByID(_offering.OfferingID);
                    if (result == true)
                    {
                        _offering.Active = false;
                        chkOfferingActive.IsChecked = _offering.Active;
                        btnDeactivate.Content = "Reactivate";
                    }
                    if (_employee.EmployeeRoles.Find(x => x.RoleID == "Admin") != null)
                    {
                        btnDelete.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception)
                {
                    // Do nothing
                }
                
            }
            else if (btnDeactivate.Content.ToString() == "Reactivate")
            {
                try
                {
                    bool result = _offeringManager.ReactivateOfferingByID(_offering.OfferingID);
                    if (result == true)
                    {
                        _offering.Active = true;
                        chkOfferingActive.IsChecked = _offering.Active;
                        btnDeactivate.Content = "Deactivate";
                    }
                    if (_employee.EmployeeRoles.Find(x => x.RoleID == "Admin") != null)
                    {
                        btnDelete.Visibility = Visibility.Collapsed;
                    }
                }
                catch (Exception)
                {
                    // Do nothing
                }
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Validates page input
        /// </summary>
        private bool validatePage()
        {
            bool isValid = false;
            String error = "There are the following errors on the page: \n";
            Regex priceRegex = new Regex(@"^([0-9])*(\.{0,1})*(\d{0,2})$");
            Match match = priceRegex.Match(Decimal.Parse(txtOfferingPrice.Text, System.Globalization.NumberStyles.Currency).ToString());
            if (!cboOfferingType.Text.IsValidOfferingType())
            {
                error += "The OfferingType is not a valid type. It must be between 1-15 characters.\n";
            }
            if (txtOfferingPrice.Text.IsValidPrice() || !match.Success)
            {
                error += "The Price is not valid. It must be a monetary value with 2 decimal places.\n";
            }
            if (txtOfferingDescription.Text == "" || txtOfferingDescription.Text == null || !txtOfferingDescription.Text.IsValidDescription())
            {
                error += "The description must be filled in and must be under 1000 charcters.\n";
            }
            if (error == "There are the following errors on the page: \n")
            {
                isValid = true;
            }
            else
            {
                MessageBox.Show(error);
            }
            return isValid;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 04/04/2019
        /// Takes the user to the given Offerings details.
        /// </summary>
        private void BtnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_offering.OfferingTypeID == "Room")
                {
                    var newObject = _offeringManager.RetrieveOfferingInternalRecordByIDAndType(_offering.OfferingID, _offering.OfferingTypeID);
                    Room newRoom = (Room)newObject;
                    //Event finalEvent = _eventManager.RetrieveEventByID(newEvent.EventID);
                    // names missing, otherwise works
                    var form = new frmAddEditViewRoom(EditMode.View, newRoom.RoomID);
                    form.ShowDialog();
                }
                if (_offering.OfferingTypeID == "Event")
                {
                    var newObject = _offeringManager.RetrieveOfferingInternalRecordByIDAndType(_offering.OfferingID, _offering.OfferingTypeID);
                    Event newEvent = (Event)newObject;
                    //Event finalEvent = _eventManager.RetrieveEventByID(newEvent.EventID);
                    // names missing, otherwise works
                    var form = new frmAddEditEvent(_employee, newEvent);
                    form.ShowDialog();
                }
                if (_offering.OfferingTypeID == "Item")
                {
                    var newObject = _offeringManager.RetrieveOfferingInternalRecordByIDAndType(_offering.OfferingID, _offering.OfferingTypeID);
                    Item newItem = (Item)newObject;
                    var form = new CreateItem(newItem, _employee);
                    form.ShowDialog();
                }
                if (_offering.OfferingTypeID == "Service")
                {
                    MessageBox.Show("Not Implemented Yet.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error retrieving the details. " +ex.Message);
            }
            
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 04/10/2019
        /// Sets up edit
        /// </summary>
        private void BtnEditOffering_Click(object sender, RoutedEventArgs e)
        {
            setupEdit();
        }
        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 04/10/2019
        /// Update Offering
        /// </summary>
        private void BtnSubmitOffering_Click(object sender, RoutedEventArgs e)
        {
            if (validatePage())
            {
                if (btnSubmitOffering.Content.ToString() == "Submit")
                {
                    try
                    {
                        Offering newOffering = new Offering(_offering.OfferingID,
                            cboOfferingType.SelectedItem.ToString(),
                            _employee.EmployeeID,
                            txtOfferingDescription.Text,
                            Decimal.Parse(txtOfferingPrice.Text, System.Globalization.NumberStyles.Currency),
                            (bool)chkOfferingActive.IsChecked);
                        var result = _offeringManager.UpdateOffering(_offering, newOffering);
                        if (result == true)
                        {
                            MessageBox.Show("Update successful");
                            this.DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("Update Failed. Try again.");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The Offering could not be updated at this time.");
                    }
                }
            }
            else
            {

            }
            
        }
    }
}
