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
    /// Interaction logic for EditLuggage.xaml
    /// </summary>
    public partial class EditLuggage : Window
    {
        private LuggageManager luggageManager;
        private GuestManager guestManager;
        private Luggage luggage;

        public EditLuggage(LuggageManager manager, Luggage l)
        {
            InitializeComponent();
            luggageManager = manager;
            luggage = l;
            List<LuggageStatus> status = luggageManager.RetrieveAllLuggageStatus();
            List<string> statusNames = new List<string>();
            foreach (var name in status)
            {
                statusNames.Add(name.LuggageStatusID);
            }
            cboStatus.ItemsSource = statusNames;
            if (l.Status.Equals("In Lobby"))
            {
                cboStatus.SelectedIndex = 0;
            }
            else if (l.Status.Equals("In Room"))
            {
                cboStatus.SelectedIndex = 1;
            }
            else
            {
                cboStatus.SelectedIndex = 2;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (luggageManager.EditLuggage(luggage, new Luggage() { LuggageID = 999999, GuestID = luggage.GuestID, Status = (string)cboStatus.SelectedItem}))
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
