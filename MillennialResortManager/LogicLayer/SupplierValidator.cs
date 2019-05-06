using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="James Heim" created="2019/02/05">
	/// Validation class for the Supplier object.
	/// </summary>
	public static class SupplierValidator
    {

        public static readonly int SUPPLIER_NAME_MAX_LENGTH = 50;
        public static readonly int SUPPLIER_ADDRESS_MAX_LENGTH = 150;
        public static readonly int SUPPLIER_CITY_MAX_LENGTH = 50;
        public static readonly int SUPPLIER_COUNTRY_MAX_LENGTH = 25;
        public static readonly int SUPPLIER_PHONE_NUMBER_LENGTH = 10;
        public static readonly int SUPPLIER_EMAIL_MAX_LENGTH = 50;
        public static readonly int SUPPLIER_CONTACT_FIRST_NAME_MAX_LENGTH = 50;
        public static readonly int SUPPLIER_CONTACT_LAST_NAME_MAX_LENGTH = 100;
        public static readonly int SUPPLIER_DESCRIPTION_MAX_LENGTH = 1000;

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate supplier's name.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateName(this Supplier supplier)
        {
            if (supplier.Name == null)
            {
                throw new ArgumentNullException("Supplier Name cannot be null.");
            }
            else if (supplier.Name == "")
            {
                throw new ArgumentException("Supplier Name is required.");
            }
            else if (supplier.Name.Length > SUPPLIER_NAME_MAX_LENGTH)
            {
                throw new ArgumentException("Supplier Name cannot be over " + SUPPLIER_NAME_MAX_LENGTH + " characters in length.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate supplier's address.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateAddress(this Supplier supplier)
        {
            if (supplier.Address == null)
            {
                throw new ArgumentNullException("Supplier Address cannot be null.");
            }
            else if (supplier.Address == "")
            {
                throw new ArgumentException("Supplier Address is required.");
            }
            else if (supplier.Address.Length > SUPPLIER_ADDRESS_MAX_LENGTH)
            {
                throw new ArgumentException("Supplier Address cannot be over " + SUPPLIER_ADDRESS_MAX_LENGTH + " characters in length.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate supplier's city.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateCity(this Supplier supplier)
        {
            if (supplier.City == null)
            {
                throw new ArgumentNullException("Supplier City cannot be null.");
            }
            else if (supplier.City == "")
            {
                throw new ArgumentException("Supplier City is required.");
            }
            else if (supplier.City.Length > SUPPLIER_CITY_MAX_LENGTH)
            {
                throw new ArgumentException("Supplier City cannot be over " + SUPPLIER_CITY_MAX_LENGTH + " characters in length.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate supplier's state.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateState(this Supplier supplier)
        {
            if (supplier.State == null)
            {
                throw new ArgumentNullException("Supplier State cannot be null.");
            }
            else if (supplier.State == "")
            {
                throw new ArgumentException("Supplier State is required.");
            }
            else if (supplier.State.Length != 2)
            {
                throw new ArgumentException("Supplier State must be 2 characters in length.");
            }

        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate supplier's postal code.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidatePostalCode(this Supplier supplier)
        {
            if (supplier.PostalCode == null)
            {
                throw new ArgumentNullException("Supplier Postal Code cannot be null.");
            }
            else if (supplier.PostalCode == "")
            {
                throw new ArgumentException("Supplier Postal Code is required.");
            }
            else if (supplier.PostalCode.Length != 5)
            {
                throw new ArgumentException("Supplier Postal Code must be 5 characters in length.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate supplier's country.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateCountry(this Supplier supplier)
        {
            if (supplier.Country == null)
            {
                throw new ArgumentNullException("Supplier Country cannot be null.");
            }
            else if (supplier.Country == "")
            {
                throw new ArgumentException("Supplier Country is required.");
            }
            else if (supplier.Country.Length > SUPPLIER_COUNTRY_MAX_LENGTH)
            {
                throw new ArgumentException("Supplier Country cannot be over " + SUPPLIER_COUNTRY_MAX_LENGTH + " characters in length.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate supplier's phone number.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidatePhoneNumber(this Supplier supplier)
        {
            if (supplier.PhoneNumber == null)
            {
                throw new ArgumentNullException("Supplier Phone Number cannot be null.");
            }
            else if (supplier.PhoneNumber == "")
            {
                throw new ArgumentException("Supplier Phone Number is required.");
            }
            else if (supplier.PhoneNumber.Length != SUPPLIER_PHONE_NUMBER_LENGTH)
            {
                throw new ArgumentException("Supplier Phone Number must be " + SUPPLIER_PHONE_NUMBER_LENGTH + " characters in length.");
            }
            else if (!Regex.IsMatch(supplier.PhoneNumber, @"^\(?\d{3}\)?[\s\-]?\d{3}\-?\d{4}$"))
            {
                throw new ArgumentException("Supplier Phone Number is an invalid phone number.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate supplier's email.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateEmail(this Supplier supplier)
        {
            if (supplier.Email == null)
            {
                throw new ArgumentNullException("Supplier Email cannot be null.");
            }
            else if (supplier.Email == "")
            {
                throw new ArgumentException("Supplier Email is required.");
            }
            else if (supplier.Email.Length > SUPPLIER_EMAIL_MAX_LENGTH)
            {
                throw new ArgumentException("Supplier Email cannot be over " + SUPPLIER_EMAIL_MAX_LENGTH + " characters in length.");
            }
            else if (!Regex.IsMatch(supplier.Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                throw new ArgumentException("Supplier Email is an invalid email address.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate Contact's first name.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateContactFirstName(this Supplier supplier)
        {
            if (supplier.ContactFirstName == null)
            {
                throw new ArgumentNullException("Supplier Contact's first name cannot be null.");
            }
            else if (supplier.ContactFirstName == "")
            {
                throw new ArgumentException("Supplier Contact's first name is required.");
            }
            else if (supplier.ContactFirstName.Length > SUPPLIER_CONTACT_FIRST_NAME_MAX_LENGTH)
            {
                throw new ArgumentException("Supplier Contact's first name cannot be over " + 
                    SUPPLIER_CONTACT_FIRST_NAME_MAX_LENGTH + " characters in length.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate Contact's last name.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateContactLastName(this Supplier supplier)
        {
            if (supplier.ContactLastName == null)
            {
                throw new ArgumentNullException("Supplier Contact's last name cannot be null.");
            }
            else if (supplier.ContactLastName == "")
            {
                throw new ArgumentException("Supplier Contact's last name is required.");
            }
            else if (supplier.ContactLastName.Length > SUPPLIER_CONTACT_LAST_NAME_MAX_LENGTH)
            {
                throw new ArgumentException("Supplier Contact's last name cannot be over " +
                    SUPPLIER_CONTACT_LAST_NAME_MAX_LENGTH + " characters in length.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Validate Description.
		/// </summary>
		/// <param name="supplier"></param>
		public static void ValidateDescription(this Supplier supplier)
        {
            if (supplier.Description.Length > SUPPLIER_DESCRIPTION_MAX_LENGTH)
            {
                throw new ArgumentException("Supplier Contact's last name cannot be over " +
                    SUPPLIER_DESCRIPTION_MAX_LENGTH + " characters in length.");
            }
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Run through all validation methods.
		/// </summary>
		/// <param name="supplier"></param>
		public static void Validate(this Supplier supplier)
        {
            supplier.ValidateName();
            supplier.ValidateAddress();
            supplier.ValidateCity();
            supplier.ValidateState();
            supplier.ValidatePostalCode();
            supplier.ValidateCountry();
            supplier.ValidatePhoneNumber();
            supplier.ValidateEmail();
            supplier.ValidateContactFirstName();
            supplier.ValidateContactLastName();
            supplier.ValidateDescription();
        }
    }
}