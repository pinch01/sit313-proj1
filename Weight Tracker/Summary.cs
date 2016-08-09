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
    [Activity(Label = "Summary")]
    public class Summary : Activity
    {
        #region private variables

        TextView txtBMI;
        TextView txtBoadyFat;
        TextView txtStartingWeight;
        TextView txtCurrentWeight;
        TextView txtGoalWeight;
        TextView txtRemaining;
        ProgressBar pbProgress;
        Button btnHome;

        User user;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Initialize();
            SetValues();
        }

        private void Initialize()
        {
            //Setting the content view
            SetContentView(Resource.Layout.Summary);

            //Setting user record
            if (User.userExists)
                user = User.getUser();
            else
                user = new User(); //if user does not currently exist - default user will be created, should not be reachable without a user.

            //setting the text view variables
            txtBMI = FindViewById<TextView>(Resource.Id.txtBmiSummary);
            txtBoadyFat = FindViewById<TextView>(Resource.Id.txtBodyFatSummary);
            txtStartingWeight = FindViewById<TextView>(Resource.Id.txtStartingWeight);
            txtCurrentWeight = FindViewById<TextView>(Resource.Id.txtCurrentWeight);
            txtGoalWeight = FindViewById<TextView>(Resource.Id.txtGoalWeight);
            txtRemaining = FindViewById<TextView>(Resource.Id.txtRemainingSummary);

            btnHome = FindViewById<Button>(Resource.Id.btnSumHome);
            pbProgress = FindViewById<ProgressBar>(Resource.Id.proWeight);
            pbProgress.Max = 100;

            //Adding action to home button
            btnHome.Click += delegate { btnHome_Click(); };

        }

        private void SetValues()
        {
            //Setting progress bar to 0% initially
           // pbProgress.Progress = 0;

            txtBMI.Text = "BMI: " + (Weight.getCurrentBMI(user) != -1 ? Weight.getCurrentBMI(user).ToString() : "Not Set");
            txtBoadyFat.Text = "Body Fat: " + (Weight.getCurrentBodyFat(user) != -1 ? Weight.getCurrentBodyFat(user).ToString() + "%" : "Not Set");
            //Setting weight loss variables
            txtStartingWeight.Text = "Starting Weight: " + (Weight.getStartingWeight() != -1 ? Weight.getStartingWeight().ToString() : "Not Set");
            txtCurrentWeight.Text = "Current Weight: " + (Weight.getCurrentWeight() != -1 ? Weight.getCurrentWeight().ToString() : "Not Set");
            txtGoalWeight.Text = "Goal Weight: " + (user.goalWeight > 0 ? user.goalWeight.ToString() : "Not Set");
            txtRemaining.Text = "Remaining: " + (Weight.getRemainingWeightLoss(user) != -1 ? Weight.getRemainingWeightLoss(user).ToString() : "Not Available");

            //Setting progress bar value
            pbProgress.Progress = getProgressPercent(Weight.getStartingWeight(), Weight.getCurrentWeight(), user.goalWeight);

        }

        //converts from Double to int too
        private int getProgressPercent(Double startWeight, Double currentWeight, Double goalWeight)
        {
            //checking that calculation can be performed
            if (startWeight == -1 || currentWeight == -1 || goalWeight <= 0)
                return 0;

            double total = startWeight - goalWeight;
            double lost = startWeight - currentWeight;

            double percent = lost / total * 100;
            percent = Math.Floor(percent);

            return Convert.ToInt32(percent);
        }

        private void btnHome_Click()
        {
            Intent ActivitySummary = new Intent(this, typeof(MainActivity));
            StartActivity(ActivitySummary);
        }
    }
}