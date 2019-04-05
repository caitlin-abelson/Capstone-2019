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
    /// Created : 3/27/2019
    /// Interaction logic for DeactivateHouseKeepingRequest.xaml
    /// </summary>
    public partial class DeactivateHouseKeepingRequest : Window
    {
        HouseKeepingRequest _houseKeepingRequest;
        IHouseKeepingRequestManager _houseKeepingRequestManager;

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This Constructor requires a HouseKeepingRequest and an instance of the IHouseKeepingRequestManager 
        /// </summary>
        public DeactivateHouseKeepingRequest(HouseKeepingRequest houseKeepingRequest, IHouseKeepingRequestManager houseKeepingRequestManager)
        {

            InitializeComponent();
            _houseKeepingRequestManager = houseKeepingRequestManager;
            _houseKeepingRequest = houseKeepingRequest;
            txtTitleContent.Text = "Are you sure you want to delete this HouseKeepingRequest?";
            txtBodyContent.Text = "Deleting this Item will remove it from our system. If you are unsure whether you should delete this please click cancel and ask your superior";
        }

        /// <summary>
        /// Author: MDalton Cleveland
        /// Created : 3/27/2019
        /// Attempts to delete the HouseKeepingRequest in our system when the "delete" button is clicked
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            try
            {
                _houseKeepingRequestManager.DeleteHouseKeepingRequest(_houseKeepingRequest.HouseKeepingRequestID, _houseKeepingRequest.Active);
                if (_houseKeepingRequest.Active)
                {
                    message = "This HouseKeepingRequest was deactivated successfully";
                }
                else
                {
                    message = "This HouseKeepingRequest was deleted successfully";
                }
            }
            catch (Exception ex)
            {
                message = "There was an error deleting this HouseKeepingRequest" + ex.Message;
            }
            MessageBox.Show(message);
            Close();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Closes the window without deleting or deactivating the HouseKeepingRequest
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
