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
    /// Craig Barkley
    /// Created: 02/05/2019
    /// 
    /// Interaction logic for BrowseAppointmentTypes.xaml
    /// </summary>
    public partial class BrowseAppointmentType : Window
    {
        public List<AppointmentType> _appointmentType;
        public List<AppointmentType> _currentAppointmentType;
        public IAppointmentTypeManager _appointmentTypeManager;


        // BrowseAppointmentType(IAppointmentTypeManager appointmentTypeManager = null)
        /// <summary>
        /// Method for retrieving all appointment types.
        /// </summary>
        /// <param name="PetType newPetType">The BrowseAppointmentType calls the RetrieveAllAppointmentTypes.</param>
        /// <returns>dgAppointmentTypes.ItemsSource</returns>
        public BrowseAppointmentType(IAppointmentTypeManager appointmentTypeManager = null)
        {
            _appointmentTypeManager = appointmentTypeManager;
            if(_appointmentTypeManager == null)
            {
                _appointmentTypeManager = new AppointmentTypeManager();
            }
            
            InitializeComponent();
            try
            {

                _appointmentType = _appointmentTypeManager.RetrieveAllAppointmentTypes("All");
                if (_currentAppointmentType == null)
                {
                    _currentAppointmentType = _appointmentType;
                }
                dgAppointmentTypes.ItemsSource = _currentAppointmentType;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        //  BtnAppointmentTypeAddAction_Click
        /// <summary>
        ///Button click event to Add an appointmentType.
        /// </summary>
        /// <param name="PetType newPetType">The BtnAppointmentTypeAddAction calls RetrieveAllAppointmentTypes.</param>
        /// <returns></returns>
        private void BtnAppointmentTypeAddAction_Click(object sender, RoutedEventArgs e)
        {


            var addAppointmentType = new CreateAppointmentType();
            var result = addAppointmentType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentAppointmentType = null;
                    _appointmentType = _appointmentTypeManager.RetrieveAllAppointmentTypes("All");
                    if (_currentAppointmentType == null)
                    {
                        _currentAppointmentType = _appointmentType;
                    }
                    dgAppointmentTypes.ItemsSource = _currentAppointmentType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        //  BtnAppointmentTypeActionDelete_Click
        /// <summary>
        /// Button for deleting an appointment Type.
        /// </summary>
        /// <param name="">The BtnAppointmentTypeActionDelete calls the RetrieveAllAppointmentTypes("All").</param>
        /// <returns></returns>

        private void BtnAppointmentTypeActionDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteAppointmentType = new DeleteAppointmentType();
            var result = deleteAppointmentType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentAppointmentType = null;
                    _appointmentType = _appointmentTypeManager.RetrieveAllAppointmentTypes("All");
                    if (_currentAppointmentType == null)
                    {
                        _currentAppointmentType = _appointmentType;
                    }
                    dgAppointmentTypes.ItemsSource = _currentAppointmentType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }
















    }
}
