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
    /// Interaction logic for BrowseSponsor.xaml
    /// </summary>
    public partial class SponsorMainWindow : Window
    {
        List<Sponsor> _allSponsors;
        List<Sponsor> _currentSponsors;
        SponsorManager _sponsorManager;
        public SponsorMainWindow()
        {
            InitializeComponent();
            _sponsorManager = new SponsorManager();
            refreshAllSponsors();
            populateSponsors();
        }



        private void refreshAllSponsors()
        {
            try
            {
                _allSponsors = _sponsorManager.SelectAllSponsors();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
            _currentSponsors = _allSponsors;
        }

        private void populateSponsors()
        {
            dgSponsors.ItemsSource = _currentSponsors;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddSponsor_Click(object sender, RoutedEventArgs e)
        {
            var createSponsor= new FrmSponsor();
            createSponsor.ShowDialog();
            refreshAllSponsors();
            populateSponsors();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgSponsors.SelectedIndex != -1)
            {
                try
                {
                    _sponsorManager.DeleteSponsor(((Sponsor)dgSponsors.SelectedItem).SponsorID, ((Sponsor)dgSponsors.SelectedItem).Active);
                    refreshAllSponsors();
                    populateSponsors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Delete that Sponsor\n" + ex.Message);
                }

            }
        }

        private void dgSponsors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yy";
            }

        }

        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            filterSponsors();
        }

        private void filterSponsors()
        {
            string searchTerm = null;

            try
            {
                searchTerm = (txtSearch.Text).ToLower().ToString();
                _currentSponsors = _allSponsors.FindAll(m => m.Name.ToLower().Contains(searchTerm));


                if (txtSearch.Text.ToString() != "")
                {
                    _currentSponsors = _currentSponsors.FindAll(m => m.Name.ToLower().Contains(txtSearch.Text.ToString().ToLower()));
                }

                dgSponsors.ItemsSource = _currentSponsors;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            filterSponsors();
        }

       


        private void dgSponsors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgSponsors.SelectedIndex != -1)
            {
                Sponsor selectedSponsor= new Sponsor();
                try
                {
                    selectedSponsor= _sponsorManager.SelectSponsor(((Sponsor)dgSponsors.SelectedItem).SponsorID);
                    var readUpdateSponsor= new FrmSponsor(selectedSponsor);
                    readUpdateSponsor.ShowDialog();
                    refreshAllSponsors();
                    populateSponsors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that Sponsor\n" + ex.Message);
                }

            }
        }
    }
}
