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
    /// Interaction logic for SetupDetail.xaml
    /// </summary>
    public partial class SetupDetail : Window
    {
        private Setup _newSetup;
        private string _newEventName;
        private Setup _oldSetup;
        SetupManager _setupManager;
        LogicLayer.EventManager _eventManager;
        private List<Event> _events;
        bool createFlag = true;
        private List<string> _eventTitles = new List<string>();
        


        /// <summary>
        /// This is the constructor for create
        /// </summary>
        public SetupDetail()
        {
            InitializeComponent();

            _setupManager = new SetupManager();
            _eventManager = new LogicLayer.EventManager();

            try
            {
                _events = _eventManager.RetrieveAllEvents();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // The combobox for all of the event names for the user to choose from
            //cboEventName.ItemsSource = _events;
            // Jared Addition
            cboEventName.Items.Clear();
            if (_events != null)
            {
                foreach (var item in _events)
                {
                    cboEventName.Items.Add(item.EventTitle);
                }
            }
            

            dtpDateEntered.IsEnabled = false;
            btnSetupList.Visibility = Visibility.Hidden;

            dtpDateEntered.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019/03/12
        /// 
        /// The Setup constructor for updating a setup
        /// </summary>
        /// <param name="oldSetup"></param>
        public SetupDetail(Setup oldSetup)
        {
            InitializeComponent();

            _setupManager = new SetupManager();
            _eventManager = new LogicLayer.EventManager();

            try
            {
                _events = _eventManager.RetrieveAllEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }

            // The combobox for all of the event names for the user to choose from
            //cboEventName.ItemsSource = _events;
            // Jared Addition
            cboEventName.Items.Clear();
            foreach (var item in _events)
            {
                cboEventName.Items.Add(item.EventTitle);

                if(item.EventID == oldSetup.EventID)
                {
                    cboEventName.SelectedItem = item.EventTitle;
                }
            }
            _oldSetup = oldSetup;

            readOnlyForm();
            

            dtpDateEntered.IsEnabled = false;
            btnSetupList.Visibility = Visibility.Hidden;

            dtpDateEntered.SelectedDate = DateTime.Now;
        }

        private void readOnlyForm()
        {
            foreach (var item in _events) 
            {
                if(item.EventTitle == (string)cboEventName.SelectedItem)
                {
                    _oldSetup.EventID = item.EventID;
                }
            }
            
            cboEventName.SelectedItem = _oldSetup.EventID;
            dtpDateEntered.SelectedDate = _oldSetup.DateEntered;
            dtpDateRequired.SelectedDate = _oldSetup.DateRequired;
            txtComments.Text = _oldSetup.Comments;

            cboEventName.IsEnabled = false;
            dtpDateEntered.IsEnabled = false;
            dtpDateRequired.IsEnabled = false;
            txtComments.IsReadOnly = true;

            btnSave.Content = "Update";
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/28/19
        /// 
        /// BtnSave submits a new Setup form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Content.Equals("Submit"))
            {
                if (!ValidateInput())
                {
                    return;
                }

                _newSetup = new Setup();

                _newSetup.EventID = _events.ElementAt(cboEventName.SelectedIndex).EventID;
                // Jared Addition
                _newSetup.DateEntered = DateTime.Now; // DateTime.Parse(dtpDateEntered.Text)
                _newSetup.DateRequired = dtpDateRequired.SelectedDate.Value; //DateTime.Parse(dtpDateRequired.Text)
                _newSetup.Comments = txtComments.Text;

                try
                {
                    if(_oldSetup == null)
                    {
                        _setupManager.InsertSetup(_newSetup);

                        _newEventName = _events.ElementAt(cboEventName.SelectedIndex).EventTitle;

                        MessageBox.Show("Setup Created: " +
                            "\nEvent Name: " + _events.ElementAt(cboEventName.SelectedIndex).EventTitle +
                            "\nDate Entered: " + _newSetup.DateEntered.ToString("MM/dd/yyyy") +
                            "\nDate Required: " + _newSetup.DateRequired.ToString("MM/dd/yyyy") +
                            "\nComments: " + _newSetup.Comments);

                    }
                    else
                    {
                        _setupManager.UpdateSetup(_newSetup, _oldSetup);
                        SetError("");
                        MessageBox.Show("Setup Update Successful: " +
                            "\nNew Event: " + _events.ElementAt(cboEventName.SelectedIndex).EventTitle +
                            "\nNew Date Required: " + _newSetup.DateRequired.ToString("MM/dd/yyyy") +
                            "\nNew Comments: " + _newSetup.Comments +
                            
                            "\nOld Event: " + _newEventName +
                            "\nOld Date Required: " + _oldSetup.DateRequired.ToString("MM/dd/yyyy") +
                            "\nOld Comments: " + _oldSetup.Comments
                            );

                    }

                    readOnly();
                    btnSetupList.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    SetError(ex.Message);
                }

            }
            else if (btnSave.Content.Equals("Update"))
            {
                editForm();
            }
        }


        private void editForm()
        {
            cboEventName.IsEnabled = true;
            dtpDateRequired.IsEnabled = true;
            txtComments.IsEnabled = true;

            btnSave.Content = "Submit";
        }

        private void readOnly()
        {
            cboEventName.IsEnabled = false;
            dtpDateRequired.IsEnabled = false;
            txtComments.IsEnabled = false;
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/27/19
        /// 
        /// The error message that will be displayed if there is invalid data entered.
        /// </summary>
        /// <param name="error"></param>
        private void SetError(string error)
        {
            lblError.Content = error;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/27/19
        /// 
        /// This method goes through all of the validation and sees if the DateEntered, DateRequired and Comments
        /// are valid. If they are, an error message is displayed.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            if (true) //ValidateDateEntered()) Don't need to validate a field they will never change Jared Addition
            {
                if (ValidateDateRequired())
                {
                    if (ValidateComments())
                    {
                        return true;
                    }
                    else
                    {
                        SetError("Your comments are too long. Please keep them within the 1000 character limit.");
                    }
                }
                else
                {
                    SetError("The DateRequired is invalid. Please choose a valid date and try again.");
                }
            }

            return false;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/27/19
        /// 
        /// The ValidateDateEntered method makes sure that date is not null, is in the future and is 
        /// earlier than DateRequired
        /// </summary>
        /// <returns></returns>
        private bool ValidateDateEntered()
        {
            dtpDateEntered.SelectedDate = DateTime.Now;

            if (dtpDateEntered.SelectedDate != DateTime.Now)
            {
                return false;
            }

            return true;



        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/27/19
        /// 
        /// The ValidateDateRequired method makes sure that the date is not null, is in the future, and
        /// is later than DateEntered.
        /// </summary>
        /// <returns></returns>
        private bool ValidateDateRequired()
        {
            // The DateRequired can't be left blank and can't be earlier than the current date.
            if (dtpDateRequired.SelectedDate == null || dtpDateRequired.SelectedDate < DateTime.Now)
            {
                return false;
            }
            // The DateRequired must be later than DateEntered
            else if (dtpDateRequired.SelectedDate < dtpDateEntered.SelectedDate 
                && dtpDateEntered.SelectedDate > dtpDateRequired.SelectedDate)
            {
                return false;
            }
            // DateEntered and DateRequired can't be on the same date
            else if (dtpDateRequired.SelectedDate == dtpDateEntered.SelectedDate)
            {
                return false;
            }

            // If all of those validations pass, DateRequired returns true.
            return true;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/27/19
        /// 
        /// Per the Data Dictionary, Comments can be null which is why there is no validation for null 
        /// or empty strings
        /// 
        /// This method only checks to make sure the user does not go past the character length.
        /// </summary>
        /// <returns></returns>
        private bool ValidateComments()
        {
            // Nulls are allowed for comments 
            if(txtComments.Text.Length > 1000)
            {
                return false;
            }

            // If validation passes, Comments returns true.
            return true;
        }

        private void BtnSetupList_Click(object sender, RoutedEventArgs e)
        {
            

            var setupListForm = new SetupListDetail(_newSetup, _newEventName);

            setupListForm.ShowDialog();
        }
    }
}
