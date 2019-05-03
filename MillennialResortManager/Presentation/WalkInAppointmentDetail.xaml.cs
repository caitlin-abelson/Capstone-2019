using DataObjects;
using LogicLayer;
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

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for AppointmentDetail.xaml
    /// </summary>
    public partial class WalkInAppointmentDetail : Window
    {
        private List<Guest> _guests;
        private IGuestManager _guestManager;
        private IAppointmentManager _appointmentManager;
        private IAppointmentTypeManager _appointmentTypeManager;


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// constructor  to create walkinappointment with three parameters.
        /// </summary>
        public WalkInAppointmentDetail(IGuestManager guestManager = null, IAppointmentManager appointmentManager = null, IAppointmentTypeManager appointmentTypeManager =null)
        {
            
            InitializeComponent();
            _appointmentManager = appointmentManager;
            _appointmentTypeManager = appointmentTypeManager;
            _guestManager = guestManager;
            if (_appointmentManager == null)
            {
                _appointmentManager = new AppointmentManager();
            }
            if (_appointmentTypeManager == null)
            {
                _appointmentTypeManager = new AppointmentTypeManager();
            }
            if (_guestManager == null)
            {
                _guestManager = new GuestManager();
            }
            try
            {
                _guests = _guestManager.ReadAllGuests();
                cboAppointmentTypes.ItemsSource = _appointmentTypeManager.RetrieveAllAppointmentTypes();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            cboEmail.ItemsSource = _guests.Select(g => g.Email);
            cboFirstName.ItemsSource = _guests.Select(g => g.FirstName);
            cboLastName.ItemsSource = _guests.Select(g => g.LastName);
            dtpikStart.SelectedDate = DateTime.Now;
            dtpikStart.DisplayDateStart = DateTime.Now;
            dtpikStart.DisplayDateEnd = DateTime.Now.AddDays(14);
            dtpikEnd.DisplayDateStart = DateTime.Now;
            dtpikEnd.DisplayDateEnd = DateTime.Now.AddDays(28);


        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to submit walkinappointment
        /// </summary>
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(cboFirstName.SelectedValue == null)
            {
                MessageBox.Show("Please select a first name.");
            } else if (cboLastName.SelectedValue == null)
            {
                MessageBox.Show("Please select a last name");
            }else if(cboEmail.SelectedValue == null)
            {
                MessageBox.Show("Please select an email");
            } else if(cboAppointmentTypes.SelectedValue == null)
            {
                MessageBox.Show("Please select an appointment type");
            } else if(dtpikStart.SelectedDate == null)
            {
                MessageBox.Show("Please select a start date");
            } else if(dtpikEnd.SelectedDate == null)
            {
                MessageBox.Show("Please select an end date");
            }
            else
            {
                Appointment newAppointment = new Appointment()
                {
                    AppointmentType = (string)cboAppointmentTypes.SelectedValue,
                    StartDate = dtpikStart.SelectedDate.Value,
                    EndDate = dtpikEnd.SelectedDate.Value,
                    GuestID = _guests.Where(g => g.FirstName == (string)cboFirstName.SelectedValue &&
                    g.LastName == (string)cboLastName.SelectedValue && g.Email == (string)cboEmail.SelectedValue).First().GuestID,
                    Description = ""

                };
                try
                {
                    _appointmentManager.CreateAppointmentByGuest(newAppointment);
                    MessageBox.Show("Appointment created!");
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
            
        }

  
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to call updateGuest firstName and email
        /// </summary>
        private void CboLastName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGuestFirstName();
            updateGuestEmail();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to update the guest first name combobox
        /// </summary>
        private void updateGuestFirstName()
        {
            string lastName = (string)cboLastName.SelectedItem;
            if (String.IsNullOrEmpty(lastName))
            {
                cboFirstName.ItemsSource = null;
                cboFirstName.IsEnabled = false;

            }
            else
            {
                cboFirstName.ItemsSource = _guests.Where(g => g.LastName == lastName).Select(g => g.FirstName).Distinct().ToList();
                cboFirstName.IsEnabled = true;

            }
            cboFirstName.SelectedIndex = -1;

        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to update the guest email address combobox
        /// </summary>
        private void updateGuestEmail()
        {

            string lastName = (string)cboLastName.SelectedItem;
            string firstName = (string)cboFirstName.SelectedItem;
            if (String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(firstName))
            {

                cboEmail.ItemsSource = null;
                cboEmail.IsEnabled = false;
            }
            else
            {

                cboEmail.ItemsSource = _guests.Where(g => g.LastName == lastName && g.FirstName == firstName).Select(g => g.Email).ToList();
                cboEmail.IsEnabled = true;
            }

            cboEmail.SelectedIndex = -1;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to call updateGuest  email
        /// </summary>
        private void CboFirstName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGuestEmail();


        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to call update start and end date
        /// </summary>
        private void DtpikStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            dtpikEnd.IsEnabled = true;
            dtpikEnd.DisplayDateStart = dtpikStart.SelectedDate;
            if (dtpikStart.SelectedDate.HasValue)
            {
                dtpikEnd.DisplayDateEnd = dtpikStart.SelectedDate.Value.AddDays(14);
            }
            else
            {
                dtpikEnd.IsEnabled = false;
            }
            
            dtpikEnd.SelectedDate = dtpikStart.SelectedDate;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to close a window.
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure that you want to quit?", "Quit", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                this.Close();
            }
            
        }
    }
}
