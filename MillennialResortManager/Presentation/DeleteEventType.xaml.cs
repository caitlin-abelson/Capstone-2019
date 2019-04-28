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
    /// Interaction logic for DeleteEventType.xaml
    /// </summary>
    public partial class DeleteEventType : Window
    {
        private IEventTypeManager _eventTypeManager;

        public DeleteEventType(IEventTypeManager eventTypeManager = null)
        {
            _eventTypeManager = eventTypeManager;
            if (_eventTypeManager == null)
            {
                _eventTypeManager = new EventTypeManager();
            }

            InitializeComponent();
            try
            {
                if (cboEventType.Items.Count == 0)
                {
                    var eventTypeID = _eventTypeManager.RetrieveAllEventTypes();
                    foreach (var item in eventTypeID)
                    {
                        cboEventType.Items.Add(item);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private bool delete()
        {
            bool result = false;
            if (cboEventType.SelectedItem == null)
            {
                MessageBox.Show("You must select an event type.");
            }
            else
            {
                result = true;
            }
            return result;
        }





        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (delete())
            {
                try
                {
                    if (_eventTypeManager.DeleteEventType(cboEventType.SelectedItem.ToString())){
                        this.DialogResult = true;
                        MessageBox.Show("Event Type Deleted.");
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Deleting Event Type Failed.");
                }

            }


        }

    }
}
