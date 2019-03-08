using DataObjects;
using LogicLayer;
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

namespace Presentation
{
    /// <summary>
    /// James Heim
    /// Created: 2019/01/21
    /// 
    /// WPF Form for a Supplier and its associated CRUD functionality.
    /// Contains fields for the supplier's name, address, city, state, zip, country,
    /// and details for the contact for the supplier. Contact details are their first
    /// name, last name, phone and email. Also shows the date the Supplier was entered into
    /// the system and an active checkbox.
    /// </summary>
    ///
    /// <remarks>
    /// </remarks>
    public partial class frmSupplier : Window
    {

        ISupplierManager _supplierManager = new SupplierManager();
        Supplier _oldSupplier;
        Supplier _newSupplier;
        Context _context;

        /// <summary>
        /// James Heim
        /// Created: 2019/01/24
        /// 
        /// The default constructor is reserved for instantiating the Create window.
        /// </summary>
        public frmSupplier()
        {
            InitializeComponent();
            _context = Context.Create;
        }

        /// <summary>
        /// James Heim
        /// Created 2019/01/30
        /// 
        /// This constructor is reserved for instantiating the View window.
        /// </summary>
        /// <param name="supplier">The supplier to view details for.</param>
        public frmSupplier(Supplier supplier)
        {
            InitializeComponent();

            _context = Context.View;
            btnPrimary.Content = "Edit";

            _oldSupplier = supplier;

            loadOldSupplier();
            disableEditing();
        }

        /// <summary>
        /// James Heim
        /// Created 2019/01/30
        /// 
        /// Load the properties of the Supplier into the appropriate fields.
        /// </summary>
        private void loadOldSupplier()
        {
            txtCity.Text = _oldSupplier.City;
            txtContactFirstName.Text = _oldSupplier.ContactFirstName;
            txtContactLastName.Text = _oldSupplier.ContactLastName;
            txtCountry.Text = _oldSupplier.Country;
            txtDateAdded.Text = _oldSupplier.DateAdded.ToString();
            txtDescription.Text = _oldSupplier.Description;
            txtEmail.Text = _oldSupplier.Email;
            txtPhoneNumber.Text = _oldSupplier.PhoneNumber;
            txtState.Text = _oldSupplier.State;
            txtStreetAddress.Text = _oldSupplier.Address;
            txtSupplierName.Text = _oldSupplier.Name;
            txtZip.Text = _oldSupplier.PostalCode;

            //chkActive.IsChecked = _oldSupplier.Active;
        }

        /// <summary>
        /// James Heim
        /// Created 2019/01/30
        /// 
        /// Disable user inputs.
        /// </summary>
        private void disableEditing()
        {
            txtCity.IsReadOnly = true;
            txtContactFirstName.IsReadOnly = true;
            txtContactLastName.IsReadOnly = true;
            txtCountry.IsReadOnly = true;
            txtDateAdded.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtPhoneNumber.IsReadOnly = true;
            txtState.IsReadOnly = true;
            txtStreetAddress.IsReadOnly = true;
            txtSupplierName.IsReadOnly = true;
            txtZip.IsReadOnly = true;

            // chkActive.IsEnabled = false;

        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019/01/31
        /// 
        /// Enable user inputs.
        /// </summary>
        private void enableEditing()
        {
            txtCity.IsReadOnly = false;
            txtContactFirstName.IsReadOnly = false;
            txtContactLastName.IsReadOnly = false;
            // Country is locked to USA.
            // txtCountry.IsReadOnly = false;
            txtDescription.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtPhoneNumber.IsReadOnly = false;
            txtState.IsReadOnly = false;
            txtStreetAddress.IsReadOnly = false;
            txtSupplierName.IsReadOnly = false;
            txtZip.IsReadOnly = false;

            //chkActive.IsEnabled = false;
        }

        /// <summary>
        /// James Heim
        /// Created: 2019/01/24
        /// 
        /// Decide what to do when the Primary button is clicked based
        /// on the current context.
        /// </summary>
        ///
        /// <remarks>
        /// James Heim
        /// Updated: 2019/01/24
        /// Added logic for Create.
        /// 
        /// James Heim
        /// Updated 2019/01/30
        /// Added logic for View and Edit views.
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrimary_Click(object sender, RoutedEventArgs e)
        {
            if (_context.Equals(Context.Create)) {
                // Add Button
                try
                {
                    CreateSupplier();
                    _supplierManager.CreateSupplier(_newSupplier);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException);
                }
            } else if (_context.Equals(Context.View))
            {
                // Edit button
                // Enable Edit mode.
                enableEditing();
                _context = Context.Edit;
                btnPrimary.Content = "Save";

            } else if (_context.Equals(Context.Edit))
            {
                // Save Button
                try
                {
                    CreateSupplier();
                    _supplierManager.UpdateSupplier(_newSupplier, _oldSupplier);
                    this.DialogResult = true;
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException);
                }

                return;
            }
        }

