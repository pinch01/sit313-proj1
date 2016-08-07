using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Weight_Tracker
{
    [Activity(Label = "Weight Tracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private static List<Weight> weights;

        //Adding label elements
        Button btnSummary;
        TextView lblCurrentWeight;
        TextView lblWeek;
        TextView lblMonth;
        TextView lblYear;
        TextView lblTotal;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Initialize();


            if (!User.userExists)
                GoToAddUser();

            // checking if weight exists before attempting to update the values for calculations
            if (weights.Count > 0)
                updateValues();

        }

        private void Initialize()
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Setting values for list of weights on initialisation
            weights = Weight.getWeights();

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btnAdd);
            btnSummary = FindViewById<Button>(Resource.Id.btnSummary);

            lblCurrentWeight = FindViewById<TextView>(Resource.Id.lblCurrentWeight);
            lblWeek = FindViewById<TextView>(Resource.Id.lblWeek);
            lblYear = FindViewById<TextView>(Resource.Id.lblYear);
            lblTotal = FindViewById<TextView>(Resource.Id.lblTotal);

            //Assigning actions to buttons
            button.Click += delegate { btnAdd_Click(); };
            btnSummary.Click += delegate { btnSummary_Click(); };

        }

        // Will update the values and perform some basic calculations
        private void updateValues()
        {
            setCurrentWeight();
            setWeightHistory();
        }

        private void setCurrentWeight()
        {
            lblCurrentWeight.Text = Weight.getMostRecent().weight.ToString() + "kg";
        }

        private void setWeightHistory()
        {
            lblWeek.Text = "Week: " + Weight.getWeekLoss().ToString();
            lblMonth.Text = "Month: " + Weight.getMonthLoss().ToString();
            lblYear.Text = "Year: " + Weight.getYearLoss().ToString();
            lblTotal.Text = "Total: " + Weight.getTotalLoss().ToString();
        }

        // To be called when the weigh in button is pressed
        private void btnAdd_Click()
        {
            //User details required for adding weight - check to see if exists before loading activity
            if (!User.userExists)
            {
                GoToAddUser();
            }
            else
            {
                GoToAddWeight();
            }

        }

        private void GoToAddUser()
        {
            // Creating intent for adding user details
            Intent ActivityAddUser = new Intent(this, typeof(UserDetails));

            //Starting activity
            StartActivity(ActivityAddUser);
        }

        private void GoToAddWeight()
        {
            // Creating intent for add weight activity
            Intent ActivityAddWeight = new Intent(this, typeof(AddWeight));

            // Starting Add weight Activity
            StartActivity(ActivityAddWeight);
        }

        private void GoToSummary()
        {
            Intent ActivitySummary = new Intent(this, typeof(Summary));
        }

        private void btnSummary_Click()
        {
            //User details required for adding weight - check to see if exists before loading activity
            if (!User.userExists)
            {
                GoToAddUser();
            }
            else
            {
                GoToSummary();
            }
        }
    }
}

