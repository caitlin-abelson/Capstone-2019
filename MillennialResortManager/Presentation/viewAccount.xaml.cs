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
    /// Interaction logic for ViewAccount.xaml
    /// </summary>
    public partial class ViewAccount : Window
    {
        private List<Member> _members;
        private List<Member> _currentMembers;
        private MemberManagerMSSQL _memberManager = new MemberManagerMSSQL();
        public Member _selectedMember = new Member();
        private string text;

        public ViewAccount()
        {
            InitializeComponent();
            populateMembers();
        }

        public ViewAccount(string filterSearch)
        {
            InitializeComponent();
            

            //Search Box should only contain the filter given
            txtSearch.IsEnabled = false;
            txtSearch.Text = filterSearch;

            //Disable buttons for this specifically constructed window
            btnFilter.IsEnabled = false;
            btnClear.IsEnabled = false;
            btnCancel.IsEnabled = false;


            btnDelete.Content = "Select Member";

            

            try
            {
                _memberManager = new MemberManagerMSSQL();

                //Refresh sponsor list to empty gird
                populateMembers();
                var _filteredMembers = new List<Member>();

                foreach (var item in _memberManager.RetrieveAllMembers().Where(s => s.FirstName.Contains(txtSearch.Text.ToString())))
                {
                    _filteredMembers.Add(item);
                }
                dgMember.ItemsSource = _filteredMembers;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not populate filtered Sponsors");
            }

        }

        public void ViewSelectedRecord()
        {
            var member = (Member)dgMember.SelectedItem;
            var viewMemberForm = new frmAccount(member);
            var result = viewMemberForm.ShowDialog();
            if (result == true)
            {

                try
                {
                    _currentMembers = null;
                    _members = _memberManager.RetrieveAllMembers();

                    if (_currentMembers == null)
                    {
                        _currentMembers = _members;
                    }
                    dgMember.ItemsSource = _currentMembers;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// Populate the members
        /// </summary>
        private void populateMembers()
        {
            try
            {
                
                _members = _memberManager.RetrieveAllMembers();

                if (_currentMembers == null)
                {
                    _currentMembers = _members;
                }
                dgMember.ItemsSource = _currentMembers;

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// When user clicks cancel reload the grids
        /// </summary>
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterMembers();

               
            
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// Modification: Gunardi Saputra
        /// Modified: 04/10/2019
        /// Filters search for the first name of the member and displays the result
        /// </summary>
        public void FilterMembers()
        {
            try
            {
                _currentMembers = _members;

                if (txtSearch.Text.ToString() != "")
                {
                    _currentMembers = _currentMembers.FindAll(s => s.FirstName.ToLower().Contains(txtSearch.Text.ToString().ToLower()));
                   
                }


                if (btnActive.IsChecked == true)
                {
                    _currentMembers = _currentMembers.FindAll(s => s.Active.Equals(btnInActive.IsChecked));

                }
                else if (btnInActive.IsChecked == true)
                {
                    _currentMembers = _currentMembers.FindAll(s => s.Active.Equals(btnActive.IsChecked));
                }





              

                dgMember.ItemsSource = _currentMembers;
                    
                
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>
        /// 
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            _currentMembers = _members;
            dgMember.ItemsSource = _currentMembers;
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>
        private void dgMember_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            

            string headerName = e.Column.Header.ToString();

            if (headerName == "FirstName")
            {
                e.Cancel = true;
            }
            if (headerName == "LastName")
            {
                e.Cancel = true;
            }
            if (headerName == "PhoneNumber")
            {
                e.Cancel = true;
            }
            if (headerName == "Email")
            {
                e.Cancel = true;
            }
            if (headerName == "Password")
            {
                e.Cancel = true;
            }
            
           

        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// when click on add member a new empty form will displays
        /// </summary>

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {

            var createMemberForm = new frmAccount();
            var formResult = createMemberForm.ShowDialog();

            if (formResult == true)
            {
                
                try
                {
                    _currentMembers = null;
                    _members = _memberManager.RetrieveAllMembers();

                    if (_currentMembers == null)
                    {
                        _currentMembers = _members;
                    }
                    dgMember.ItemsSource = _currentMembers;
                    
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.Message);
                }
            }
           
        }
        private void dgMember_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            ViewSelectedRecord();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>
       
        private void btnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            if (btnDelete.Content.Equals("Select Member"))
            {
                if(dgMember.SelectedItem != null)
                {
                    _selectedMember = (Member)dgMember.SelectedItem;
                    if (_selectedMember != null)
                    {
                        this.DialogResult = true;
                    }
                    else
                    {
                        this.DialogResult = false;
                        MessageBox.Show("Must have a Member selected!\nCreate a new one if the Member does not exist yet.");
                    }
                }
                
            }
            else
            {
                try
                {
                    if (((Member)dgMember.SelectedItem).Active)
                    {
                        var result = MessageBox.Show("Are you sure you want to deactivate a member", "Member deactivating.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("Member has been deactivated");
                        }
                        else if (result == MessageBoxResult.No)
                        {

                        }
                    }
                    else
                    {
                        var result = MessageBox.Show("Are you sure you want to delete a member", "Member Account Deleting.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("Member has been deleted");
                        }

                    }
                    var Member = (Member)dgMember.SelectedItem;




                    // Set the record to inactive.
                    _memberManager.DeleteMember(Member);

                    // Refresh the Member List.
                    _currentMembers = null;
                    populateMembers();

                    // Remove the record from the list of Active Members.
                    _currentMembers.Remove(Member);
                    dgMember.Items.Refresh();
                }
                catch (NullReferenceException)
                {

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException);
                }
            }

            
        }

       
    }
}
   

