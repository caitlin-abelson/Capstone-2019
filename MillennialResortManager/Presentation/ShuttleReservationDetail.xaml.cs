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
    /// Interaction logic for ShuttleReservationDetail.xaml
    /// </summary>
    public partial class ShuttleReservationDetail : Window
    {

        private IShuttleReservationManager _shuttleReservationManager;
        private IGuestManager _guestManager;
        private IEmployeeManager _employeeManager;
        private ShuttleReservation _selectedShuttleReservation;
        private List<Guest> _guestInfo;
        private List<Employee> _employeeInfo;
     
        private bool _isUpdate = false;

       

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// constructor  to create shuttleReservation with  one parameter.
        /// </summary>
        public ShuttleReservationDetail(IShuttleReservationManager shuttleReservationManager = null, IEmployeeManager employeeManager = null, IGuestManager guestManager = null)
        {
            if (shuttleReservationManager == null)
            {
                shuttleReservationManager = new ShuttleReservationManager();
            }

            if(employeeManager == null)
            {
                employeeManager = new EmployeeManager();
            }
            if(guestManager == null)
            {
                guestManager = new GuestManager();
            }

            _shuttleReservationManager = shuttleReservationManager;
            _employeeManager = employeeManager;
            _guestManager = guestManager;

            _isUpdate = false;
            

            InitializeComponent();
            btnDropoffSubmit.Visibility = Visibility.Collapsed;
           
        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to update the guest first name combobox
        /// </summary>
        private void updateGuestFirstName()
        {
            string lastName = (string) cboGuestLast.SelectedItem;
            if (String.IsNullOrEmpty(lastName))
            {
                cboGuestFirst.ItemsSource = null;
                cboGuestFirst.IsEnabled = false;
               
            }
            else
            {
                cboGuestFirst.ItemsSource = _guestInfo.Where(g => g.LastName == lastName).Select(g => g.FirstName).ToList();
                cboGuestFirst.IsEnabled = true;
                
            }
            cboGuestFirst.SelectedIndex = -1;
           
        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to update the guest phone number combobox
        /// </summary>
        private void updateGuestPhoneNumber()
        {
           
            string lastName = (string)cboGuestLast.SelectedItem;
            if (String.IsNullOrEmpty(lastName))
            {
               
                cboGuestPhoneNumber.ItemsSource = null;
                cboGuestPhoneNumber.IsEnabled = false;
            }
            else
            {
               
                cboGuestPhoneNumber.ItemsSource = _guestInfo.Where(g => g.LastName == lastName).Select(g => g.PhoneNumber).ToList();
                cboGuestPhoneNumber.IsEnabled = true;
            }
          
            cboGuestPhoneNumber.SelectedIndex = -1;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to update the employee first name combobox
        /// </summary>
        private void updateEmployeeFirstName()
        {
            string lastName = (string)cboEmployeeLast.SelectedItem;
            if (String.IsNullOrEmpty(lastName))
            {
                cboEmployeeFirst.ItemsSource = null;
                cboEmployeeFirst.IsEnabled = false;

            }
            else
            {
                cboEmployeeFirst.ItemsSource = _employeeInfo.Where(g => g.LastName == lastName).Select(g => g.FirstName).ToList();
                cboEmployeeFirst.IsEnabled = true;
            }
            cboEmployeeFirst.SelectedIndex = -1;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// constructor  to create shuttleReservation with  three parameters.
        /// </summary>
        public ShuttleReservationDetail(ShuttleReservation selectedShuttleReservation, IShuttleReservationManager shuttleReservationManager = null, IEmployeeManager employeeManager = null, IGuestManager guestManager = null)
        {
            _selectedShuttleReservation = selectedShuttleReservation;

            if(shuttleReservationManager == null)
            {
                shuttleReservationManager = new ShuttleReservationManager();
            }
            if (employeeManager == null)
            {
                employeeManager = new EmployeeManager();
            }
            if (guestManager == null)
            {
                guestManager = new GuestManager();
            }

            _shuttleReservationManager = shuttleReservationManager;
            _employeeManager = employeeManager;
            _guestManager = guestManager;

            InitializeComponent();


            txtPickupLocation.Text = _selectedShuttleReservation.PickupLocation;
            txtDropoffDestination.Text = _selectedShuttleReservation.DropoffDestination;
          
          
            this.Title = "Update Shuttle Reservatiion";
            cboGuestLast.IsEnabled = false;
            cboGuestFirst.IsEnabled = false;
            cboGuestPhoneNumber.IsEnabled = false;
            cboEmployeeLast.IsEnabled = false;
            cboEmployeeFirst.IsEnabled = false;
            if(_selectedShuttleReservation.DropoffDateTime != null)
            {
                btnDropoffSubmit.IsEnabled = false;
            }
       
            _isUpdate = true;
         
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method for the window loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpPickupDate.DisplayDate = DateTime.Today;
               
                var now = DateTime.Now;

                var hours = new List<int>();
                var minutes = new List<string>();
                for (int i = 1; i <= 23; i++)
                {
                    hours.Add(i);

                }
                for (int i = 0; i <= 9; i++)
                {
                    minutes.Add("0" + i);
                }
                for (int i = 10; i <= 59; i++)
                {
                    minutes.Add(i.ToString());
                }

                cboHour.ItemsSource = hours;
              
                cboMinute.ItemsSource = minutes;
                
                cboHour.SelectedItem = now.Hour;
               
                if (now.Minute.ToString().Length == 1)
                {
                    cboMinute.SelectedItem = "0" + now.Minute.ToString();
                }
                else
                {
                    cboMinute.SelectedItem = now.Minute.ToString();
                }

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            try
            {
                _guestInfo = _guestManager.RetrieveAllGuestInfo();
                _employeeInfo = _employeeManager.RetrieveAllEmployeeInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            if (_guestInfo != null)
            {
                cboGuestLast.ItemsSource = _guestInfo.Select(g => g.LastName);
            }
            
            
            updateGuestFirstName();
            updateGuestPhoneNumber();
            cboEmployeeLast.ItemsSource = _employeeInfo.Select(em => em.LastName);
            updateEmployeeFirstName();
            if (!_isUpdate)
            {
                dtpPickupDate.IsTodayHighlighted = true;
                dtpPickupDate.SelectedDate = DateTime.Now;
            }
            else
            {
                cboEmployeeLast.SelectedValue = _selectedShuttleReservation.Employee.LastName;
                updateEmployeeFirstName();
                cboEmployeeFirst.SelectedValue = _selectedShuttleReservation.Employee.FirstName;
                cboEmployeeLast.IsEnabled = true;
                cboGuestLast.SelectedValue = _selectedShuttleReservation.Guest.LastName;
                updateGuestFirstName();
                cboGuestFirst.SelectedValue = _selectedShuttleReservation.Guest.FirstName;
                updateGuestPhoneNumber();
                cboGuestPhoneNumber.SelectedValue = _selectedShuttleReservation.Guest.PhoneNumber;
                cboGuestLast.IsEnabled = cboGuestFirst.IsEnabled = cboGuestPhoneNumber.IsEnabled = false;
                dtpPickupDate.SelectedDate = _selectedShuttleReservation.PickupDateTime;
                cboHour.SelectedItem = _selectedShuttleReservation.PickupDateTime.Hour;
                if (_selectedShuttleReservation.PickupDateTime.Minute.ToString().Length == 1)
                {
                    cboMinute.SelectedItem = "0" + _selectedShuttleReservation.PickupDateTime.Minute.ToString();
                }
                else
                {
                    cboMinute.SelectedItem = _selectedShuttleReservation.PickupDateTime.Minute.ToString();
                }

            }
           
               

        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to save shuttle reservation.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            //validation for empty string
            if (cboHour.Text == "" || cboMinute.Text =="")
            {
                MessageBox.Show("Hour and Minutes can't be Blank");
                return;
            }
           if (dtpPickupDate.Text == "")
            {
                MessageBox.Show("Pickup Date can't be Blank");
                return;
            }

            // make sure they fill out all of the stuff correctly
            if (txtPickupLocation.Text == "")
            {
                MessageBox.Show("Pickup Location can't be Blank");

                txtPickupLocation.Focus();
              
                return;
            }
            if (txtDropoffDestination.Text == "")
            {
                MessageBox.Show("Dropoff Destination can't be Blank");
            
                return;
            }
            
            if (cboGuestLast.Text == "" || cboGuestLast.Text=="" || cboGuestPhoneNumber.Text == "")
            {
                MessageBox.Show("Guest Last,Fist and Phone# can't be Blank");

                return;
            }
           
            if (cboEmployeeLast.Text == "" || cboEmployeeFirst.Text =="")
            {
                MessageBox.Show("Employee Last Name and First Name can't be Blank");



                return;
            }

          
            var hour = (int)cboHour.SelectedValue;

              var minute = int.Parse((String)cboMinute.SelectedValue);
          

            if ( dtpPickupDate.Text == "")
            {
                MessageBox.Show(" Pickup Date  can't be Blank");

                return;
            }
           
            
          var full = new DateTime(dtpPickupDate.SelectedDate.Value.Year, dtpPickupDate.SelectedDate.Value.Month, dtpPickupDate.SelectedDate.Value.Day, hour, minute, 0);
    
            ShuttleReservation newShuttleReservation = new ShuttleReservation()
            {

               

              PickupLocation = txtPickupLocation.Text,
              DropoffDestination = txtDropoffDestination.Text, 
              PickupDateTime = full,
              DropoffDateTime = null,
              EmployeeID = _employeeInfo.Where(g => g.LastName == (string)cboEmployeeLast.SelectedValue && g.FirstName == (string)cboEmployeeFirst.SelectedValue).Select(g => g.EmployeeID).First(),
              GuestID = _guestInfo.Where(g => g.LastName == (string)cboGuestLast.SelectedValue && g.FirstName == (string)cboGuestFirst.SelectedValue).Select(g => g.GuestID).First(),
              
               
            };
            

            if (_isUpdate)
            {
                newShuttleReservation.Active = _selectedShuttleReservation.Active;
                try
                {
                    _shuttleReservationManager.UpdateShuttleReservation(_selectedShuttleReservation, newShuttleReservation);
                    this.DialogResult = true;

                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }

            }
            else
            {

                try
                {


                    if (_shuttleReservationManager.CreateShuttleReservation(newShuttleReservation) == 0)
                    {
                        MessageBox.Show("Shuttler Reservation Saved");
                        this.DialogResult = true;
                    }



                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }

            }

            this.Close();
            
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to close a window.
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit?", "Closing Application", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to call updateGuest firstName and phone number
        /// </summary>

        private void CboGuestLast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGuestFirstName();
            updateGuestPhoneNumber();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to call updateemployee firstName 
        /// </summary>

        private void CboEmployeeLast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateEmployeeFirstName();
        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to submit the dropofftime
        /// </summary>
        private void BtnDropoffSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to mark this as dropped off?", "Dropoff", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    _shuttleReservationManager.UpdateShuttleReservationDropoff(_selectedShuttleReservation);
                    this.DialogResult = true;
                    this.Close();
                }
               
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
