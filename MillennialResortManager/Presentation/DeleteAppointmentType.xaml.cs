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
    /// Interaction logic for DeleteAppointmentType.xaml
    /// </summary>
    public partial class DeleteAppointmentType : Window
    {
        private IAppointmentTypeManager _appointmentTypeManager;
       

        public DeleteAppointmentType(IAppointmentTypeManager appointmentTypeManager = null)
        {
            _appointmentTypeManager = appointmentTypeManager;
            if(_appointmentTypeManager == null)
            {
                _appointmentTypeManager = new AppointmentTypeManager();
                
            }
            InitializeComponent();
            try
            {
                if (cboAppointmentType.Items.Count == 0)
                {
                    var appointmentTypeID = _appointmentTypeManager.RetrieveAllAppointmentTypes();
                    foreach (var item in appointmentTypeID)
                    {
                        cboAppointmentType.Items.Add(item);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }            
        }

        private bool delete()
        {
            bool result = false;

            if (cboAppointmentType.SelectedItem == null)
            {
                MessageBox.Show("You must select an appointment type.");

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
                    var result = _appointmentTypeManager.DeleteAppointmentType(cboAppointmentType.SelectedItem.ToString());
                    if (result == true)
                    {
                        var boxResult = MessageBox.Show("Are you sure you want to Delete this Appointment Type?");
                        if(boxResult == MessageBoxResult.OK)
                        {
                            this.DialogResult = true;
                            MessageBox.Show("Appointment Type Deleted.");
                            Close();
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Deleting Appointment Type Failed.");
                }
            }
        }


    }
}
