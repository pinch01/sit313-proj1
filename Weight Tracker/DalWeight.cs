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
    static class DalWeight
    {
        //Setting as a list of weights initially, may change into a database or something else if required
        //List of weights that have been recorded
        private static int _INITIALIZED = 0;
        private static List<Weight> weights = new List<Weight>();

        public static int Count
        {
            get { return weights.Count(); }
        }

        public static void Add(Weight weight)
        {
            weights.Add(weight);
            //Sorting weights for future reference, this is to ensure that pos[0] is always the newest

            weights.Sort((x,y) => y.weightTime.CompareTo(x.weightTime));
        }

        public static void Remove(Weight weight)
        {
            weights.Remove(weight);
        }

        public static List<Weight> retrieveWeights()
        {
            //user is passed but not implemented at present time
            return weights;
        }

        public static Weight mostRecent()
        {
            if (weights.Count > 0)
                return weights[0];
            else
                return null;
        }

        public static Weight mostRecentWeight()
        {
            int i = 0;

            while (i < weights.Count)
            {
                if (weights[i].weight > 0)
                    return weights[0];
            }

            // if cant be found return null
            return null;
        }

        public static Weight mostRecentBodyFat()
        {
            int i = 0;

            while (i < weights.Count)
            {
                if (weights[i].bodyFat != 0)
                    return weights[i];
                i++;
            }

            // if cant be found return null
            return null;
        }

        public static Weight getStartingWeight()
        {
            //starting from end of list
            int i = weights.Count - 1;

            while (i >= 0)
            {
                if (weights[i].weight != 0)
                    return weights[i];
                i--;
            }

            //if unable to find, return null
            return null;
        }

    }
}