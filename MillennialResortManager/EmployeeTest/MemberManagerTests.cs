using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class MemberManagerTests
    {
        private List<Member> _members;
        private IMemberManager _memberManager;
        private MemberAccessorMock _memb;

        //private MemberAccessorMock _memberAccessor;

        [TestInitialize]

        public void testSetup()
        {
            _memb = new MemberAccessorMock();
            _memberManager = new MemberManagerMSSQL(_memb);
            _members = new List<Member>();
            _members = _memberManager.RetrieveAllMembers();

        }

        [TestMethod]
        public void testAddMemberValid()
        {
            // Arrange
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "1231231234",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };

            // Act
            try
            {
                _memberManager.CreateMember(tMember);
                _members = _memberManager.RetrieveAllMembers();
            }
            catch (Exception)
            {

                throw;
            }
            // Assert
            Assert.IsNotNull(_members.Find(x =>
           x.MemberID == tMember.MemberID &&
           x.FirstName == tMember.FirstName &&
           x.LastName == tMember.LastName &&
           x.PhoneNumber == tMember.PhoneNumber &&
           x.Email == tMember.Email &&
           x.Password == tMember.Password &&
           x.Active == tMember.Active)
           );

        }

        // Test First Name with null value

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void TestAddMemberInvalidFirstNameNull()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = null,
                LastName = "Chhetri",
                PhoneNumber = "1231231234",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }


        // Test the first name with empty string
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidFirstNameEmpty()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "",
                LastName = "Chhetri",
                PhoneNumber = "1231231234",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }

        // Test first name with too long 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidFirstNameTooLong()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Name",
                LastName = "Chhetri",
                PhoneNumber = "1231231234",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            for (int i = 0; i < MemberValidator.MEMBER_FIRST_NAME_MAX_LENGTH + 1; i++)
            {
                tMember.FirstName += "A";
            }

            _memberManager.CreateMember(tMember);
        }


        // Test For last Name with null value

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void TestAddMemberInvalidLastNameNull()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = null,
                PhoneNumber = "1231231234",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }


        // Test the last name with empty string
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidLastNameEmpty()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "",
                PhoneNumber = "1231231234",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }

        // Test last name with too long
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidLastNameTooLong()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Name",
                LastName = "Chhetri",
                PhoneNumber = "1231231234",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            for (int i = 0; i < MemberValidator.MEMBER_LAST_NAME_MAX_LENGTH + 1; i++)
            {
                tMember.FirstName += "A";
            }

            _memberManager.CreateMember(tMember);
        }


        
        // Test Phone Number with Null
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void TestAddMemberInvalidPhoneNumberNull()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = null,
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }

        
        // Test Phone Number with Empty
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidPhoneNumberEmpty()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }
        
        // Test Phone Number With Too Short
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidPhoneNumberTooShort()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "319111222",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }
        
        // Test Phone Number With TooLong
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidPhoneNumberTooLong()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "31911122222",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }
        
        // Test Phone Number With Non number
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidPhoneNumberNonNumber()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "c19123123a",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }

        
     
        // Test Email Null
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void TestAddMemberInvalidEmailNull()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "3191231234",
                Email = null,
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }

        
        // Test Email Empty 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidEmailEmpty()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ramesh",
                LastName = "Chhetri",
                PhoneNumber = "3191231234",
                Email = "",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }
        
        // Test Email With TooShort
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidEmailTooShort()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "319111222",
                Email = "R@test.com",
                Password = "Test",
                Active = true
            };
            _memberManager.CreateMember(tMember);
        }
        
        // Test Email With TooLong
     
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddMemberInvalidEmailTooLong()
        {
            Member tMember = new Member()
            {
                MemberID = 12345,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "31911122222",
                Email = "Ram@test.com",
                Password = "Test",
                Active = true
            };
            for (int i = 0; i < MemberValidator.MEMBER_EMAIL_MAX_LENGTH + 1; i++)
            {
                tMember.Email += "a";
            }


            _memberManager.CreateMember(tMember);
        }

        [TestMethod]
        public void TestRetrieveMember()
        {
            // Arrange
            int tMemberID = 12311;

            Member tMember = new Member()
            {
                MemberID = tMemberID,
                FirstName = "Ram",
                LastName = "Chhetri",
                PhoneNumber = "3191231234",
                Email = "test@company.com",
                Password = "Test",
                Active = true

            };
            _memberManager.CreateMember(tMember);
            // Act
            Member retrievedMember = _memberManager.RetrieveMember(tMemberID);

            // Assert
            Assert.AreEqual(retrievedMember, tMember);

        }

        
        

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateMemberInvalidFirstNameNull()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = null,
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidFirstNameEmpty()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidFirstNameTooLong()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            for (int i = 0; i < MemberValidator.MEMBER_FIRST_NAME_MAX_LENGTH + 1; i++)
            {
                tMember.FirstName += "AA";
            }
            _memberManager.UpdateMember(tMember, oMember);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateMemberInvalidLastNameNull()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "Ram",
                LastName = null,
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidLastNameEmpty()
        {
            Member fMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(fMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "Test",
                LastName = "",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.UpdateMember(tMember, fMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidLastNameTooLong()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            for (int i = 0; i < MemberValidator.MEMBER_LAST_NAME_MAX_LENGTH + 1; i++)
            {
                tMember.LastName += "AA";
            }
            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateMemberInvalidPhoneNumberNull()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "Ramesh",
                LastName = "Chhetr",
                PhoneNumber = null,
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidPhoneNumberEmpty()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "",
                LastName = "Chhetr",
                PhoneNumber = "",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidPhoneNumberTooLong()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "Ram",
                LastName = "Chhetr",
                PhoneNumber = "12312312345",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            
            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidPhoneNumberNonNumber()
        {
            Member fMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(fMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "Ram",
                LastName = "Chhetr",
                PhoneNumber = "1231A3123B",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };

            _memberManager.UpdateMember(tMember, fMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateMemberInvalidEmailNull()
        {
            Member fMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(fMember);

            Member tMemebr = new Member()
            {
                MemberID = 1234,
                FirstName = "Ramesh",
                LastName = "Chhetr",
                PhoneNumber = "1231231234",
                Email = null,
                Password = "Test",
                Active = true,
            };
            _memberManager.UpdateMember(tMemebr, fMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidEmailEmpty()
        {
            Member fMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(fMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "",
                LastName = "Chhetr",
                PhoneNumber = "",
                Email = "",
                Password = "Test",
                Active = true,
            };
            _memberManager.UpdateMember(tMember, fMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidEmailTooLong()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "Ram",
                LastName = "Chhetr",
                Email = "Test@company.com",
                PhoneNumber = "12312312345",
                Password = "Test",
                Active = true,
            };
            for(int i = 0; i < MemberValidator.MEMBER_EMAIL_MAX_LENGTH -10; i++)
            {
                tMember.Email += "A";
            }

            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMemberInvalidEmailNoAtSign()
        {
            Member oMember = new Member()
            {


                MemberID = 1234,
                FirstName = "Ramchan",
                LastName = "Chhetr",
                PhoneNumber = "3191231235",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(oMember);

            Member tMember = new Member()
            {
                MemberID = 1234,
                FirstName = "Ram",
                LastName = "Chhetr",
                PhoneNumber = "1231A3123B",
                Email = "chancompany.com",
                Password = "Test",
                Active = true,
            };

            _memberManager.UpdateMember(tMember, oMember);
        }

        [TestMethod]
        public void TestDeactivate()
        {
            // Arrange
            int memberID = 1111;
            Member member = new Member()
            {
                MemberID = memberID,
                FirstName = "Ram",
                LastName = "Chhetr",
                PhoneNumber = "6051231234",
                Email = "chan@company.com",
                Password = "Test",
                Active = true,
            };
            _memberManager.CreateMember(member);

            // Act
            _memberManager.DeleteMember(member);
            // Assert
            Assert.IsFalse(_memberManager.RetrieveMember(memberID).Active);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestDelete()
        {
            int memberID = 1111;
            Member member = new Member()
            {
                MemberID = memberID,
                FirstName = "Ram",
                LastName = "Chhetr",
                PhoneNumber = "6051231234",
                Email = "chan@company.com",
                Password = "Test",
                Active = false,
            };
            _memberManager.CreateMember(member);
            _memberManager.DeleteMember(member);
            _memberManager.RetrieveMember(memberID);

        }
    }
}



