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
    ///  Interaction logic for CreateAppointmentType.xaml
    ///  @Author Craig Barkley
    ///  @Created 2/06/2019
    ///  </summary>
    public partial class CreateAppointmentType : Window
    {

        private IAppointmentTypeManager _appointmentTypeManager;

        private AppointmentType _newAppointmentType; //for edit and add





        public CreateAppointmentType(IAppointmentTypeManager appointmentTypeManager = null)
        {

            _appointmentTypeManager = appointmentTypeManager;
            if(_appointmentTypeManager == null)
            {
                _appointmentTypeManager = new AppointmentTypeManager();
            }
            InitializeComponent();
            this.Title = "Add an Appointment Type";
            this.btnCreate.Content = "Create";
        }




        /// <summary>
        /// Craig Barkley
        /// Created: 02/06/2019
        /// Create new Appointment type
        /// </summary>
        ///
        /// <remarks>
        ///  Adds if the return is true.
        /// </remarks>
        private void BtnAppointmentTypeAction_Click(object sender, RoutedEventArgs e)
        {
            

            if (createNewAppointmentType())
            {
                try
                {
                    var result = _appointmentTypeManager.AddAppointmentType(_newAppointmentType);
                    //add if this returns true
                    if (result == true)
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Appointment Type Added");
                }
            }

        }



        /// <summary>
        /// Craig Barkley
        /// Created: 02/06/2019
        /// Create new Appointment type function
        /// </summary>
        ///
        /// <remarks>
        ///  Validates fields for input data.
        /// </remarks>
        private bool createNewAppointmentType()
        {
            bool result = false;
            if (txtAppointmentTypeID.Text == "" || txtDescription.Text == "")
            {
                MessageBox.Show("You must fill out all fields.");
            }
            else if (txtAppointmentTypeID.Text.Length > 15 || txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your Appointment Type is too long. Try Again.");

            }
            else if (txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your description is too long. Try again.");
            }
            else
            {
                result = true;
                _newAppointmentType = new AppointmentType
                {
                    AppointmentTypeID = txtAppointmentTypeID.Text,
                    Description = txtDescription.Text,

                };
            }
            return result;

        }

















    }
}
