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
    /// Interaction logic for Read_BrowseOrder.xaml
    /// </summary>
    public partial class Read_BrowseOrder : Window
    {
        private SpecialOrderManagerMSSQL _specialOrderLogic = new SpecialOrderManagerMSSQL();
        private int _order;

        public Read_BrowseOrder(int order)
        {
            InitializeComponent();
            _order = order;
        }


        private void ShowOrderLine_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {


                ShowOrderLine.ItemsSource = _specialOrderLogic.RetrieveOrderLinesByID(_order);


            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }

}
