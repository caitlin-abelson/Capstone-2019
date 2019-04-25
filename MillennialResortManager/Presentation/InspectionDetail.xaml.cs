/// <summary>
/// Danielle Russo
/// Created: 2019/02/28
/// 
/// Window that displays Inspection.
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
    /// Interaction logic for InspectionDetail.xaml
    /// </summary>
    public partial class InspectionDetail : Window
    {
        private Inspection newInspection;
        private Inspection selectedInspection;
        private InspectionManager inspectionManager = new InspectionManager();

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/27
        /// 
        /// Contstructor that initializes the Inspection window for "Add" view.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public InspectionDetail(int resortPropertyID)
        {
            InitializeComponent();
            txtResortPropertyID.Text = resortPropertyID.ToString();
            setupEditable();
            this.Title = "Add a New Inspection";
            this.btnPrimaryAction.Content = "Add";
            this.btnSecondaryAction.Content = "Cancel";
        }

        public InspectionDetail(Inspection selectedInspection)
        {
            InitializeComponent();

            this.selectedInspection = selectedInspection;
            setupEditable();
            setupInspection();
            this.Title = "Inspection";
            this.btnPrimaryAction.Content = "Save";
            this.btnSecondaryAction.Content = "Cancel";
        }

        private void setupInspection()
        {
            txtAffiliation.Text = selectedInspection.ResortInspectionAffiliation;
            txtFixNotes.Text = selectedInspection.InspectionFixNotes;
            txtInspectionName.Text = selectedInspection.Name;
            txtProblemNotes.Text = selectedInspection.InspectionProblemNotes;
            txtRating.Text = selectedInspection.Rating;
            txtResortPropertyID.Text = selectedInspection.ResortPropertyID.ToString();
            dtpkrDateInspected.SelectedDate = selectedInspection.DateInspected;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/27
        /// 
        /// Sets up an editiable version of the window, so that
        /// details of an inspection can be edited
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void setupEditable()
        {
            txtInspectionName.IsReadOnly = false;
            txtResortPropertyID.IsReadOnly = true;
            txtAffiliation.IsReadOnly = false;
            dtpkrDateInspected.IsEnabled = true;
            txtProblemNotes.IsReadOnly = false;
            txtFixNotes.IsReadOnly = false;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/28
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
                createNewInspection();
                try
                {
                    var inspectionAdded = inspectionManager.CreateInspection(newInspection);
                    if (inspectionAdded == true)
                    {
                        this.DialogResult = true;
                        MessageBox.Show(newInspection.Name + " added.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Inspection not added.");
                }
                return;
            }
            else if (btnPrimaryAction.Content.ToString() == "Save")
            {
                createNewInspection();
                try
                {
                    var inspectionAdded = inspectionManager.UpdateInspection(selectedInspection, newInspection);
                    if (inspectionAdded)
                    {
                        this.DialogResult = true;
                        MessageBox.Show(newInspection.Name + " updated.");
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
        /// Created: 2019/02/28
        /// 
        /// Creates a new Inspection obj. to be added to the 
        /// database.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void createNewInspection()
        {
            newInspection = new Inspection()
            {
                Name = txtInspectionName.Text,
                ResortInspectionAffiliation = txtAffiliation.Text,
                DateInspected = (DateTime)dtpkrDateInspected.SelectedDate,
                ResortPropertyID = int.Parse(txtResortPropertyID.Text),
                Rating = txtRating.Text,
                InspectionProblemNotes = txtProblemNotes.Text,
                InspectionFixNotes = txtFixNotes.Text
            };
        }
    }
}
