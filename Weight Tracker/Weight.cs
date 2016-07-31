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
    class Weight
    {


        private void Initialise()
        {
            //Setting timestamp
            _timeStamp = DateTime.Now;
        }

        #region private variables

        private decimal _weight;
        private decimal _bodyFat;
        private DateTime _timeStamp;
        private DateTime _weightTime;
        private string _username;

        #endregion

        #region access modifiers

        public decimal weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public decimal bodyFat
        {
            get { return _bodyFat; }
            set { _bodyFat = value; }
        }

        public DateTime weightTime
        {
            get { return _weightTime; }
            set { _weightTime = value; }
        }

        public DateTime timeStamp
        {
            get { return _timeStamp; }
        }

        #endregion

        #region Constructors

        public Weight(decimal inWeight, decimal inFat, DateTime inTime)
        {
            Initialise();
            _weight = inWeight;
            _bodyFat = inFat;
            _weightTime = inTime;
        }

        public Weight(decimal inWeight, DateTime inTime)
            : this(inWeight, 0 , inTime)
        {
        }

        public Weight()
            : this(0, 0, DateTime.Now)
        {
        }

        #endregion

        #region Public Methods

        public static void Add(Weight weight)
        {
            DalWeight.Add(weight);
        }

        public void Add()
        {
            DalWeight.Add(this);
        }

        #endregion
    }
}