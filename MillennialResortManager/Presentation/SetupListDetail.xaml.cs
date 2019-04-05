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
    /// Interaction logic for SetupListDetail.xaml
    /// </summary>
    public partial class SetupListDetail : Window
    {
        private SetupListManager _setupListManager;
        private LogicLayer.EventManager _eventManager;
        private SetupManager _setupManager;
        private SetupList _newSetupList;
        private SetupList _oldSetupList;
        private VMSetupList _newVMSetupList;
        private VMSetupList _oldVMSetupList;
        private Setup _setup;
        private List<Setup> _setups;
        private List<Event> _events;
        private List<VMSetupList> _vmSetupList;
        private string _eventName;
        private VMSetupList chosenSetupList;
        private string eventTitle;
        private SetupList chosenSetupList1;
        private VMSetupList eventTitle1;



        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 3/5/19
        /// 
        /// The constructor for creating a SetupList
        /// </summary>
        public SetupListDetail(Setup setup, string eventName)
        {
            InitializeComponent();

            _setupListManager = new SetupListManager();
            _setupManager = new SetupManager();
            _eventManager = new LogicLayer.EventManager();
            _setup = setup;
            _vmSetupList = new List<VMSetupList>();
            _eventName = eventName;

            txtEventName.Text = eventName;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 3/5/19
        /// 
        /// The constructor for updating a SetupList
        /// </summary>
        public SetupListDetail(SetupList setupList, string eventName)
        {
            InitializeComponent();

            _setupListManager = new SetupListManager();
            _setupManager = new SetupManager();
            _eventManager = new LogicLayer.EventManager();


            _oldSetupList = setupList;
            _eventName = eventName;

            txtEventName.Text = eventName;

            readOnlyForm();

        }



        private void readOnlyForm()
        {
            txtComments.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            cbxCompleted.IsEnabled = false;

            txtComments.Text = _oldSetupList.Comments;
            txtDescription.Text = _oldSetupList.Description;

            btnSave.Content = "Update";
        }

        private void editForm()
        {
            txtComments.IsReadOnly = false;
            txtDescription.IsReadOnly = false;
            cbxCompleted.IsEnabled = true;

            txtEventName.IsReadOnly = true;


            btnSave.Content = "Submit";
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Content.Equals("Submit"))
            {
                if (!ValidateInput())
                {
                    return;
                }

                _newSetupList = new SetupList();
                if (_oldSetupList == null)
                {
                    
                    _newSetupList.SetupID = _setup.SetupID;
                    _newSetupList.Description = txtDescription.Text;
                    _newSetupList.Comments = txtComments.Text;
                }
                else
                {
                    _newSetupList.SetupListID = _oldSetupList.SetupListID;
                    _newSetupList.SetupID = _oldSetupList.SetupID;
                    _newSetupList.Completed = cbxCompleted.IsChecked.Value;
                    _newSetupList.Description = txtDescription.Text;
                    _newSetupList.Comments = txtComments.Text;
                }

                try
                {
                    if (_oldSetupList == null)
                    {

                        _setupListManager.InsertSetupList(_newSetupList);

                        MessageBox.Show("SetupList Created: " +
                            "\nEvent Name: " + _eventName +
                            "\nDescription: " + _newSetupList.Description +
                            "\nComments: " + _newSetupList.Comments);

                    }
                    else
                    {
                        _setupListManager.UpdateSetupList(_newSetupList, _oldSetupList);
                        SetError("");
                        MessageBox.Show("Setup List Update Successful: " +
                            "\nEvent Name: " + _eventName +
                            "\n" +
                            "\nNew Completed: " + _newSetupList.Completed +
                            "\nNew Description: " + _newSetupList.Description +
                            "\nNew Comments: " + _newSetupList.Comments +
                            "\n" +
                            "\nOld Completed: " + _oldSetupList.Completed +
                            "\nOld Description: " + _oldSetupList.Description +
                            "\nOld Comments: " + _oldSetupList.Comments);
                    }
                }

                catch (Exception ex)
                {
                    SetError(ex.Message);
                }

                Close();
            }
            else if (btnSave.Content.Equals("Update"))
            {
                editForm();
            }

        }



        private bool ValidateInput()
        {
            if (ValidateDescription())
            {
                if (ValidateComments())
                {
                    return true;
                }
                else
                {
                    SetError("Your comments are invalid. Please try again.");
                }
            }
            else
            {
                SetError("Your Description is invalid. Please try again.");
            }
            return false;
        }

        private void SetError(string error)
        {
            lblError.Content = error;
        }

        private bool ValidateDescription()
        {
            // Description can't be null or empty string
            if (txtDescription.Text == null || txtDescription.Text == "")
            {
                return false;
            }

            // Description must be less than 1000 characters and greater and 1
            if (txtDescription.Text.Length > 1000 || txtDescription.Text.Length < 1)
            {
                return false;
            }

            // If the Description if greater than 1 character and less than 1000
            return true;
        }

        private bool ValidateComments()
        {
            // Comments can't be null or empty string
            if (txtComments.Text == null || txtComments.Text == "")
            {
                return false;
            }

            // Comments must be less than 1000 characters and greater and 1
            if (txtComments.Text.Length > 1000 || txtComments.Text.Length < 1)
            {
                return false;
            }

            // If the Comments if greater than 1 character and less than 1000
            return true;
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
