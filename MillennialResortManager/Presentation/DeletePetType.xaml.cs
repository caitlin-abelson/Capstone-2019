using LogicLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for DeletePetType.xaml
    /// </summary>
    public partial class DeletePetType : Window
    {
        private IPetTypeManager _petTypeManager;



        public DeletePetType(IPetTypeManager petTypeManager = null)
        {
            _petTypeManager = petTypeManager;
            if (_petTypeManager == null)
            {
                _petTypeManager = new PetTypeManager();
            }

            InitializeComponent();
            try
            {
                if (cboPetType.Items.Count == 0)
                {
                    var petTypeID = _petTypeManager.RetrieveAllPetTypes();
                    foreach (var item in petTypeID)
                    {
                        cboPetType.Items.Add(item);
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
            if (cboPetType.SelectedItem == null)
            {
                MessageBox.Show("You must select a pet type.");
            }
            else
            {
                result = true;
            }
            return result;
        }


        public void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (delete())
            {
                try
                {

                    var boxResult = MessageBox.Show("Are you sure you want to Delete this Pet Type?", "Delete Pet Type", MessageBoxButton.YesNo);
                    if (boxResult == MessageBoxResult.Yes)
                    {
                        var result = _petTypeManager.DeletePetType(cboPetType.SelectedItem.ToString());
                        if (result == true)
                        {
                            this.DialogResult = true;
                            MessageBox.Show("Pet Type Deleted.");
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Pet Type could not be deleted");
                            this.DialogResult = false;
                        }

                    }

                }
                //catch (SqlException ex)
                //{
                //    MessageBox.Show(ex.Message + Environment.NewLine + "Pet Type could not be Deleted.");
                //}
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Deleting Pet Type Failed.");
                }

            }



        }
    }
}
