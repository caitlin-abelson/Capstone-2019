/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Code for interacting with the window for selcting a guest for an appointment
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
    /// Interaction logic for frmSelectGuestForAppointment.xaml
    /// </summary>
    public partial class frmSelectGuestForAppointment : Window
    {
        private AppointmentGuestViewModel _agvm;
        private List<AppointmentGuestViewModel> _guestViewModels;
        private IAppointmentManager _apptMrg;
        public frmSelectGuestForAppointment(AppointmentGuestViewModel agvm, IAppointmentManager apptMrg)
        {
            _agvm = agvm;
            _apptMrg = apptMrg;
            try
            {
                _guestViewModels = _apptMrg.RetrieveGuestList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Handles the interacting when the Select Guest Button is pressed
        /// </summary>
        private void BtnSelectGuest_Click(object sender, RoutedEventArgs e)
        {
            bool guestFound = false;
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Enter a first name");
            }
            else if(txtLastName.Text == "")
            {
                MessageBox.Show("Enter a last name");
            }
            else if(txtEmail.Text == "")
            {
                MessageBox.Show("Enter a email");
            }
            else
            {
                foreach (var guest in _guestViewModels)
                {
                    if(guest.FirstName == txtFirstName.Text && guest.LastName == txtLastName.Text && guest.Email == txtEmail.Text)
                    {
                        _agvm.FirstName = guest.FirstName;
                        _agvm.LastName = guest.LastName;
                        _agvm.Email = guest.Email;
                        _agvm.GuestID = guest.GuestID;
                        guestFound = true;
                        break;
                    }
                }
            }
            if(guestFound)
            {
                MessageBox.Show("Guest Found");
                this.Close();
            }
            else
            {
                MessageBox.Show("No guest found");
                this.Close();
            }
        }
    }
}
