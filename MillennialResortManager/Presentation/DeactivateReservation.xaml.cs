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
    /// Author: Matt LaMarche
    /// Created : 2/09/2019
    /// Interaction logic for DeactivateReservation.xaml
    /// </summary>
    public partial class DeactivateReservation : Window
    {
        Reservation _reservation;
        IReservationManager _reservationManager;

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/09/2019
        /// This Constructor requires a Reservation and an instance of the IReservationManager 
        /// </summary>
        /// <param name="reservation">The Reservation we want to deactivate</param>
        /// <param name="reservationManager">The ReservationManager instance being used for this program</param>
        public DeactivateReservation(Reservation reservation, IReservationManager reservationManager)
        {

            InitializeComponent();
            _reservationManager = reservationManager;
            _reservation = reservation;
            txtTitleContent.Text = "Are you sure you want to delete this Reservation?";
            txtBodyContent.Text = "Deleting this Item will remove it from our system. If you are unsure whether you should delete this please click cancel and ask your superior";
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/09/2019
        /// Attempts to delete the Reservation in our system when the "delete" button is clicked
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            try
            {
                _reservationManager.DeleteReservation(_reservation.ReservationID, _reservation.Active);
                if (_reservation.Active)
                {
                    message = "This Reservation was deactivated successfully";
                }
                else
                {
                    message = "This Reservation was deleted successfully";
                }
            }
            catch (Exception ex)
            {
                message = "There was an error deleting this Reservation: "+ex.Message;
            }
            MessageBox.Show(message);
            Close();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/09/2019
        /// Closes the window without deleting or deactivating the Reservation
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
