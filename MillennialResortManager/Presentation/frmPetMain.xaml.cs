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
    /// /// <summary>
    /// @Author Craig Barkley
    /// 
    /// Pet window for showing all Pets at Resort
    /// 
    /// Interaction logic for frmPetMain.xaml
    /// </summary>
    public partial class frmPetMain : Window
    {
        public frmPetMain()
        {
            InitializeComponent();
        }
        private Pet _pet;

        private PetManager _petManager = new PetManager();

        private List<Pet> _pets;

        private PetTypeManager petTypeManager = new PetTypeManager();

        public frmPetMain(Pet pet)
        {
            _pet = pet;

            InitializeComponent();
            populatePets();
            dgPets.IsEnabled = true;
        }

       

       

        private void TabPet_GotFocus(object sender, RoutedEventArgs e)
        {

        }


        private void dgPets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }


        private void BtnCreatePet_Click(object sender, RoutedEventArgs e)
        {
            var addPet = new frmAddEditPet();
            var result = addPet.ShowDialog();
            if(result == true)
            {
                populatePets();
            }
        }


        private void DgPets_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
           // if(headerName == "PetID") { e.Cancel = true; }
           // if (headerName == "EmployeeID") { e.Cancel = true; 
           

        }



        private void populatePets()
        {
            try
            {
                _pets = _petManager.RetrieveAllPets();
                dgPets.ItemsSource = _pets;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not retrieve the list of Pets." + "\n" + ex.StackTrace);
                
            }
        }

        private void BtnViewPet_Click(object sender, RoutedEventArgs e)
        {
            if (dgPets.SelectedIndex > -1)
            {
                var selectedPet = (Pet)dgPets.SelectedItem;

                if (selectedPet == null)
                {
                    MessageBox.Show("No Selected Pets.");
                }
                else
                {
                    var petDetail = new frmAddEditPet(selectedPet);

                    petDetail.ShowDialog();

                    if (petDetail.DialogResult == true)
                    {
                        populatePets();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a pet to view");
            }
        }

        private void BtnDeletePet_Click(object sender, RoutedEventArgs e)
        {
            Pet currentPet = (Pet)dgPets.SelectedItem;

            if(currentPet == null)
            {
                MessageBox.Show("Please select a pet to delete.");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete the pet?", "Delete Pet", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (_petManager.DeletePet(currentPet.PetID))
                    {
                        MessageBox.Show("Pet deleted");

                    }
                    else
                    {
                        MessageBox.Show("Pet was not deleted");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
            populatePets();
        }

        private void BtnEditPet_Click(object sender, RoutedEventArgs e)
        {
            if (dgPets.SelectedIndex > -1)
            {
                var selectedPet = (Pet)dgPets.SelectedItem;

                if (selectedPet == null)
                {
                    MessageBox.Show("No Selected Pets.");
                }
                else
                {
                    var petDetail = new frmAddEditPet(selectedPet, null, true);

                    petDetail.ShowDialog();

                    if (petDetail.DialogResult == true)
                    {
                        populatePets();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a pet to edit");
            }
        }
    }
}
