using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
namespace DataAccessLayer
{
  
    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/03/20
    /// 
    /// ShuttleReservationAccessorMock to be used for testing
    /// 
    /// the class ShuttleReservationAccessorMock deriveds IShuttleReservationManager
    /// </summary>
    public class ShuttleReservationAccessorMock : IShuttleReservationAccessor
    {
        private List<ShuttleReservation> _shuttleReservation;
        private List<int> _allShuttleReservations;

        public ShuttleReservationAccessorMock()
        {
            _shuttleReservation = new List<ShuttleReservation>();
            _shuttleReservation.Add(new ShuttleReservation() { ShuttleReservationID= 100000,GuestID= 100000, EmployeeID = 100000,PickupLocation="1840 Las Vegas Blvd",DropoffDestination="840 Broadway Street",PickupDateTime=  new DateTime(2008, 11, 11),DropoffDateTime= new DateTime(2008, 11, 12), Active = true });
            _shuttleReservation.Add(new ShuttleReservation() { ShuttleReservationID = 100001, GuestID = 100001, EmployeeID = 100001, PickupLocation = "2050 Blvd", DropoffDestination = "690 Broadway Street", PickupDateTime = new DateTime(2008, 11, 12), DropoffDateTime = new DateTime(2008, 11, 13), Active = true });
            
            _allShuttleReservations = new List<int>();
            foreach (var item in _shuttleReservation)
            {
                _allShuttleReservations.Add(item.ShuttleReservationID);
            }
        }
        
        public void DeactivateShuttleReservation(int shuttleReservationID)
        {
            bool searchedShuttleReservation = false;


            foreach (var item in _shuttleReservation)
            {
                if (item.ShuttleReservationID == shuttleReservationID)
                {



                    searchedShuttleReservation = true;

                    break;
                }
            }
            if (!searchedShuttleReservation)
            {
                throw new ApplicationException("ShuttleReservation was not found with this ID");
            }
        }

        

      
        public int InsertShuttleReservation(ShuttleReservation newShuttleReservation)
        {
            _shuttleReservation.Add(newShuttleReservation);
            return newShuttleReservation.ShuttleReservationID;
        }
       
        public List<ShuttleReservation> RetrieveActiveShuttleReservations()
        {
            return _shuttleReservation;
        }

       
            
        public List<ShuttleReservation> RetrieveAllShuttleReservations()
        {
            return _shuttleReservation;
            
        }
       

        public List<int> RetrieveEmployeeIDs()
        {
            
            List<int> employeeIds = new List<int>();
            return employeeIds;
        }

        public List<int> RetrieveGuestIDs()
        {
           
            List<int> guestIds = new List<int>();
            return guestIds;
        }

    

        public List<ShuttleReservation> RetrieveInactiveShuttleReservations()
        {
           
            return _shuttleReservation;
        }
       

       
      

        public ShuttleReservation RetrieveShuttleReservationByShuttleReservationID(int shuttleReservationID)
        {
            ShuttleReservation shuttleReservation = new ShuttleReservation();
            shuttleReservation = _shuttleReservation.Find(b => b.ShuttleReservationID == shuttleReservationID);
            if (shuttleReservation.Equals(shuttleReservation))
            {
                throw new ArgumentException("ShuttleReservationID doesn't match.");
            }

            return shuttleReservation;
        }

     
        public void UpdateShuttleReservation(ShuttleReservation oldShuttleReservation, ShuttleReservation newShuttleReservation)
        {
            foreach (var shuttleReservation in _shuttleReservation)
            {
                if (shuttleReservation.ShuttleReservationID == oldShuttleReservation.ShuttleReservationID)
                {
                    shuttleReservation.ShuttleReservationID = newShuttleReservation.ShuttleReservationID;
                    shuttleReservation.GuestID = newShuttleReservation.GuestID;
                    shuttleReservation.EmployeeID = newShuttleReservation.EmployeeID;
                    shuttleReservation.PickupLocation = newShuttleReservation.PickupLocation;
                    shuttleReservation.DropoffDestination = newShuttleReservation.DropoffDestination;
                    shuttleReservation.PickupDateTime = newShuttleReservation.PickupDateTime;
                   // shuttleReservation.DropoffDateTime = newShuttleReservation.DropoffDateTime;


                }
            }
        }

        public void UpdateShuttleReservationDropoff(ShuttleReservation oldShuttleReservation)
        {
            foreach(var shuttleReservation in _shuttleReservation)
            {
                if(shuttleReservation.ShuttleReservationID == oldShuttleReservation.ShuttleReservationID)
                {
                    shuttleReservation.ShuttleReservationID = oldShuttleReservation.ShuttleReservationID;
                    shuttleReservation.GuestID = oldShuttleReservation.GuestID;
                    shuttleReservation.EmployeeID = oldShuttleReservation.EmployeeID;
                    shuttleReservation.PickupLocation = oldShuttleReservation.PickupLocation;
                    shuttleReservation.DropoffDestination = oldShuttleReservation.DropoffDestination;
                    shuttleReservation.PickupDateTime = oldShuttleReservation.PickupDateTime;
                   // shuttleReservation.DropoffDateTime = newShuttleReservation.DropoffDateTime;
                }
            }
        }
    }
    
}
