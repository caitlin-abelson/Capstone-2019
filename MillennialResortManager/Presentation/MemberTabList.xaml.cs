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
    /// Interaction logic for MemberTabList.xaml
    /// </summary>
    public partial class MemberTabList : Window
    {

        private MemberTabManager _memberTabManager = new MemberTabManager();

        public VMTab SelectedItem { get; private set; }

        /// <summary>
        ///  James Heim
        ///  Created 2019-04-26
        ///  
        /// The only constructor passes the MemberID so all
        /// Tabs for that ID can be retrieved.
        /// </summary>
        /// <param name="member"></param>
        public MemberTabList(int memberID)
        {
            InitializeComponent();

            try
            {
                List<MemberTab> memberTabs = _memberTabManager.RetrieveMemberTabsByMemberID(memberID).ToList();

                List <VMTab> vmTabs = new List<VMTab>();

                foreach (var tab in memberTabs)
                {
                    vmTabs.Add(new VMTab()
                    {
                        MemberTabID = tab.MemberTabID,
                        MemberID = tab.MemberID,
                        Active = tab.Active,
                        TotalPrice = tab.TotalPrice,
                        Date = tab.MemberTabLines.First().DatePurchased
                    });

                }

                vmTabs.Reverse();

                dgTabs.ItemsSource = vmTabs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Cast the selected item to a VMTab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgTabs.SelectedItem != null)
            {
                SelectedItem = (VMTab) dgTabs.SelectedItem;
                DialogResult = true;
            }
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Cast the selected item to a VMTab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgTabs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgTabs.SelectedItem != null)
            {
                SelectedItem = (VMTab)dgTabs.SelectedItem;
                DialogResult = true;
            }
        }
    }
}