        /// <summary>
        /// James Heim
        /// Created: 2019/01/24
        /// 
        /// Create a Supplier from data provided via the user input.
        /// </summary>
        /// <remarks>
        /// James Heim
        /// Updated: 2019/01/30
        /// Moved created Supplier to a varible the button handler can access.
        /// </remarks>
        private void CreateSupplier()
        {
            // Load inputs.
            string supplierName = txtSupplierName.Text.Trim();
            string streetAddress = txtStreetAddress.Text.Trim();
            string city = txtCity.Text.Trim();
            string state = txtState.Text.Trim();
            string zip = txtZip.Text.Trim();
            string country = txtCountry.Text.Trim();
            string contactFirstName = txtContactFirstName.Text.Trim();
            string contactLastName = txtContactLastName.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string email = txtEmail.Text.Trim();
            string description = txtDescription.Text.Trim();

            
            // Check for empty inputs.
            if (supplierName.Length == 0 ||
                streetAddress.Length == 0 ||
                city.Length == 0 ||
                state.Length == 0 ||
                zip.Length == 0 ||
                country.Length == 0 ||
                contactFirstName.Length == 0 ||
                contactLastName.Length == 0 ||
                phoneNumber.Length == 0 ||
                email.Length == 0)
            {
                throw new ApplicationException("Fill out all data.");
            }
            // Phone number validation.
            else if (!Regex.IsMatch(phoneNumber, @"^\(?\d{3}\)?[\s\-]?\d{3}\-?\d{4}$"))
            {
                txtPhoneNumber.Select(0, txtPhoneNumber.Text.Length);
                txtPhoneNumber.Focus();
                throw new ApplicationException("Please enter a valid phone number.");
            }
            // Email validation.
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                txtEmail.Select(0, txtEmail.Text.Length);
                txtEmail.Focus();
                throw new ApplicationException("Enter a valid email.");
            }
            
            else
            {

                SupplierValidator.ValidateEmail(new Supplier() { Email = email });
                SupplierValidator.ValidatePhoneNumber(new Supplier() { PhoneNumber = phoneNumber });
            
                // Create the Supplier.
                try
                {
                    _newSupplier = new Supplier
                    {
                        Name = supplierName,
                        Address = streetAddress,
                        City = city,
                        State = state,
                        PostalCode = zip,
                        Country = country,
                        ContactFirstName = contactFirstName,
                        ContactLastName = contactLastName,
                        PhoneNumber = phoneNumber,
                        Email = email,
                        Description = description
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException, "Error Creating Supplier");
                }
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created: 2019/01/30
        /// 
        /// Operations to perform immediately when the window loads.
        /// Focus the first field in the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSupplierName.Focus();
        }

        /// <summary>
        /// Author: James Heim
        /// Created on: 2019/01/31
        /// 
        /// Closes the window or cancels an edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            switch (_context)
            {
                case Context.Create:
                    DialogResult = true;
                    break;
                case Context.View:
                    DialogResult = false;
                    break;
                    
                case Context.Edit:
                    // Revert to View mode.
                    disableEditing();
                    _context = Context.View;
                    btnPrimary.Content = "Edit";
                    break;
            }
        }
    }

    /// <summary>
    /// Author James Heim
    /// Created 2019/01/31
    /// 
    /// Enumeration to allow switching between Create, View, or Edit.
    /// </summary>
    public enum Context
    {
        Create,
        View,
        Edit
    }
}
