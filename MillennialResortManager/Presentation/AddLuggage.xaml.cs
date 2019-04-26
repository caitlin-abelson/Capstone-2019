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
    /// Interaction logic for AddLuggage.xaml
    /// </summary>
    public partial class AddLuggage : Window
    {
        private LuggageManager luggageManager;
        private GuestManager guestManager;

        public AddLuggage(LuggageManager luggageManager, GuestManager guestManager)
        {
            InitializeComponent();
            this.luggageManager = luggageManager;
            this.guestManager = guestManager;
            List<LuggageStatus> status = null;
            try
            {
                status = luggageManager.RetrieveAllLuggageStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            List<string> statusNames = new List<string>();
            if (status != null)
            {
                foreach (var s in status)
                {
                    statusNames.Add(s.LuggageStatusID);
                }
            }
            
            cboStatus.ItemsSource = statusNames;
            cboStatus.SelectedIndex = 0;
            List<Guest> guests = null;
            try
            {
                guests = guestManager.ReadAllGuests();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            List<int> guestIDs = new List<int>();
            if (guests != null)
            {
                foreach (var g in guests)
                {
                    guestIDs.Add(g.GuestID);
                }
            }
            
            cboGuest.ItemsSource = guestIDs;
            cboGuest.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (luggageManager.AddLuggage(new DataObjects.Luggage() { LuggageID = 999999, GuestID = (int)cboGuest.SelectedItem, Status = (string)cboStatus.SelectedItem}))
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
