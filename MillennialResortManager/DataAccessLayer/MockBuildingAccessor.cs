/// <summary>
/// Danielle Russo
/// Created: 2019/02/20
/// 
/// This is a mock Data Accessor which implements IBuildingAccessor.  This is for testing purposes only.
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class BuildingAccessorMock : IBuildingAccessor
    {
        // Fake list of buildings serves as database
        private List<Building> buildings;

        /// <summary>
        /// Author: Dani Russo
        /// Created : 2/20/2019
        /// This constructor that sets up dummy data
        /// </summary>
        public BuildingAccessorMock()
        {
            buildings = new List<Building>
            {
                new Building {BuildingID = "Hotel 101", Name = "The Mud Burrow",
                    Address = "1202 Turtle Pond Parkway", Description = "Guest Hotel Rooms",
                    StatusID = "Available", ResortPropertyID = 110000},
                new Building {BuildingID = "Hotel 102", Name = "Shell Shack",
                    Address = "1202 Turtle Pond Parkway", Description = "Guest Hotel Rooms",
                    StatusID = "Available", ResortPropertyID = 120000},
                new Building {BuildingID = "Guest Bld 101", Name = "Chlorine Dreams",
                    Address = "1202 Turtle Pond Parkway", Description = "Water Park",
                    StatusID = "Available", ResortPropertyID = 130000},
                new Building {BuildingID = "Shopping Center 101", Name = "The Coral Reef",
                    Address = "1202 Turtle Pond Parkway", Description = "Shopping Center",
                    StatusID = "Available", ResortPropertyID = 140000},
                new Building {BuildingID = "Food Center 101", Name = "Trout Hatch",
                    Address = "1202 Turtle Pond Parkway", Description = "Food Court",
                    StatusID = "Available", ResortPropertyID = 150000},
                new Building {BuildingID = "Welcome Center", Name = "Canopy Center",
                    Address = "1202 Turtle Pond Parkway", Description = "Main Guest Center",
                    StatusID = "Available", ResortPropertyID = 160000},
                new Building {BuildingID = "GenBld01", Name = "Sea Cow Storage",
                    Address = "1202 Turtle Pond Parkway", Description = "Storage",
                    StatusID = "Available", ResortPropertyID = 170000}
            };
        }


        public int InsertBuilding(Building newBuilding)
        {
            int listLength = buildings.Count;
            buildings.Add(newBuilding);
            if (listLength + 1 == buildings.Count)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<Building> SelectAllBuildings()
        {
            return buildings;
        }

        public List<Building> SelectAllBuildingsByStatusID(string statusID)
        {
            throw new NotImplementedException();
        }

        public List<string> SelectAllBuildingStatus()
        {
            throw new NotImplementedException();
        }

        public int SelectBuildingByID(int buildingID)
        {
            throw new NotImplementedException();
        }

        public int UpdateBuilding(Building oldBuilding, Building updatedBuilding)
        {
            int rowsUpdated = 0;
            foreach (var bld in buildings)
            {
                if (bld.BuildingID == oldBuilding.BuildingID)
                {
                    bld.Name = updatedBuilding.Name;
                    bld.Address = updatedBuilding.Address;
                    bld.Description = updatedBuilding.Description;
                    bld.StatusID = updatedBuilding.StatusID;

                    rowsUpdated++;
                }

                if (rowsUpdated == 0)
                {
                    throw new ArgumentException("Building not found.");
                }
            }

            return rowsUpdated;
        }
    }
}
