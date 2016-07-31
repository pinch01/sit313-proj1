using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Weight_Tracker
{
    [Activity(Label = "Weight Tracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Initialize();

            if (!User.userExists)
                GoToAddUser();
        }

        private void Initialize()
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btnAdd);

            button.Click += delegate { btnAdd_Click(); };
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
    }
}

