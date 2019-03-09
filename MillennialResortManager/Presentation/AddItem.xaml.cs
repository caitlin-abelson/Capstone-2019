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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        private SpecialOrderManagerMSSQL _specialOrderLogic = new SpecialOrderManagerMSSQL();
        private int _order;

        public AddItem()
        {


        }

        public AddItem(int order)
        {
            InitializeComponent();
            _order = order;
            setDetails();
        }
        
        private void setDetails()
        {

            SpecialOrderID.Text = _order.ToString();

        }

        private bool TestDataOrder()
        {
            bool valid = true;


           if (QuantityNeed.Text == "")
            {
                MessageBox.Show("Invalid Entry for Description, please try again");
                valid = false;
            }
            else if (InputQTYRec.Text == "")
            {
                MessageBox.Show("Invalid Entry for Description, please try again");
                valid = false;
            }
            
            return valid;
        }

        private void btn_SaveItem_Click(object sender, RoutedEventArgs e)
        {

        }

       
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/03/08
        /// 
        ///Setting the input fields for add or update.//Load the list of employeeId to the combo box
        ///
        /// </summary>
        private void ItemID_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = _specialOrderLogic.listOfitemID();
        }

    }
}
