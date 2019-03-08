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
    /// Craig Barkley
	/// Created: 2019/02/18
    /// Interaction logic for BrowsePetType.xaml
    /// </summary>
    public partial class BrowsePetType : Window
    {
        public List<PetType> _petType;
        public List<PetType> _currentPetType;
        public IPetTypeManager _petTypeManager;


        //  BrowsePetType(IPetTypeManager petTypeManager = null)
        /// <summary>
        /// Method for initializing component retrieves all pet types to itemsSource.        /// </summary>
        /// <param name="IPetTypeManager petTypeManager = null">Provides list of pet types to view.</param>
        /// <returns></returns>
        public BrowsePetType(IPetTypeManager petTypeManager = null)
        {
            _petTypeManager = petTypeManager;
            if (_petTypeManager == null)
            {
                _petTypeManager = new PetTypeManager();
            }
            InitializeComponent();
            try
            {
                _petType = _petTypeManager.RetrieveAllPetTypes("All");
                if (_currentPetType == null)
                {
                    _currentPetType = _petType;
                }
                dgPetTypes.ItemsSource = _currentPetType;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        //  BtnPetTypeAddAction_Click
        /// <summary>
        /// Button for adding a pet type.
        /// </summary>
        /// <param name="">The BtnPetTypeActionDelete calls the RetrieveAllAppointmentTypes("All").</param>
        /// <returns></returns>
        private void BtnPetTypeAddAction_Click(object sender, RoutedEventArgs e)
        {
            var addPetType = new CreatePetType();
            var result = addPetType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentPetType = null;
                    _petType = _petTypeManager.RetrieveAllPetTypes("All");
                    if (_currentPetType == null)
                    {
                        _currentPetType = _petType;
                    }
                    dgPetTypes.ItemsSource = _currentPetType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }                                 
        }

        //  BtnAppointmentTypeActionDelete_Click
        /// <summary>
        /// Button for deleting an appointment Type.
        /// </summary>
        /// <param name="">The BtnAppointmentTypeActionDelete calls the RetrieveAllAppointmentTypes("All").</param>
        /// <returns></returns>

        private void BtnPetTypeActionDelete_Click(object sender, RoutedEventArgs e)
        {
            var deletePetType = new DeletePetType();
            var result = deletePetType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentPetType = null;
                    _petType = _petTypeManager.RetrieveAllPetTypes("All");
                    if (_currentPetType == null)
                    {
                        _currentPetType = _petType;
                    }
                    dgPetTypes.ItemsSource = _currentPetType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }








    }
}

