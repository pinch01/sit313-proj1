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
    class DalWeight
    {
        //Setting as a list of weights initially, may change into a database or something else if required
        //List of weights that have been recorded
        private static int _INITIALIZED = 0;
        private static List<Weight> weights = new List<Weight>();

        public static void Add(Weight weight)
        {
            weights.Add(weight);
        }

        public static void Remove(Weight weight)
        {
            weights.Remove(weight);
        }

    }
}