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
    /// @Created: 4/3/2019
    /// 
    /// Interaction logic for frmAddEventSponsor.xaml
    /// 
    /// Presentation Window for operations in Event Sponsor
    /// </summary>
    public partial class frmAddEventSponsor : Window
    {
        private EventSponsorManager _eventSponsManager;
        private EventSponsor _newEventSponsor;

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Initialize Component method for the page
        /// </summary>
        public frmAddEventSponsor()
        {
            InitializeComponent();

            _eventSponsManager = new EventSponsorManager();

            this.Title = "Add an Event/Sponsor Relationship";
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Button Event Handler to capture inputs and create a new object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEvSponsAdd_Click(object sender, RoutedEventArgs e)
        {
            captureFields();

            try
            {
                _eventSponsManager.CreateEventSponsor(_newEventSponsor);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nInsert for EventSponsor failed.");
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Method to capture the inputs
        /// </summary>
        private void captureFields()
        {

            try
            {
                if(txtEventID.Text == null || txtEventID.Text.Length != 6 || txtSponsID.Text.Length != 6)
                {
                    MessageBox.Show("Input fields must be six digits to be valid.");
                    return;
                }
                else if(!int.TryParse(txtEventID.Text, out int aNumber) || !int.TryParse(txtSponsID.Text, out aNumber))
                {
                    MessageBox.Show("Input fields must be numbers only!");
                    return;
                }

                //Once we are here, input is valid
                else
                {
                    _newEventSponsor = new EventSponsor
                    {
                        EventID = int.Parse(txtEventID.Text),
                        SponsorID = int.Parse(txtSponsID.Text)
                    };
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not capture fields to create a record.");
            }
        }

        private void BtnEventSponsCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
