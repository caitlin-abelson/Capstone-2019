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
    /// Interaction logic for BrowseReceiving.xaml
    /// </summary>
    public partial class BrowseReceiving : Window
    {

        ReceivingTicketManager _receivingManager = new ReceivingTicketManager();
        SupplierOrderManager _supplierManager = new SupplierOrderManager();

        public BrowseReceiving()
        {
            InitializeComponent();
            dgReceiving.ItemsSource = _receivingManager.retrieveAllReceivingTickets();

        }


        private void dgReceiving_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if(dgReceiving.SelectedIndex < 0)
            {
                MessageBox.Show("You must have a ticket selected");
            }
            else
            {
                ReceivingTicket ticket = (ReceivingTicket)dgReceiving.SelectedItem;
                var viewTicket = new OrderRecieving(ticket);
                viewTicket.ShowDialog();
            }
        }

        private void dgReceiving_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yy";
            }
            if (headerName == "Active")
            {
                e.Cancel = true;
            }
            if(headerName == "ReceivingTicketExceptions")
            {
                e.Cancel = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgReceiving.SelectedIndex < 0)
            {
                MessageBox.Show("You must have a ticket selected");
            }
            else
            {
                ReceivingTicket ticket = (ReceivingTicket)dgReceiving.SelectedItem;
                var viewTicket = new OrderRecieving(ticket);
                viewTicket.ShowDialog();
            }
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgReceiving.SelectedIndex < 0)
                {
                    MessageBox.Show("You must have a ticket selected");
                }
                else
                {
                    ReceivingTicket ticket = (ReceivingTicket)dgReceiving.SelectedItem;
                    _supplierManager.CompleteSupplierOrder(ticket.SupplierOrderID);
                    _receivingManager.deactivateReceivingTicket(ticket.ReceivingTicketID);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR:" + ex.Message);
            }
        }
    }
}
