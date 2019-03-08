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
    /// Craig Barkley
	/// Created: 2019/01/24
	/// 
    /// Interaction logic for BrowseEventTypes.xaml
    /// </summary>
    public partial class BrowseEventType : Window
    {        
        public List<EventType> _eventType;
        public List<EventType> _currentEventType;
        public IEventTypeManager _eventTypeManager;


        public BrowseEventType(IEventTypeManager eventTypeManager = null)
        {
            _eventTypeManager = eventTypeManager;

            if (_eventTypeManager == null)
            {
                _eventTypeManager = new EventTypeManager();
            }

           


            InitializeComponent();
            try
            {
                _eventType = _eventTypeManager.RetrieveAllEventTypes("All");
                if (_currentEventType == null)
                {
                    _currentEventType = _eventType;
                }
                dgEventTypes.ItemsSource = _currentEventType;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }      
        
        

        private void BtnEventTypeAddAction_Click(object sender, RoutedEventArgs e)
        {
            //An empty constructor allows us to invoke the Event Type Add.
            //form with out having starting data. So we can add it. 

            var addEventType = new CreateEventType();
            var result = addEventType.ShowDialog();
            if(result == true)
            {
                try
                {
                    _currentEventType = null;
                    _eventType = _eventTypeManager.RetrieveAllEventTypes("All");
                    if(_currentEventType == null)
                    {
                        _currentEventType = _eventType;
                    }
                    dgEventTypes.ItemsSource = _currentEventType;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }




       



        private void BtnEventTypeActionDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteEventType = new DeleteEventType();
            var result = deleteEventType.ShowDialog();
            if(result == true)
            {
                try
                {
                    _currentEventType = null;
                    _eventType = _eventTypeManager.RetrieveAllEventTypes("All");
                    if(_currentEventType == null)
                    {
                        _currentEventType = _eventType;
                    }
                    dgEventTypes.ItemsSource = _currentEventType;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
    }
}
