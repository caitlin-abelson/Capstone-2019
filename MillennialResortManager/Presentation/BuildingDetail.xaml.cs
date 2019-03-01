/// <summary>
/// Danielle Russo
/// Created: 2019/01/21
/// 
/// Window that displays Building
/// </summary>
///
/// <remarks>
/// </remarks>
///

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
    /// Interaction logic for BuildingDetail.xaml
    /// </summary>
    public partial class BuildingDetail : Window
    {
        private BuildingManager buildingManager = new BuildingManager();
        private Building newBuilding;
        private Building selectedBuilding;

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Contstructor that initializes the Building window for "Add" view.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public BuildingDetail()
        {
            InitializeComponent();
            setupEditable();
            this.Title = "Add a New Building";
            this.btnPrimaryAction.Content = "Add";
            this.btnSecondaryAction.Content = "Cancel";
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// Constructor that initializes the Building window for "Edit" view.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        public BuildingDetail(Building building)
        {
            InitializeComponent();

            this.selectedBuilding = building;
            setupEditable();
            setupSelectedBuilding();
            this.btnPrimaryAction.Content = "Save";
            this.btnSecondaryAction.Content = "Cancel";
            txtID.IsReadOnly = true;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// Set up window with fields populated with the selected building's
        /// information.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void setupSelectedBuilding()
        {
            txtID.Text = selectedBuilding.BuildingID;
            txtName.Text = selectedBuilding.Name;
            txtAddress.Text = selectedBuilding.Address;
            txtDescription.Text = selectedBuilding.Description;
            cboStatusID.SelectedItem = selectedBuilding.StatusID;

            // TO DO: fill dgShops 
            // TO DO: fill dgRooms
        }

        private void setupReadOnly()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Sets up an editiable version of the window, so that
        /// details of a building can be edited
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void setupEditable()
        {
            txtID.IsReadOnly = false;
            txtName.IsReadOnly = false;
            txtAddress.IsReadOnly = false;
            txtDescription.IsReadOnly = false;
            cboStatusID.IsEnabled = true;
        }


        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// When the btnPrimaryAction button is clicked the information
        /// is either saved or added to the database.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrimaryAction_Click(object sender, RoutedEventArgs e)
        {
            if (btnPrimaryAction.Content.ToString() == "Add")
            {
                createNewBuilding();
                try
                {
                    var buildingAdded = buildingManager.CreateBuilding(newBuilding);
                    if (buildingAdded == true)
                    {
                        this.DialogResult = true;
                        MessageBox.Show(newBuilding.BuildingID + " added.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "New building not saved.");
                }
                return;
            }
            else if (btnPrimaryAction.Content.ToString() == "Save")
            {
                createNewBuilding();
                try
                {
                    var buildingUpdated = buildingManager.UpdateBuilding(selectedBuilding, newBuilding);
                    if (buildingUpdated)
                    {
                        this.DialogResult = true;
                        MessageBox.Show(selectedBuilding.BuildingID + " updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Update not saved.");
                }
                return;
            }
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Creates a new Building obj. to be added to the 
        /// database.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void createNewBuilding()
        {
            // Validate input
            if (validateAllInput())
            {
                // If validation clears, create a new building
                newBuilding = new Building()
                {
                    BuildingID = txtID.Text,
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    Description = txtDescription.Text,
                    StatusID = cboStatusID.SelectedItem.ToString()
                };
            }
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Validates input from all fields.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if all input was validated, false if input was not validated</returns>
        private bool validateAllInput()
        {
            bool result = false;

            // Validate all required fields have been filled out
            if (validateRequiredFields())
            {
                // Validate BuildingID
                if (validateBuildingID())
                {
                    // Validate Name
                    if (validateName())
                    {
                        // Validate Address
                        if (validateAddress())
                        {
                            // Validate Description
                            if (validateDescription())
                            {
                                // Everything was validated!
                                result = true;
                            }
                        }
                    }
                }
            }
            else
            {
                setErrorMessage("Please fill out all fields.");
            }
            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks if description field contains a valid amount of characters.  Displays error message if length is too long.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <returns>True if description is apporpriate length, false if description is too long</returns>
        private bool validateDescription()
        {
            bool result = true;

            // Check for Description length, max is 10000
            if (txtDescription.Text.Length > 1000)
            {
                setErrorMessage("Building description is too long, limit to 250 characters.");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks if address field contains a valid amount of characters.  Displays error message if length is too long.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if address is apporpriate length, false if address is too long</returns>
        private bool validateAddress()
        {
            bool result = true;

            // Max length 150
            if (txtAddress.Text.Length > 150)
            {
                setErrorMessage("Address it too long, limit to 150 characters");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks if name field contains a valid amount of characters.  Displays error message if length is too long.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if name is apporpriate length, false if name is too long</returns>
        private bool validateName()
        {
            bool result = true;

            // Max 150 characters
            if (txtName.Text.Length > 150)
            {
                setErrorMessage("Name is too long, limit to 150 characters");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Danielle Russo, code taken from Matt Lamarche 
        /// Created: 2019/02/04
        /// 
        /// Displays error message
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="message">The message to be displayed</param>
        private void setErrorMessage(string message)
        {
            lblError.Content = message;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks to see if any required fields have been left blank.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <returns>True if all req. fields are filled out, false if any req. fields are blank</returns>
        private bool validateRequiredFields()
        {
            bool result = true;
            if (txtID.Text == "" || txtName.Text == "" || txtAddress.Text == "" || txtDescription.Text == "")
            {
                result = false;
            }

            return result;

        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks if ID field contains a valid amount of characters.  Displays error message if length is too long.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if ID is apporpriate length, false if ID is too long</returns>
        private bool validateBuildingID()
        {
            bool result = true;
            // Check for ID content length, max is 50
            if (txtID.Text.Length > 50)
            {
                setErrorMessage("Building ID is too long, limit to 507  characters.");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// Exits window then the btnSecondaryAction button is clicked.
        /// Any changes are noted saved.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSecondaryAction_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// Opens a new window to add a room to the selected building.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="selectedBuilding">The building that the room will be added to</param>
        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature not yet implemented. Need code from Room classes.");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboStatusID.ItemsSource = buildingManager.RetrieveAllBuildingStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void tabBldInfo_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
