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
    /// Created by: Kevin Broskow
    /// Created: 2/27/2019
    /// Interaction logic for CreateShop.xaml
    /// </summary>
    public partial class CreateShop : Window
    {
        RoomManager _roomManager = new RoomManager();
        ShopManagerMSSQL _shopManager = new ShopManagerMSSQL();
        Shop newShop = new Shop();
        Shop oldShop = new Shop();
        public CreateShop()
        {
            InitializeComponent();
            this.btnSubmit.Content = "Submit";
            this.Title = "Create Shop";
            this.cboRoomID.Focus();
            this.txtShopID.Visibility = Visibility.Hidden;
            this.lblShopID.Visibility = Visibility.Hidden;
            this.cbActive.Visibility = Visibility.Hidden;
            this.lblActive.Visibility = Visibility.Hidden;
            this.btnEdit.Visibility = Visibility.Hidden;
        }
        
        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //not yet implemented
            MessageBox.Show("This functionality is not yet implemented");
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!validInput())
            {
                return;



            }
            try
            {
                if ((String)this.btnSubmit.Content == "Submit")
                {
                    newShop.RoomID = (int)cboRoomID.SelectedValue;
                    newShop.Name = this.txtName.Text;
                    newShop.Description = this.txtDescription.Text;
                    int shopID = _shopManager.InsertShop(newShop);
                    MessageBox.Show("Add worked.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool validInput()
        {
            if (ValidName())
            {
                if (ValidDescription())
                {
                    if (ValidRoomID())
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid RoomID");
                    }


                    }
                    else
                {
                    MessageBox.Show("Invlaid Description");
                }
                }
                else
                {
                    MessageBox.Show("Invalid Name");
                }
            
        
            return false;
        }

        private bool ValidRoomID()
        {
            if (cboRoomID.SelectedIndex > -1)
            {
                return true;
            }
            
            return false;
        }

        private bool ValidDescription()
        {
            if (txtDescription.Text.Length > 1001 || txtDescription.Text == null || txtDescription.Text =="")
            {
                return false;
            }
            return true;
        }

        private bool ValidName()
        {
            if (txtName.Text == null || txtName.Text == "")
            {
                return false;
            }
                if (txtName.Text.Length > 50)
            {
                return false;
            }
            return true;
        }
    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Room> roomIDs = new List<Room>();
            List<int> roomNums = new List<int>();
            try
            {
                roomIDs = _roomManager.RetrieveRoomList();
                foreach (var room in roomIDs)
                {
                    roomNums.Add(room.RoomID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            foreach (var room in roomNums)
            {
                cboRoomID.Items.Add(room);
            }
        }







        /*Userd for Read and edit form
        public CreateShop(Shop shop)
        {
        
        }
        */



    }
}
