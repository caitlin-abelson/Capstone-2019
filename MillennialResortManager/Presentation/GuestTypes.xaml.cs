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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for GuestTypes.xaml
    /// </summary>
    public partial class GuestTypes : Window
    {
        public List<GuestType> _guests;
        public List<GuestType> _currentGuests;
        IGuestTypeManager guestManager;

        /// <summary>
        /// Loads the datagrid with the guest type table
        /// </summary>
        public GuestTypes()
        {
            InitializeComponent();

            guestManager = new GuestTypeManager();
            try
            {
                _guests = guestManager.RetrieveAllGuestTypes("All");
                if (_currentGuests == null)
                {
                    _currentGuests = _guests;
                }
                dgGuests.ItemsSource = _currentGuests;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Opens up the add window and updates the datagrid if guest type was created successfully
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addGuest = new AddGuestType();
            var result = addGuest.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentGuests = null;
                    _guests = guestManager.RetrieveAllGuestTypes("All");
                    if (_currentGuests == null)
                    {
                        _currentGuests = _guests;
                    }
                    dgGuests.ItemsSource = _currentGuests;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Opens up the delete window and updates the datagrid if guest type was deleted successfully
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteGuestType = new DeleteGuestType();
            var result = deleteGuestType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentGuests = null;
                    _guests = guestManager.RetrieveAllGuestTypes("All");
                    if (_currentGuests == null)
                    {
                        _currentGuests = _guests;
                    }
                    dgGuests.ItemsSource = _currentGuests;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
