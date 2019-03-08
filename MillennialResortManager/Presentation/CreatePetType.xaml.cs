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
    /// Interaction logic for CreatePetType.xaml
    /// </summary>
    public partial class CreatePetType : Window
    {
        private IPetTypeManager _petTypeManager;

        private PetType _newPetType; //for edit and add

        private bool _result = false;

        //CreatePetType(IPetTypeManager petTypeManager = null)
        /// <summary>
        /// Constructs a new Pet Type Manager. 
        /// </summary>
        /// <param name="IPetTypeManager petTypeManager">TheCreatePetType constructs the _petTypeManager.</param>
        /// <returns></returns>
        //Method for creating a new Pet
        public CreatePetType(IPetTypeManager petTypeManager = null)
        {
            _petTypeManager = petTypeManager;
            if (_petTypeManager == null)
            {
                _petTypeManager = new PetTypeManager();
            }


            InitializeComponent();

            this.Title = "Add a Pet Type";
            this.btnCreate.Content = "Create";
        }

        /// <summary>
        /// Craig Barkley
        /// Created: 02/06/2019
        /// BtnPetTypeAction_Click
        /// <summary>
        /// Button Type Action click. 
        /// </summary>
        /// <param name="IPetTypeManager petTypeManager">The button BtnPetTypeAction calls AddPetType.</param>
        /// <returns></returns>
        //Method for creating a new Pet
        private void BtnPetTypeAction_Click(object sender, RoutedEventArgs e)
        {
            

            if (createNewPetType())
            {
                try
                {
                    var result = _petTypeManager.AddPetType(_newPetType);
                    //add if this returns true
                    if (result == true)
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Pet Type Added");
                }
            }

        }

        /// <summary>
        /// Craig Barkley
        /// Created: 02/06/2019
        /// Create new Pet type function
        /// </summary>
        ///
        /// <remarks>
        ///  Validates fields for input data.
        /// </remarks>
        private bool createNewPetType()
        {
            bool result = false;
            if (txtPetTypeID.Text == "" || txtDescription.Text == "")
            {
                MessageBox.Show("You must fill out all fields.");
            }
            else if (txtPetTypeID.Text.Length > 15 || txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your Pet Type is too long. Try Again.");

            }
            else if (txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your description is too long. Try again.");
            }
            else
            {
                result = true;
                _newPetType = new PetType
                {
                    PetTypeID = txtPetTypeID.Text,
                    Description = txtDescription.Text,

                };
            }
            return result;

        }












    }
}
