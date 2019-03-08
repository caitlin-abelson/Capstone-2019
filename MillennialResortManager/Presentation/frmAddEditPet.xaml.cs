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
using Microsoft.Win32;

namespace Presentation
{
    /// <summary>
    /// @Author Craig Barkley
    /// @Created 2/10/2019
    /// 
    /// Interaction logic for frmCreatePet.xaml
    /// 
    /// Presentation Window for the options in Pets
    /// </summary>
    public partial class frmAddEditPet : Window
    {


        private Pet _pet;
        private IPetManager _petManager;
        private Pet _oldPet;
        private Pet _newPet;
        private PetTypeManager _petTypeManager = new PetTypeManager();



        public frmAddEditPet(IPetManager petManager = null)
        {
            //_pet = pet;
            InitializeComponent();
            setEditable();
            if (petManager == null)
            {
                petManager = new PetManager();
            }
            _petManager = petManager;

            this.Title = "New Pet";
            this.btnPetAction.Content = "Create";
        }


        public frmAddEditPet(Pet oldPet, IPetManager petManager = null, bool isEdit = false)
        {
            InitializeComponent();
            _oldPet = oldPet;
            setOldPet();
            //Overloads setEditable
            if (petManager == null)
            {
                petManager = new PetManager();
            }
            _petManager = petManager;
            this.Title = "View Pet Record " + _oldPet.PetName;
            this.btnPetAction.Content = "Save";
            setReadOnly();

            if (isEdit)
            {
                setEditable();
                this.Title = "Edit Pet Record " + _oldPet.PetName;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboPetType.Items.Count == 0)
                {
                    var petTypes = _petTypeManager.RetrieveAllPetTypes();
                    foreach (var item in petTypes)
                    {
                        cboPetType.Items.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Pet Types not found.");
            }
            cboPetGender.Items.Add("Male");
            cboPetGender.Items.Add("Female");
            cboPetGender.Items.Add("Other");
            cboPetGender.SelectedIndex = 0;
            cboPetType.SelectedIndex = 0;
        }


        private void BtnPetCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }


        private void setOldPet()
        {
            txtPetID.Text = _oldPet.PetID.ToString();
            txtPetName.Text = _oldPet.PetName;
            cboPetGender.Text = _oldPet.Gender;
            txtPetSpecies.Text = _oldPet.Species;
            cboPetType.SelectedItem = _oldPet.PetTypeID;
            txtGuestID.Text = _oldPet.GuestID.ToString();
        }

        private void setEditable()
        {
            //pet ID does not change
            txtPetID.IsEnabled = false;
            txtPetName.IsEnabled = true;
            cboPetGender.IsEnabled = true;
            txtPetSpecies.IsEnabled = true;
            cboPetType.IsEnabled = true;
            btnAddPetType.IsEnabled = true;
            btnPetAction.IsEnabled = true;
            txtGuestID.IsEnabled = true;
            btnAddEventType.IsEnabled = true;
            btnAddAppointmentType.IsEnabled = true;
            btnAddPetType.IsEnabled = true;
            btnImageUpload.IsEnabled = true;
        }




        private void setReadOnly()
        {
            //pet ID does not change
            txtPetID.IsEnabled = false;
            txtPetName.IsEnabled = false;
            cboPetGender.IsEnabled = false;
            txtPetSpecies.IsEnabled = false;
            cboPetType.IsEnabled = false;
            txtGuestID.IsEnabled = false;
            btnAddPetType.IsEnabled = false;
            btnPetAction.IsEnabled = false;
            btnAddPetType.IsEnabled = false;
            btnAddEventType.IsEnabled = false;
            btnAddAppointmentType.IsEnabled = false;
            btnImageUpload.IsEnabled = false;
        }


        private bool captureNewPet()
        {
            bool result = false;
            try
            {

                if (txtPetName.Text.Length < 1 || txtPetName.Text.Length > 25)
                {
                    MessageBox.Show("Pet Name must be between 1 and 25 characters");
                }
                else if (cboPetGender.Text.Length < 1 || cboPetGender.Text.Length > 50)
                {
                    MessageBox.Show("Pet Gender must be between 1 and 50 characters.");
                }
                else if (txtPetSpecies.Text.Length < 1 || txtPetSpecies.Text.Length > 50)
                {
                    MessageBox.Show("Pet Species must be between 1 and 50 characters.");
                }
                else if (txtPetSpecies.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Pet Species cannot contain a number");
                }
                else if (cboPetType.SelectedItem == null)
                {
                    MessageBox.Show("Please select Pet Type.");
                }
                else if (int.Parse(txtGuestID.Text) > 999999 || int.Parse(txtGuestID.Text) / 100000 < 1)
                {
                    MessageBox.Show("Guest ID must be a six digit number");
                }
                else
                {
                    _newPet = new Pet()
                    {
                        PetName = txtPetName.Text,
                        Gender = cboPetGender.Text,
                        Species = txtPetSpecies.Text,
                        PetTypeID = cboPetType.Text,
                        GuestID = int.Parse(txtGuestID.Text)
                    };
                    result = true;
                }
            }
            catch (FormatException)
            {

                MessageBox.Show("Guest ID must be a number");
            }
            return result;
        }


        private void BtnPetAction_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title == "New Pet")
            {
                if (captureNewPet())
                {
                    try
                    {
                        _petManager.CreatePet(_newPet);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Creating a new pet has failed, try again." + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                    }
                }
            }
            if (this.btnPetAction.Content.ToString() == "Save")
            {
                if (captureNewPet())
                {
                    try
                    {
                        _petManager.UpdatePet(_oldPet, _newPet);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                    }
                }
            }
        }



        private void BtnPetDelete_Click(object sender, RoutedEventArgs e)
        {

        }

       
        private void BtnImageUpload_Click(object sender, RoutedEventArgs e)
        {
            //Taken from:
            //https://stackoverflow.com/questions/10315188/open-file-dialog-and-select-a-file-using-wpf-controls-and-c-sharp

            // Create OpenFileDialog 
            var dlg = new OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();



            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                imgPet.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }









        private void BtnAddEventType_Click(object sender, RoutedEventArgs e)
        {
            var browse = new BrowseEventType();
            browse.ShowDialog();
        }

        private void BtnAddAppointmentType_Click(object sender, RoutedEventArgs e)
        {
            var browse = new BrowseAppointmentType();
            browse.ShowDialog();
        }

        private void BtnAddPetType_Click(object sender, RoutedEventArgs e)
        {
            var browse = new BrowsePetType();
            browse.ShowDialog();
        }
    }
}
            
