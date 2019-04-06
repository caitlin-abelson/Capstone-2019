using System;
using System.Globalization;

namespace LogicLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 4/01/2019
    ///
    /// Used in conjunction with the GenericPropertyValidator
    /// for validation of any property.
    ///
    /// This class stores the criteria for validation by
    /// the GenericPropertyValidator
    ///
    /// Current supported types: string, int, DateTime
    /// </summary>
    public class ValidationCriteria
    {
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        /// 
        /// True if property can be null
        /// False if property cannot be null
        /// </summary>
        public bool CanBeNull { get; set; }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        /// 
        /// (int)    -> lower bound
        /// (double) -> lower bound
        /// (string) -> minimum length
        /// </summary>
        public double LowerBound { get; set; }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        /// 
        /// (int)    -> upper bound
        /// (double) -> upper bound
        /// (string) -> maximum length
        /// </summary>
        public double UpperBound
        {
            get => _isUpperBoundSet ? _upperBound : ThenOffsetHigherThanLowerBound;

            set
            {
                // check for int.Max. use tolerance to avoid floating point
                // comparison error
                _upperBound = Math.Abs(value - int.MaxValue) < 0.01 ? value - 1 : value;
                _isUpperBoundSet = true;
            }
        }

        /// <summary>
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// For Regex Checking
        /// </summary>
        public string RegexExpression { get; set; }

        #region Helpers

        private double _upperBound;

        private bool _isUpperBoundSet;

        private double ThenOffsetHigherThanLowerBound => LowerBound + 1;

        #endregion
    }
}