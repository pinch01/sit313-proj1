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
        private static int _ID = 0;

        private void Initialise()
        {
            //Setting timestamp
            _timeStamp = DateTime.Now;
        }

        #region private variables

        private Double _weight;
        private Double _bodyFat;
        private DateTime _timeStamp;
        private DateTime _weightTime;
        private string _username;
        private int _Id;

        #endregion

        #region access modifiers

        public int id
        {
            get { return _Id; }
        }

        public Double weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public Double bodyFat
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

        public static int Count
        {
            get { return DalWeight.Count; }
        }

        #endregion

        #region Constructors

        public Weight(Double inWeight, Double inFat, DateTime inTime)
        {
            Initialise();
            _weight = inWeight;
            _bodyFat = inFat;
            _weightTime = inTime;

            _ID++;
            _Id = _ID;
        }

        public Weight(Double inWeight, DateTime inTime)
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

        public static List<Weight> getWeights()
        {
            return DalWeight.retrieveWeights();
        }

        public static Weight getMostRecent()
        {
            if (Weight.Count > 0)
            {
                //sorting list of weights
                return DalWeight.mostRecent();
            }
            return null;
        }

        public static Double getWeekLoss()
        {
            List<Weight> weights = Weight.getWeights();

            Weight first = new Weight();
            first.weightTime = DateTime.MaxValue;

            Weight last = new Weight();
            last.weightTime = DateTime.MinValue;

            // moving through loop to determine if in date rang
            // find oldest vs newest to get weight loss
            foreach (Weight w in weights)
            {
                // checking date for weigh in
                if (w.weightTime >= DateTime.Now.AddDays(-7))
                {
                    //Comparing to existing first and last value
                    if (w.weightTime < first.weightTime)
                    {
                        first = w;
                    }

                    if (w.weightTime > last.weightTime)
                    {
                        last = w;
                    }
                } //end if
            } //end foreach


            return first.weight - last.weight;

        }

        public static Double getMonthLoss()
        {
            List<Weight> weights = Weight.getWeights();

            Weight first = new Weight();
            first.weightTime = DateTime.MaxValue;

            Weight last = new Weight();
            last.weightTime = DateTime.MinValue;

            // moving through loop to determine if in date rang
            // find oldest vs newest to get weight loss
            foreach (Weight w in weights)
            {
                // checking date for weigh in
                if (w.weightTime >= DateTime.Now.AddMonths(-1))
                {
                    //Comparing to existing first and last value
                    if (w.weightTime < first.weightTime)
                    {
                        first = w;
                    }

                    if (w.weightTime > last.weightTime)
                    {
                        last = w;
                    }
                } //end if
            } //end foreach


            return first.weight - last.weight;
        }

        public static Double getYearLoss()
        {
            List<Weight> weights = Weight.getWeights();

            Weight first = new Weight();
            first.weightTime = DateTime.MaxValue;

            Weight last = new Weight();
            last.weightTime = DateTime.MinValue;

            // moving through loop to determine if in date rang
            // find oldest vs newest to get weight loss
            foreach (Weight w in weights)
            {
                // checking date for weigh in
                if (w.weightTime >= DateTime.Now.AddYears(-1))
                {
                    //Comparing to existing first and last value
                    if (w.weightTime < first.weightTime)
                    {
                        first = w;
                    }

                    if (w.weightTime > last.weightTime)
                    {
                        last = w;
                    }
                } //end if
            } //end foreach


            return first.weight - last.weight;
        }

        public static Double getTotalLoss()
        {
            List<Weight> weights = Weight.getWeights();

            Weight first = new Weight();
            first.weightTime = DateTime.MaxValue;

            Weight last = new Weight();
            last.weightTime = DateTime.MinValue;

            // moving through loop to determine if in date rang
            // find oldest vs newest to get weight loss
            foreach (Weight w in weights)
            {
                    //Comparing to existing first and last value
                    if (w.weightTime < first.weightTime)
                    {
                        first = w;
                    }

                    if (w.weightTime > last.weightTime)
                    {
                        last = w;
                    }
            } //end foreach


            return first.weight - last.weight;
        }

        //BMI calculation - will return -1 if unable to calculate
        public static Double getCurrentBMI(User user)
        {
            //Use of the official BMI calculation
            // weight kg \ height*height metres 
            if (user.height <= 0)
                return -1;

            //get most recent weight
            Weight recentWeight = DalWeight.mostRecentWeight();

            // if no weights have been recorded return invalid value
            if (recentWeight == null)
                return -1;

            Double BMI;

            // divided by 100 because height is entered in centimetres, calculation takes value in metres
            Double height = user.height / 100;
            Double weight = recentWeight.weight;

            height = height * height;
            BMI = weight / height;

            return BMI;
        }

        public static Double getCurrentBodyFat(User user)
        {
            Weight recentWeight = DalWeight.mostRecentBodyFat();

            if (recentWeight == null)
                return -1;

            return recentWeight.bodyFat;
        }

        public static Double getStartingWeight()
        {
            Weight startWeight = DalWeight.getStartingWeight();

            if (startWeight == null)
                return -1;

            return startWeight.weight;
        }

        public static Double getCurrentWeight()
        {
            Weight currentWeight = DalWeight.mostRecentWeight();

            if (currentWeight == null)
                return -1;

            return currentWeight.weight;
        }

        public static Double getRemainingWeightLoss(User user)
        {
            Weight currentWeight = DalWeight.mostRecentWeight();

            if (currentWeight == null || user.goalWeight == 0)
                return -1;

            return currentWeight.weight - user.goalWeight;
        }

        #endregion
    }
}