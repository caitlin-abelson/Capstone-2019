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
    /// @Author: Phillip Hansen
    /// @Created 2/12/2019
    /// 
    /// Interaction logic for frmEventDeleteConfirmation.xaml
    /// 
    /// Displays a confirmation window when deleting a specified event, and shows some record information in window to confirm
    /// user satisfaction.
    /// </summary>
    public partial class frmEventDeleteConfirmation : Window
    {
        private LogicLayer.EventManager _eventManager = new LogicLayer.EventManager();
        private Event _newEvent;
        public frmEventDeleteConfirmation(Event purgeEvent)
        {
            InitializeComponent();
            _newEvent = purgeEvent;
            

            this.Title = "Delete Event " + purgeEvent.EventTitle + " created by " + purgeEvent.EmployeeName;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When the DeleteConfirmation window is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDelEventID.Text = _newEvent.EventID.ToString();
            txtDelEventTitle.Text = _newEvent.EventTitle.ToString();

            dateDelEventStart.SelectedDate = _newEvent.EventStartDate.Date;
            dateDelEventEnd.SelectedDate = _newEvent.EventEndDate.Date;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When the 'Cancel' button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When the 'Confirm' button is clicked, and should delete and pass the event into the 'manager' logic to allow purge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _eventManager.DeleteEvent(_newEvent);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not delete event. Is the Event 'Not Approved'?\nIs the setup to the event deleted?");
            }
        }
    }
}
