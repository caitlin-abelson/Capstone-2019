using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class SetupAccessorMock : ISetupAccessor
    {
        private List<Setup> _setups;
        private List<VMSetup> _vmSetups;
        private int _id = 100000;


        public SetupAccessorMock()
        {
            _setups = new List<Setup>();
            _vmSetups = new List<VMSetup>();
        }

        
        /// <summary>
        /// James Heim
        /// Created 2019-03-14
        /// 
        /// Return the list of mock Setups.
        /// </summary>
        /// <returns></returns>
        public List<Setup> SelectAllSetup()
        {
            return _setups;
        }

        /// <summary>
        /// Eduardo
        /// 
        /// Create a new mock setup.
        /// ID is auto-incremented.
        /// </summary>
        /// <remarks>
        /// 
        /// James Heim
        /// Updated 2019-03-15
        /// Added ID incrementing.
        /// 
        /// 
        /// </remarks>
        /// <param name="newSetup"></param>
        /// <returns></returns>
        public int InsertSetup(Setup newSetup)
        {

            // Validation.
            if (newSetup.DateEntered <= DateTime.Now.AddDays(-1))
            {
                // If DateEntered is before today
                throw new ArgumentOutOfRangeException("Date Entered cannot be before today.");
            }
            else if (newSetup.DateEntered >= DateTime.Now.AddDays(1))
            {
                // If DateEntered is after today
                throw new ArgumentOutOfRangeException("Date Entered cannot be after today.");
            }
            else if (newSetup.DateRequired <= DateTime.Now.AddDays(-1))
            {
                // If DateEntered is before today
                throw new ArgumentOutOfRangeException("Date Required cannot be before today.");
            }

            newSetup.SetupID = _id;
            _id++;

            _setups.Add(newSetup);

            return newSetup.SetupID;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-03-14
        /// 
        /// Replace the Setup with the new Setup properties.
        /// </summary>
        /// <param name="newSetup">New Setup.</param>
        /// <param name="oldSetup">Original Setup to be replaced.</param>
        public void UpdateSetup(Setup newSetup, Setup oldSetup)
        {

            // Validate.
            if (newSetup.DateEntered != oldSetup.DateEntered)
            {
                // DateEntered cannot change.
                throw new InvalidOperationException();
            }
            if (newSetup.DateRequired <= DateTime.Now.AddDays(-1))
            {
                // DateRequired cannot be before today.
                throw new ArgumentOutOfRangeException();
            }

            if (newSetup.SetupID == oldSetup.SetupID)
            {
                _setups.Remove(oldSetup);
                _setups.Add(newSetup);
            }
        }

        /// <summary>
        /// James Heim
        /// Created 2019-03-14
        /// 
        /// Retrieve the Setup by the specified ID.
        /// </summary>
        /// <param name="setupID">The Setup ID.</param>
        /// <returns>The matching Setup.</returns>
        public Setup SelectSetup(int setupID)
        {
            Setup setup = null;

            setup = _setups.Find(x => x.SetupID == setupID);

            return setup;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-03-14
        /// 
        /// Delete the Setup with the matching ID.
        /// </summary>
        /// <param name="setupID">The ID of the Setup.</param>
        public void DeleteSetup(int setupID)
        {
            _setups.Remove( _setups.Find(x => x.SetupID == setupID));
        }

        public List<VMSetup> SelectVMSetups()
        {
            return _vmSetups;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-05-03
        /// 
        /// Select all VMSetups where the Date Entered property is specified
        /// by the parameter.
        /// </summary>
        /// <param name="dateEntered"></param>
        /// <returns></returns>
        public List<VMSetup> SelectDateEntered(DateTime dateEntered)
        {
            int eventID = 100000;

            foreach (var setup in _setups)
            {
                _vmSetups.Add(new VMSetup()
                {
                    Comments = setup.Comments,
                    DateEntered = setup.DateEntered,
                    DateRequired = setup.DateRequired,
                    EventID = eventID ++,
                    EventTitle = "Test Event",
                    SetupID = setup.SetupID
                });
            }

            return _vmSetups.Where(x => x.DateEntered == dateEntered).ToList();
        }

        /// <summary>
        /// James Heim
        /// Created 2019-05-03
        /// 
        /// Select all VMSetups where the Date Required property is specified
        /// by the parameter.
        /// </summary>
        /// <param name="dateRequired"></param>
        /// <returns></returns>
        public List<VMSetup> SelectDateRequired(DateTime dateRequired)
        {
            int eventID = 100000;

            foreach (var setup in _setups)
            {
                _vmSetups.Add(new VMSetup()
                {
                    Comments = setup.Comments,
                    DateEntered = setup.DateEntered,
                    DateRequired = setup.DateRequired,
                    EventID = eventID++,
                    EventTitle = "Test Event",
                    SetupID = setup.SetupID
                });
            }

            return _vmSetups.Where(x => x.DateRequired == dateRequired).ToList();
        }

        /// <summary>
        /// James Heim
        /// Created 2019-05-03
        /// 
        /// Select all VMSetups where the Event Title property is specified
        /// by the parameter.
        /// </summary>
        /// <param name="eventTitle"></param>
        /// <returns></returns>
        public List<VMSetup> SelectSetupEventTitle(string eventTitle)
        {
            int eventID = 100000;

            foreach (var setup in _setups)
            {
                _vmSetups.Add(new VMSetup()
                {
                    Comments = setup.Comments,
                    DateEntered = setup.DateEntered,
                    DateRequired = setup.DateRequired,
                    EventID = eventID++,
                    EventTitle = "Test Event",
                    SetupID = setup.SetupID
                });
            }

            return _vmSetups.Where(x => x.EventTitle == eventTitle).ToList();
        }
    }
}
