using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Weight_Tracker
{
    class User
    {
        #region Private variables

        private string _firstName;
        private string _lastName;
        private decimal _height;
        private DateTime _goalDate;
        private decimal _goalWeight;
        private decimal _goalBodyFat;

        #endregion

        #region Public accessors

        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public decimal height
        {
            get { return _height; }
            set { _height = value; }
        }

        public DateTime goalDate
        {
            get { return _goalDate; }
            set { _goalDate = value; }
        }

        public decimal goalWeight
        {
            get { return _goalWeight; }
            set { _goalWeight = value; }
        }

        public decimal goalBodyFat
        {
            get { return _goalBodyFat; }
            set { _goalBodyFat = value; }
        }

        public static bool userExists
        {
            get {
                if (DalUser.UserCount > 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region Constructors

        public User(string fName, string lName, decimal nHeight, DateTime date, decimal weight, decimal bodyFat)
        {
            _firstName = fName;
            _lastName = lName;
            _height = nHeight;
            _goalDate = date;
            _goalWeight = weight;
            _goalBodyFat = bodyFat;
        }

        public User()
        {
            _firstName = String.Empty;
            _lastName = String.Empty;
            _height = Decimal.Zero;
            _goalDate = DateTime.MinValue;
            _goalWeight = Decimal.Zero;
            _goalBodyFat = Decimal.Zero;
        }

        #endregion

        #region Public methods

        public void Add()
        {
            DalUser.Add(this);
        }

        public static void Add(User user)
        {
            DalUser.Add(user);
        }

        #endregion
    }
}