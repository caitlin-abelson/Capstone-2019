/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Code for interacting with the window for adding, editing and viewing appointments
/// </summary>
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
    /// Interaction logic for frmAddEditViewAppoitment.xaml
    /// </summary>
    public partial class frmAddEditViewAppointment : Window
    {
        private AppointmentManager _appMgr;
        private List<AppointmentType> _appointmentTypes;
        private List<String> _appointmentTypeIDs;
        private Appointment _appointment;
        private AppointmentGuestViewModel _guest;
        private EditMode _mode;
        private bool inputsGood = false;
        public frmAddEditViewAppointment()
        {
            _appMgr = new AppointmentManager();
            _guest = new AppointmentGuestViewModel()
            {
                GuestID = 0,
                FirstName = "",
                LastName = "",
                Email = ""
            };
            _appointmentTypes = _appMgr.RetrieveAppointmentTypes();
            _appointmentTypeIDs = new List<string>();
            getAppointmentTypeIDs();
            _appointment = new Appointment();

            _mode = EditMode.Add;
            InitializeComponent();
        }

        public frmAddEditViewAppointment(EditMode mode, int appointmentID)
        {
            _mode = mode;
            try
            {
                _appointment = _appMgr.RetrieveAppointmentByID(appointmentID);
            }
            catch (Exception)
            {

                throw;
            }
            InitializeComponent();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// What to do when the window loads
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.cboAppointmentType.ItemsSource = _appointmentTypeIDs;

            if(_mode == EditMode.Add)
            {
                lockControls(false);
                this.Title = "Create Appointment";
                this.btnAddEditView.Content = "Save Appointment";
            }
            else if(_mode == EditMode.Edit)
            {
                lockControls(false);
                this.Title = "Edit Appointment";
                this.btnAddEditView.Content = "Save Appointment";
            }
            else if(_mode == EditMode.View)
            {
                lockControls(true);
                this.Title = "View Appointment";
                this.btnAddEditView.Content = "Edit Appointment";
            }
        }

        private void getAppointmentTypeIDs()
        {
            foreach (var appType in _appointmentTypes)
            {
                _appointmentTypeIDs.Add(appType.AppointmentTypeID);
            }
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Opens a window to select a guest
        /// </summary>
        private void BtnSelectGuest_Click(object sender, RoutedEventArgs e)
        {
            var roomForm = new frmSelectGuestForAppointment(_guest, _appMgr);
            var results = roomForm.ShowDialog();
            if(_guest.GuestID >= 100000 && _guest.FirstName != "" && _guest.LastName != "" && _guest.Email != "")
            {
                this.txtFirstName.Text = _guest.FirstName;
                this.txtLastName.Text = _guest.LastName;
                this.txtEmail.Text = _guest.Email;
            }
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Used to lock or unlock the user controls
        /// </summary>
        private void lockControls(bool readOnly)
        {
            this.cboAppointmentType.IsEnabled = !readOnly;
            this.txtDescription.IsReadOnly = readOnly;
            this.dtpStartTime.IsReadOnly = readOnly;
            this.dtpEndTime.IsReadOnly = readOnly;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Closes the window while doing nothing
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// When the Add, Edit or save button is pressed
        /// </summary>
        private void BtnAddEditView_Click(object sender, RoutedEventArgs e)
        {
            bool results = false;
            if (_mode == EditMode.Add)
            {
                checkInputs();
                if(inputsGood)
                {
                    try
                    {
                        results =_appMgr.CreateAppointmentByGuest(_appointment);
                        if(results)
                        {
                            MessageBox.Show("Appointment Added.");
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (_mode == EditMode.Edit)
            {
                
            }
            else if (_mode == EditMode.View)
            {
                
            }
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Validates the information in the user controls is valid
        /// </summary>
        private void checkInputs()
        {
            if(cboAppointmentType.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Appointment Type");
                inputsGood = false;
            }
            else if(dtpStartTime.Value == null)
            {
                MessageBox.Show("Please Select a Start Date and Time");
                inputsGood = false;
            }
            else if (dtpEndTime.Value == null)
            {
                MessageBox.Show("Please Select a Start Date and Time");
                inputsGood = false;
            }
            else if(string.IsNullOrEmpty(dtpStartTime.ToString()))
            {
                MessageBox.Show("Please Select a Start Date and Time");
                inputsGood = false;
            }
            else if(string.IsNullOrEmpty(dtpEndTime.ToString()))
            {
                MessageBox.Show("Please Select a End Time");
                inputsGood = false;
            }
            else if ((DateTime)dtpStartTime.Value < DateTime.Now.Date)
            {
                MessageBox.Show("Start Date cannot be in the past");
                inputsGood = false;
            }
            else if(DateTime.Compare((DateTime)dtpEndTime.Value, (DateTime)dtpStartTime.Value) < 1)
            {
                MessageBox.Show("End Date must come after Start Date");
                inputsGood = false;
            }
            else if (_guest.GuestID < 100000)
            {
                MessageBox.Show("Select a guest");
                inputsGood = false;
            }
            else
            {
                _appointment.GuestID = _guest.GuestID;
                _appointment.AppointmentType = cboAppointmentType.SelectedItem.ToString();
                _appointment.StartDate = (DateTime)dtpStartTime.Value;
                _appointment.EndDate = (DateTime)dtpEndTime.Value;
                _appointment.Description = txtDescription.Text;
                inputsGood = true;
            }
        }
    }
}
