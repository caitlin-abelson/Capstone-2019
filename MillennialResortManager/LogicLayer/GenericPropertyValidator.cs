using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LogicLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Generic Property that validates any object given
    /// a set Validation Rules in a dictionary.
    ///
    /// Currently Supports : string null and bounds validation
    ///                      int bounds validation
    ///                      DateTime null validation
    ///                      TODO: nullable int, double/float
    ///
    /// Usage: (1) Add 'this.MeetsValidationCriteria(myDataObject, GetResortVehicleValidationCriteria());'
    ///            in function of choice to validate data object. <see cref="ResortPropertyManager"/> #Line 39
    ///        (2) Add GetResortVehicleValidationCriteria() getter function which defines validation rules
    ///            see <see cref="ResortPropertyManager"/> #Line 146
    /// </summary>
    public static class GenericPropertyValidator
    {
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Generic extension method used to validate
        /// object of any type given the dictionary settings
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="o">Extension identifier</param>
        /// <param name="target">Object to be validated</param>
        /// <param name="validationCriteria">Validation Instruction Set</param>
        public static void MeetsValidationCriteria<T>(this object o, T target, Dictionary<string, ValidationCriteria> validationCriteria)
        {
            string validationErrorMessage = "";

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                // ... ignore properties not of interest in inner scope
                {
                    var criterion = validationCriteria.SingleOrDefault(x => x.Key.Equals(property.Name.ToString())).Value;

                    // criterion is null, property is meant to be ignored then
                    if (criterion == null)
                        continue;
                }

                // ... string validation
                validationErrorMessage = ValidateString(target, validationCriteria, property, validationErrorMessage);

                // ... int validation
                validationErrorMessage = ValidateInteger(target, validationCriteria, property, validationErrorMessage);

                // ... DateTime validation
                validationErrorMessage = ValidateDate(target, validationCriteria, property, validationErrorMessage);

                if (validationErrorMessage.Length != 0)
                    throw new ArgumentException(validationErrorMessage);
            }
        }

        /// <summary>
        /// Francis Mingoimba
        /// Created: 2019/04/03
        /// 
        /// Helper method to validate DateTime objects
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="o">Extension identifier</param>
        /// <param name="target">Object to be validated</param>
        /// <param name="validationCriteria">Validation Instruction Set</param>
        /// <param name="property">Property Reflection Information of object</param>
        /// <param name="validationErrorMessage">Validation Error Message</param>
        /// <returns>concatenated list of errors</returns>
        private static string ValidateDate<T>(T target, Dictionary<string, ValidationCriteria> validationCriteria, PropertyInfo property,
            string validationErrorMessage)
        {
            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
                return validationErrorMessage;

            var value = property.GetValue(target);
            var criterion = validationCriteria.SingleOrDefault(x => x.Key.Equals(property.Name.ToString())).Value;

            if (value == null && !criterion.CanBeNull)
                validationErrorMessage += $"{property.Name} cannot be null\n";

            return validationErrorMessage;
        }

        /// <summary>
        /// Francis Mingoimba
        /// Created: 2019/04/03
        /// 
        /// Helper method to validate int primitive data type
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="target">Object to be validated</param>
        /// <param name="validationCriteria">Validation Instruction Set</param>
        /// <param name="property">Property Reflection Information of object</param>
        /// <param name="validationErrorMessage">Validation Error Message</param>
        /// <returns>concatenated list of errors</returns>
        private static string ValidateInteger<T>(T target, Dictionary<string, ValidationCriteria> validationCriteria, PropertyInfo property,
            string validationErrorMessage)
        {
            if (property.PropertyType != typeof(int))
                return validationErrorMessage;

            var value = (int)property.GetValue(target);
            var criterion = validationCriteria.SingleOrDefault(x => x.Key.Equals(property.Name.ToString())).Value;

            // ... check bounds.
            if(value < criterion.LowerBound && Math.Abs(criterion.LowerBound) < 0.0001) //Math.Abs to avoid arithmetic comparison error
                validationErrorMessage += $"{property.Name} cannot be less than 0\n";
            else if(value < criterion.LowerBound || value > criterion.UpperBound)
                validationErrorMessage += 
                    $"{property.Name} must be between {criterion.LowerBound} and {criterion.UpperBound} in length.\n";

            return validationErrorMessage;
        }

        /// <summary>
        /// Francis Mingoimba
        /// Created: 2019/04/03
        /// 
        /// Helper method to validate string primitive data type
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="o">Extension identifier</param>
        /// <param name="target">Object to be validated</param>
        /// <param name="validationCriteria">Validation Instruction Set</param>
        /// <param name="property">Property Reflection Information of object</param>
        /// <param name="validationErrorMessage">Validation Error Message</param>
        /// <returns>concatenated list of errors</returns>
        private static string ValidateString<T>(T target, Dictionary<string, ValidationCriteria> validationCriteria, PropertyInfo property,
            string validationErrorMessage)
        {
            if (property.PropertyType != typeof(string))
                return validationErrorMessage;

            var value = (string)property.GetValue(target);
            var criterion = validationCriteria.SingleOrDefault(x => x.Key.Equals(property.Name.ToString())).Value;

            // ... check for nulls
            if (value == null && !criterion.CanBeNull)
                validationErrorMessage += $"{property.Name} cannot be null\n";

            // check bounds
            if (value != null)
            {
                if (value.Length < criterion.LowerBound || value.Length > criterion.UpperBound)
                {
                    validationErrorMessage +=
                        $"{property.Name} length must be between {criterion.LowerBound} and {criterion.UpperBound}\n";
                }
            }

            // regex checking
            if (value != null && criterion.RegexExpression != null)
            {
                var regex = new Regex(criterion.RegexExpression);
                if (!regex.Match(value).Success)
                    validationErrorMessage += $"Invalid {property.Name}\n";
            }

            return validationErrorMessage;
        }

    }
}