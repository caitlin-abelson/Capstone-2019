using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created : 02/27/2019
    /// This class contains extension methods for all of the various fields 
    /// involved in the resort project.
    /// Included are extension methods for both the datatype of the field and most have
    /// string versions as well. This makes it easier to verify data coming in from controls that 
    /// might not have an accurate datatype for them.
    /// CAUTION: Some methods involve fields that can be either nullable or no. In this case,
    /// those fields are marked and these extension methods will validate the non-null versions.
    /// </summary>
    public static class Validation
    {

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid offerin type, returns true, else returns false.
        /// A valid offering type is:
        ///     - Between 1 and 15 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidOfferingType(this string type)
        {
            bool isValid = false;
            if (type != "" && type != null && type.Length >= 1 && type.Length <= 15)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a decimal value is a valid quantity, returns true, else returns false.
        /// A valid quantity is:
        ///     - Positive
        /// </summary>
        public static bool IsValidRecipeQuantity(this decimal decimalValue)
        {
            bool isValidNumber = false;

            if (decimalValue > 0)
            {
                isValidNumber = true;
            }
            return isValidNumber;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// If a string value is a valid quantity, returns true, else returns false.
        /// A valid quantity is:
        ///     - Decimal
        ///     - Positive
        /// </summary>
        public static bool IsValidRecipeQuantity(this string stringValue)
        {
            decimal numberValue = 0;
            bool isValidNumber = false;
            try
            {
                if (Decimal.TryParse(stringValue, out numberValue))
                {
                    if (numberValue >= 0)
                    {
                        isValidNumber = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("The value cannot be less than 0.");
                    }
                }
                else
                {
                    throw new InvalidCastException("The value was not an decimal.");
                }

            }
            catch (Exception)
            {
            }

            return isValidNumber;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// If a string value is a valid unit of measure, returns true, else returns false.
        /// A valid unit of measure is:
        ///     - Between 1 and 25 characters
        ///     - Only Letters
        ///     - Not null
        /// </summary>
        public static bool IsValidRecipeUnitOfMeasure(this string stringValue)
        {
            
            bool isValid = false;
            
            if (stringValue != "" && stringValue != null && stringValue.Length >= 1 && stringValue.Length <= 25)
            {
                Regex unitOfMeasureRegex = new Regex(@"^[a-zA-Z ]+$");
                Match match = unitOfMeasureRegex.Match(stringValue);
                if (match.Success)
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// If a string value is a valid price, returns true, else returns false.
        /// A valid price is:
        ///     - Decimal
        ///     - Positive
        /// </summary>
        public static bool IsValidPrice(this decimal price)
        {
            bool isValid = false;
            decimal numberValue = 0;
            if (numberValue >= 0)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// If a string value is a valid price, returns true, else returns false.
        /// A valid name is:
        ///     - Decimal
        ///     - Positive
        /// </summary>
        public static bool IsValidPrice(this string price)
        {
            bool isValid = false;
            decimal numberValue = 0;
            try
            {
                if (Decimal.TryParse(price, out numberValue))
                {
                    if (numberValue >= 0)
                    {
                        isValid = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("The value cannot be less than 0.");
                    }
                }
                else
                {
                    throw new InvalidCastException("The value was not an decimal.");
                }
            }
            catch (Exception)
            {

            }

            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid first name, returns true, else returns false.
        /// A valid first name is:
        ///     - Between 1 and 50 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidFirstName(this string name)
        {
            bool isValid = false;
            if (name != "" && name != null && name.Length >= 1 && name.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid Item Name, returns true, else returns false.
        /// A valid Item Name is:
        ///     - Between 1 and 50 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidItemName(this string name)
        {
            bool isValid = false;
            if (name != "" && name != null && name.Length >= 1 && name.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid last name, returns true, else returns false.
        /// A valid last name is:
        ///     - Between 1 and 100 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidLastName(this string name)
        {
            bool isValid = false;
            if (name != "" && name != null && name.Length >= 1 && name.Length <= 100)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid phone number, returns true, else returns false.
        /// A valid phone number is:
        ///     - Between 1 and 11 characters
        ///     - Not null
        ///     - Consists of only numbers
        /// </summary>
        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            bool isValid = false;
            Regex phoneNumberRegex = new Regex(@"^[0-9]+$");
            Match match = phoneNumberRegex.Match(phoneNumber);
            if (phoneNumber != "" && phoneNumber != null && phoneNumber.Length >= 1 && phoneNumber.Length <= 11 && match.Success)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid email, returns true, else returns false.
        /// A valid email is:
        ///     - Between 6 and 250 characters
        ///     - Not null
        ///     - Matches the email regular expression
        /// Regular Expression from https://www.regextester.com/19
        /// </summary>
        public static bool IsValidEmail(this string email)
        {
            bool isValid = false;
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
            Match match = emailRegex.Match(email);
            if (email != "" && email != null && email.Length >= 6 && email.Length <= 250 && match.Success)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid DepartmentID, returns true, else returns false.
        /// A valid DepartmentID is:
        ///     - Between 1 and 50 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidDepartmentID(this string deptID)
        {
            bool isValid = false;
            if (deptID != "" && deptID != null && deptID.Length >= 1 && deptID.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid RoleID, returns true, else returns false.
        /// A valid DepartmentID is:
        ///     - Between 1 and 50 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidRoleID(this string roleID)
        {
            bool isValid = false;
            if (roleID != "" && roleID != null && roleID.Length >= 1 && roleID.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid Description, returns true, else returns false.
        /// CAUTION: Descriptions can be null and this will only work for non-null values.
        /// A valid description is:
        ///     - Max 1000 characters
        ///     - May or may not be nullable
        /// </summary>
        public static bool IsValidDescription(this string description)
        {
            bool isValid = false;
            if (description != null && description.Length <= 1000)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid GuestTypeID, returns true, else returns false.
        /// A valid GuestTypeID is:
        ///     - Between 1 and 25 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidGuestTypeID(this string guestTypeID)
        {
            bool isValid = false;
            if (guestTypeID != "" && guestTypeID != null && guestTypeID.Length >= 1 && guestTypeID.Length <= 25)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid Emergency Relation, returns true, else returns false.
        /// A valid Emergency Relation is:
        ///     - Between 1 and 25 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidEmergencyRelation(this string emergencyRelation)
        {
            bool isValid = false;
            if (emergencyRelation != "" && emergencyRelation != null && emergencyRelation.Length >= 1 && emergencyRelation.Length <= 25)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid number of pets, returns true, else returns false.
        /// A valid number of pets is:
        ///     - Integer greater than or equal to 0 and less than 100
        ///     - Not null
        ///     -
        /// </summary>
        public static bool IsValidNumberOfPets(this string numPets)
        {
            bool isValid = false;
            int result = 0;
            try
            {
                if (numPets != "" && numPets != null && Int32.TryParse(numPets, out result) && result >= 0 && result < 100)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If an int value is a valid number of pets, returns true, else returns false.
        /// A valid number of pets is:
        ///     - Integer greater than or equal to 0 and less than 100
        /// </summary>
        public static bool IsValidNumberOfPets(this int numPets)
        {
            bool isValid = false;
            if (numPets >= 0 && numPets < 100)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid number of Guests, returns true, else returns false.
        /// A valid number of guests is:
        ///     - Integer greater than or equal to 0
        ///     - Not null
        /// </summary>
        public static bool IsValidNumberOfGuests(this string numGuests)
        {
            bool isValid = false;
            int result = 0;
            try
            {
                if (numGuests != "" && numGuests != null && Int32.TryParse(numGuests, out result) && result >= 0)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If an int value is a valid number of pets, returns true, else returns false.
        /// A valid number of guests is:
        ///     - Integer greater than or equal to 0 and less than 100
        /// </summary>
        public static bool IsValidNumberOfGuests(this int numGuests)
        {
            bool isValid = false;
            if (numGuests >= 0)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a int value is a valid quantity, returns true, else returns false.
        /// A valid quantity is:
        ///     - Positive
        ///     - Int
        /// </summary>
        public static bool IsValidQuantity(this int intValue)
        {
            bool isValidNumber = false;

            if (intValue >= 0)
            {
                isValidNumber = true;
            }
            return isValidNumber;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid quantity, returns true, else returns false.
        /// A valid quantity is:
        ///     - Int
        ///     - Positive
        /// </summary>
        public static bool IsValidQuantity(this string decimalValue)
        {
            int numberValue = 0;
            bool isValidNumber = false;
            try
            {
                if (Int32.TryParse(decimalValue, out numberValue))
                {
                    if (numberValue >= 0)
                    {
                        isValidNumber = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("The value cannot be less than 0.");
                    }
                }
                else
                {
                    throw new InvalidCastException("The value was not an decimal.");
                }

            }
            catch (Exception)
            {
            }

            return isValidNumber;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid car make, returns true, else returns false.
        /// A valid car make is:
        ///     - Between 1 and 30 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidCarMake(this string carMake)
        {
            bool isValid = false;
            if (carMake != "" && carMake != null && carMake.Length >= 1 && carMake.Length <= 30)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid car model, returns true, else returns false.
        /// A valid car model is:
        ///     - Between 1 and 30 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidCarModel(this string carModel)
        {
            bool isValid = false;
            if (carModel != "" && carModel != null && carModel.Length >= 1 && carModel.Length <= 30)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid license plate number, returns true, else returns false.
        /// A valid car license plate number is:
        ///     - Between 2 and 10 characters (10 characters allows for foreign plates)
        ///     - Not null
        ///     - Only letters, numbers, and hyphens
        /// </summary>
        public static bool IsValidCarLicensePlateNumber(this string plateNumber)
        {
            bool isValid = false;
            if (plateNumber != "" && plateNumber != null && plateNumber.Length >= 2 && plateNumber.Length <= 10)
            {
                plateNumber = plateNumber.ToUpper();
                Regex licensePlate = new Regex(@"^[A-Z 0-9 \-]*$");
                Match match = licensePlate.Match(plateNumber);
                if (match.Success)
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid car color, returns true, else returns false.
        /// A valid car color is:
        ///     - Between 1 and 30 characters
        ///     - Not null
        /// </summary>
        public static bool IsValidCarColor(this string carColor)
        {
            bool isValid = false;
            if (carColor != "" && carColor != null && carColor.Length >= 1 && carColor.Length <= 30)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/27
        /// 
        /// If a string value is a valid parking location, returns true, else returns false.
        /// A valid parking location is:
        ///     - Between 1 and 50 characters
        /// </summary>
        public static bool IsValidParkingLocation(this string carParkingLocation)
        {
            bool isValid = false;
            if (carParkingLocation.Length >= 1 && carParkingLocation.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid building ID, returns true, else returns false.
        /// A valid building ID is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidBuildingID(this string buildingID)
        {
            bool isValid = false;
            if (buildingID != "" && buildingID != null && buildingID.Length >= 1 && buildingID.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid room number, returns true, else returns false.
        /// A valid room number is:
        ///     - Between 1 and 15 characters
        /// </summary>
        public static bool IsValidRoomNumber(this string roomNumber)
        {
            bool isValid = false;
            if (roomNumber != "" && roomNumber != null && roomNumber.Length >= 1 && roomNumber.Length <= 15)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid RoomTypeID, returns true, else returns false.
        /// A valid RoomTypeID is:
        ///     - Between 1 and 15 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidRoomTypeID(this string roomTypeID)
        {
            bool isValid = false;
            if (roomTypeID != "" && roomTypeID != null && roomTypeID.Length >= 1 && roomTypeID.Length <= 15)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Capacity, returns true, else returns false.
        /// A valid Capacity is:
        ///     - Value greater than or equal to 1
        ///     - Integer
        ///     - Not Null
        /// </summary>
        public static bool IsValidCapacity(this string capacity)
        {
            bool isValid = false;
            int result = 0;
            try
            {
                if (capacity != "" && capacity != null && Int32.TryParse(capacity, out result) && result >= 1)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If an integer value is a valid Capacity, returns true, else returns false.
        /// A valid Capacity is:
        ///     - Value greater than or equal to 1
        /// </summary>
        public static bool IsValidCapacity(this int capacity)
        {
            bool isValid = false;
            if (capacity >= 1)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Room Status ID, returns true, else returns false.
        /// A valid Room Status ID is:
        ///     - Between 1 and 25 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidRoomStatusID(this string roomStatusID)
        {
            bool isValid = false;
            if (roomStatusID != "" && roomStatusID != null && roomStatusID.Length >= 1 && roomStatusID.Length <= 25)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid address, returns true, else returns false.
        /// CAUTION: Some addresses can be null and this will only work for non-null values.
        /// A valid address is:
        ///     - Between 1 and 150 characters
        /// </summary>
        public static bool IsValidAddress(this string address)
        {
            bool isValid = false;
            if (address != "" && address != null && address.Length >= 1 && address.Length <= 150)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid building name, returns true, else returns false.
        /// CAUTION: Building Names can be null and this will only work for non-null values.
        /// A valid building name is:
        ///     - Between 1 and 150 characters
        /// </summary>
        public static bool IsValidBuildingName(this string buildingName)
        {
            bool isValid = false;
            if (buildingName != "" && buildingName != null && buildingName.Length >= 1 && buildingName.Length <= 150)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Building Status ID, returns true, else returns false.
        /// A valid Building Status ID is:
        ///     - Between 1 and 25 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidBuildingStatusID(this string buildingStatusID)
        {
            bool isValid = false;
            if (buildingStatusID != "" && buildingStatusID != null && buildingStatusID.Length >= 1 && buildingStatusID.Length <= 25)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Maintenance Type ID, returns true, else returns false.
        /// A valid Maintenance Type ID is:
        ///     - Between 1 and 15 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidMaintenanceTypeID(this string maintenanceTypeID)
        {
            bool isValid = false;
            if (maintenanceTypeID != "" && maintenanceTypeID != null && maintenanceTypeID.Length >= 1 && maintenanceTypeID.Length <= 15)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Note / Comment, returns true, else returns false.
        /// A valid Note / Comment is:
        ///     - Less than or equal to 1000 characters
        ///     - Nullable
        /// </summary>
        public static bool IsValidNoteOrComment(this string textValue)
        {
            bool isValid = false;
            if (textValue == null)
            {
                isValid = true;
            } else if (textValue != "" && textValue != null && textValue.Length <= 1000)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Maintenance Status, returns true, else returns false.
        /// A valid Maintenance Status is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidMaintenanceStatus(this string maintenanceStatus)
        {
            bool isValid = false;
            if (maintenanceStatus != "" && maintenanceStatus != null && maintenanceStatus.Length >= 1 && maintenanceStatus.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Service Component ID, returns true, else returns false.
        /// A valid Service Component ID is:
        ///     - Between 1 and 15 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidServiceComponentID(this string serviceComponentID)
        {
            bool isValid = false;
            if (serviceComponentID != "" && serviceComponentID != null && serviceComponentID.Length >= 1 && serviceComponentID.Length <= 15)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Event Type ID, returns true, else returns false.
        /// A valid Event Type ID is:
        ///     - Between 1 and 15 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidEventTypeID(this string eventTypeID)
        {
            bool isValid = false;
            if (eventTypeID != "" && eventTypeID != null && eventTypeID.Length >= 1 && eventTypeID.Length <= 15)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Max Number of Guests, returns true, else returns false.
        /// A valid Max Number of Guests is:
        ///     - Integer
        ///     - 1 or greater
        ///     - Not Null
        /// </summary>
        public static bool IsValidMaxNumberOfGuests(this string maxNum)
        {
            bool isValid = false;
            int result = 0;
            try
            {
                if (maxNum != null && Int32.TryParse(maxNum, out result) && result >= 1)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
            }
            
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Location, returns true, else returns false.
        /// A valid Location is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidLocation(this string location)
        {
            bool isValid = false;
            if (location != "" && location != null && location.Length >= 1 && location.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Performance Title, returns true, else returns false.
        /// A valid Performance Title is:
        ///     - Between 1 and 100 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidPerformanceTitle(this string title)
        {
            bool isValid = false;
            if (title != "" && title != null && title.Length >= 1 && title.Length <= 100)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Recipe Name, returns true, else returns false.
        /// A valid Recipe name is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidRecipeName(this string recipeName)
        {
            bool isValid = false;
            if (recipeName != "" && recipeName != null && recipeName.Length >= 1 && recipeName.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Item Type ID, returns true, else returns false.
        /// A valid Item Type ID is:
        ///     - Between 1 and 15 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidItemTypeID(this string itemTypeID)
        {
            bool isValid = false;
            if (itemTypeID != "" && itemTypeID != null && itemTypeID.Length >= 1 && itemTypeID.Length <= 15)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid City, returns true, else returns false.
        /// A valid City is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidCity(this string city)
        {
            bool isValid = false;
            if (city != "" && city != null && city.Length >= 1 && city.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid State, returns true, else returns false.
        /// A valid State is:
        ///     - 2 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidState(this string state)
        {
            bool isValid = false;
            if (state != "" && state != null && state.Length == 2)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Country, returns true, else returns false.
        /// A valid Country is:
        ///     - Between 1 and 25 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidCountry(this string country)
        {
            bool isValid = false;
            if (country != "" && country != null && country.Length >= 1 && country.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Postal Code, returns true, else returns false.
        /// A valid Postal Code is:
        ///     - Between 3 and 12 characters
        ///     - Not Null
        ///     - Can include spaces and hyphens
        /// </summary>
        public static bool IsValidPostalCode(this string postalCode)
        {
            bool isValid = false;
            Regex postalRegex = new Regex(@"^[a-zA-Z0-9\- ]*$");
            Match match = postalRegex.Match(postalCode);
            if (postalCode != "" && postalCode != null && postalCode.Length >= 3 && postalCode.Length <= 12 && match.Success)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Pet Type ID, returns true, else returns false.
        /// A valid Pet Type ID is:
        ///     - Between 1 and 25 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidPetType(this string petType)
        {
            bool isValid = false;
            if (petType != "" && petType != null && petType.Length >= 1 && petType.Length <= 25)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Pet Name, returns true, else returns false.
        /// A valid Pet Name is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidPetName(this string petName)
        {
            bool isValid = false;
            if (petName != "" && petName != null && petName.Length >= 1 && petName.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Pet Species, returns true, else returns false.
        /// A valid Pet Species is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidPetSpecies(this string species)
        {
            bool isValid = false;
            if (species != "" && species != null && species.Length >= 1 && species.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Vehicle ID, returns true, else returns false.
        /// A valid Vehicle ID is:
        ///     - Between 1 and 20 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidVehicleID(this string vehicleID)
        {
            bool isValid = false;
            if (vehicleID != "" && vehicleID != null && vehicleID.Length >= 1 && vehicleID.Length <= 20)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Resort Vehicle Status, returns true, else returns false.
        /// A valid Resort Vehicle Status is:
        ///     - Between 1 and 25 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidResortVehicleStatus(this string resortVehicleStatus)
        {
            bool isValid = false;
            if (resortVehicleStatus != "" && resortVehicleStatus != null && resortVehicleStatus.Length >= 1 && resortVehicleStatus.Length <= 25)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Sponsor Name, returns true, else returns false.
        /// A valid Sponsor Name is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidSponsorName(this string sponsorName)
        {
            bool isValid = false;
            if (sponsorName != "" && sponsorName != null && sponsorName.Length >= 1 && sponsorName.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Supplier Name, returns true, else returns false.
        /// A valid Supplier Name is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidSupplierName(this string supplierName)
        {
            bool isValid = false;
            if (supplierName != "" && supplierName != null && supplierName.Length >= 1 && supplierName.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Inspection Rating, returns true, else returns false.
        /// A valid Inspection Rating is:
        ///     - Between 1 and 50 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidInspectionRating(this string inspectionRating)
        {
            bool isValid = false;
            if (inspectionRating != "" && inspectionRating != null && inspectionRating.Length >= 1 && inspectionRating.Length <= 50)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Inspection Affiliation, returns true, else returns false.
        /// A valid Inspection Affiliation is:
        ///     - Between 1 and 25 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidInspectionAffiliation(this string inspectionAffiliation)
        {
            bool isValid = false;
            if (inspectionAffiliation != "" && inspectionAffiliation != null && inspectionAffiliation.Length >= 1 && inspectionAffiliation.Length <= 25)
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/28
        /// 
        /// If a string value is a valid Resort Property type ID, returns true, else returns false.
        /// A valid Resort Property Type ID is:
        ///     - Between 1 and 20 characters
        ///     - Not Null
        /// </summary>
        public static bool IsValidResortPropertyTypeID(this string resortPropertyType)
        {
            bool isValid = false;
            if (resortPropertyType != "" && resortPropertyType != null && resortPropertyType.Length >= 1 && resortPropertyType.Length <= 20)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
