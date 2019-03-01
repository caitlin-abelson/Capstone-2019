using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataObjects;
using DataAccessLayer;


namespace UnitTests
{

    ///  /// <summary>
    /// Eduardo Colon
    /// Created: 2019/02/09
    /// 
    /// description for RoleManagerTests
    /// </summary>
    [TestClass]
    public class RoleManagerTests
    {
        private List<Role> _roles;
        private IRoleManager _roleManager;
        private RoleAccessorMock _roleMock;

        [TestInitialize]
        public void testSetup()
        {
            _roleMock = new RoleAccessorMock();
            _roleManager = new RoleManager(_roleMock);
            _roles = new List<Role>();
            _roles = _roleManager.RetrieveAllRoles();
        }


        private string createStringLength(int length)
        {
            string testingString = "";
            for (int i = 0; i < length; i++)
            {
                testingString += "X";
            }
            return testingString;
        }

        private void setRole(Role newRole, Role oldRole)
        {
            newRole.RoleID = oldRole.RoleID;
            newRole.Description = oldRole.Description;
             
        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// Testing  to retrieve all roles

        [TestMethod]
        public void TestRetrieveAllRoles()
        {
            //Arrange
            List<Role> roles = null;
            //Act
            roles = _roleManager.RetrieveAllRoles();
            //Assert
            CollectionAssert.Equals(_roles, roles);
        }

      

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// Testing  all valid inputs to create role 
        /// </summary>
        [TestMethod]
        public void TestCreateRoleWithAllValidInputs()
        {

            //arrange
            Role newRole = new Role() { RoleID = "Registration", Description = "Registers rooms ", Active = true };
            //Act
            _roleManager.CreateRole(newRole);
            //Assert
            _roles = _roleManager.RetrieveAllRoles();

            Assert.IsNotNull(_roles.Find(r => r.RoleID == newRole.RoleID &&
                r.Description == newRole.Description &&
                r.Active == newRole.Active
            ));
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// Testing invalid input for RoleId too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRoleInValidInputRoleIDTooLong()
        {
            //Arrange
            Role role = new Role()
            {
                RoleID = createStringLength(51),
                Description = "Administers to roles",
                Active = true
            };
            //Act
            _roleManager.CreateRole(role);
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// Testing invalid input for RoleId too small
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRoleInValidInputRoleIDTooSmall()
        {
            //Arrange
            Role role = new Role()
            {
                RoleID = createStringLength(0),
                Description = "Administers to roles",
                Active = true
            };
            //Act
            _roleManager.CreateRole(role);
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// Testing invalid input for Description too long
        /// </summary>
        [TestMethod]
       [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRoleInValidInputDescriptionTooLong()
        {
            //Arrange
            Role role = new Role()
            {
                RoleID = "Administers",
                Description = createStringLength(1001),
                Active = true
            };
            //Act
            _roleManager.CreateRole(role);
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// Testing invalid input for Description too small
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRoleInValidInputDescriptionTooSmall()
        {
            //Arrange
            Role role = new Role()
            {
                RoleID = "Administers",
                Description = createStringLength(0),
                Active = true
            };
            //Act
            _roleManager.CreateRole(role);
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// Testing invalid input empty for Description
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateDescriptionInValidInputEmpty()
        {
            //Arrange
            Role role = new Role()
            {
                RoleID = "Checks in",
                Description = "",
                Active = true
            };
            //Act
            _roleManager.CreateRole(role);
        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// Testing invalid input empty for RoleId
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRoleIDInValidInputEmpty()
        {
            //Arrange
            Role role = new Role()
            {
                RoleID = "",
                Description = "Admins roles ",
                Active = true
            };
            //Act
            _roleManager.CreateRole(role);
        }



   

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// Testing valid  retrieving all roles
        /// </summary>
         [TestMethod]
        public void TestRetrieveAllRolesWithValidInput()
        {
            //Arrange
            List<Role> roles = new List<Role>();

            //Act
            roles = _roleManager.RetrieveAllRoles();

            //Assert
            Assert.IsNotNull(roles);
        }



        




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// Testing valid input to update description in the  role
        /// </summary>
        [TestMethod]
        public void TestUpdateRoleInDescriptionWithValidInput()
        {
            // Arrange
            var validDescriptionLength = 1000;
            var oldRole = new Role();
            var newRole = new Role() { Description = new string(new char[validDescriptionLength]) };

            //Act
            _roleManager.UpdateRole(oldRole, newRole);

           
        }
      
       

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// Testing invalid input for Description when updating the role
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateRoleWithNewRoleDescriptionLessThan1()
        {
            // Arrange
            var oldRole = new Role();
            var newRole = new Role() { Description = "" };

            _roleManager.UpdateRole(oldRole, newRole);


           

        }
        
       

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// Testing invalid input for Description when updating the role
        /// </summary>
        [TestMethod]
       [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateRoleWithNewRoleDescriptionMoreThan250()
        {
            // Arrange
            var oldRole = new Role();
            var newRole = new Role() { Description = new string(new char[1001]) };

            _roleManager.UpdateRole(oldRole, newRole);
        }
       
        

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// 
        /// Testing valid input when deactivating an employee role
        /// Expected : does not throw exception
        /// </summary>
        [TestMethod]
        public void TestDeactivateRoleWithValidRoleID()
        {
            //Arrange
             string roleID = "Checks in";
            bool roleActive = true;
            //Act
            _roleManager.DeleteRole(roleID,roleActive);
          
            
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// 
        /// Testing valid input when deactivating an employee role
        /// Expected : does not throw exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateRoleWithInValidRoleID()
        {
            //Arrange
            string roleID = "null";
        
            //Act
            _roleManager.DeleteRole(roleID, false);


        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// 
        /// Testing valid input for deleting a role
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteEmployeeValid()
        {


          
            string roleID = "Checks in";
          
            //Act
            _roleManager.DeleteRole(roleID, false);

            //Assert
            _roleManager.RetrieveRoleByRoleId(roleID);
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// 
        /// Testing retrieving all roles with at least one object
        /// </summary>

        [TestMethod]
        public void TestRetrieveAllRoles_RetrieveAtLeastOneObject()
        {
            var roles = _roleManager.RetrieveAllRoles();

            bool hasAtLeastOneElement = roles.Count > 0;

            Assert.IsTrue(hasAtLeastOneElement);
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/09
        /// 
        /// 
        /// Testing retrieving all roles with  zero  object
        /// </summary>

        [TestMethod]
      
        public void TestRetrieveAllRoles_RetrieveZeroObject()
        {
            var roles = _roleManager.RetrieveAllRoles();

            bool hasAtLeastZeroElement = roles.Count < 0;

            Assert.IsFalse(hasAtLeastZeroElement);
        }









    }
}
