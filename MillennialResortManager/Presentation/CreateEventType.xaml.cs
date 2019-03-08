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
    ///  Interaction logic for CreateEventType.xaml
    ///  @Author Craig Barkley
    ///  @Created 1/23/2019
    ///  </summary>
    public partial class CreateEventType : Window
    {
        private IEventTypeManager _eventTypeManager;

        private EventType _newType; //for edit and add

        private bool _result = false;



        public CreateEventType(IEventTypeManager eventTypeManager = null)
        {
            _eventTypeManager = eventTypeManager;
            if (_eventTypeManager == null)
            {
                _eventTypeManager  = new EventTypeManager();
            }
            

            InitializeComponent();

            this.Title = "Add an Event Type";
            this.btnCreate.Content = "Create";
        }

        /// <summary>
        /// Craig Barkley
        /// Created: 2019/01/28
        /// Create new event type
        /// </summary>
        ///
        /// <remarks>
        ///  Adds if the return is true.
        /// </remarks>
        private void BtnEventTypeAction_Click(object sender, RoutedEventArgs e)
        {
            createNewEventType();
            if (_result == true)
            {
                try
                {
                    var result = _eventTypeManager.AddEventType(_newType);
                    //add if this returns true
                    if (result == true)
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Event Type Added");
                }
            }

        }

        /// <summary>
        /// Craig Barkley
        /// Created: 2019/01/28
        /// Create new event type function
        /// </summary>
        ///
        /// <remarks>
        ///  Validates fields for input data.
        /// </remarks>
        private bool createNewEventType()
        {
            if (txtEventTypeID.Text == "" || txtDescription.Text == "")
            {
                MessageBox.Show("You must fill out all fields.");
            }
            else if (txtEventTypeID.Text.Length > 15 || txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your Event Type is too long. Try Again.");

            }
            else if (txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your description is too long. Try again.");
            }
            else
            {
                _result = true;
                _newType = new EventType
                {
                    EventTypeID = txtEventTypeID.Text,
                    Description = txtDescription.Text,

                };
            }
            return _result;
            
        }










    }
}

