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
    [Activity(Label = "UserDetails")]
    public class UserDetails : Activity
    {
        //Variables
        TextView txtFirstName;
        TextView txtLastName;
        TextView txtGoalDate;
        TextView txtGoalWeight;
        TextView txtGoalBodyFat;
        TextView txtUserHeight;
        Button btnSaveUser;
        Button btnGoalDate;

        //Variables for validation
        private DateTime date;
        private Double weight;
        private Double fat;
        private Double height;
        private bool IsNew;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Initialize();
            // Create your application here

            if (User.userExists)
                setUserDetails();
            else
                IsNew = true;

        }

        private void Initialize()
        {
            // Set our view from the "UserDetails" layout resource
            SetContentView(Resource.Layout.UserDetails);

            //Adding button press action

            txtFirstName = FindViewById<TextView>(Resource.Id.txtFirstName);
            txtLastName = FindViewById<TextView>(Resource.Id.txtLastName);
            txtGoalDate = FindViewById<TextView>(Resource.Id.txtGoalDate);
            txtGoalWeight = FindViewById<TextView>(Resource.Id.txtGoalWeight);
            txtGoalBodyFat = FindViewById<TextView>(Resource.Id.txtGoalBodyFat);
            txtUserHeight = FindViewById<TextView>(Resource.Id.txtHeight);
            btnSaveUser = FindViewById<Button>(Resource.Id.btnSaveUser);
            btnGoalDate = FindViewById<Button>(Resource.Id.btnChooseGoalDate);

            btnSaveUser.Click += delegate { btnSaveUser_Click(); };
            //txtGoalDate.Click += delegate { DatePicker(txtGoalDate); }; //Changed to a button instead of clicking on date
            btnGoalDate.Click += delegate { btnGoalDate_Click(); };

            //Setting default goal date - this will be overidden by SetUserDetails method if user exists
            txtGoalDate.Text = DateTime.Now.AddMonths(6).ToShortDateString();

        }

        private void setUserDetails()
        {
            User user = User.getUser();
            IsNew = false;

            txtFirstName.Text = user.firstName;
            txtLastName.Text = user.lastName;
            txtGoalDate.Text = user.goalDate.ToShortDateString();
            txtGoalWeight.Text = user.goalWeight.ToString();
            txtGoalBodyFat.Text = user.goalBodyFat.ToString();
            txtUserHeight.Text = user.height.ToString();
        }

        private void btnSaveUser_Click()
        {
            // return if unable to validate fields
            if (!ValidateFields())
                return;

            User user = new User();

            user.firstName = txtFirstName.Text;
            user.lastName = txtLastName.Text;
            user.goalDate = date;
            user.goalWeight = weight;
            user.goalBodyFat = fat;
            user.height = height;
            if (IsNew)
                user.Add();
            else
                user.Update();

            //Returning to home screen
            Home();


        }

        private bool ValidateFields()
        {
            // Validating that names are entered
            if (txtFirstName.Text == "" || txtLastName.Text == "")
            {
                showMessage("Name must be completed");
                return false;
            }

            if (!DateTime.TryParse(txtGoalDate.Text, out date))
            {
                showMessage("Date is not the correct format, please correct and try again.");
                return false;
            }

            if (!Double.TryParse(txtUserHeight.Text, out height))
            {
                showMessage("Height is not the correct format, please correct and try again.");
                return false;
            }

            if (height <= 1)
            {
                showMessage("Height must be greater than 1, please correct and try again.");
                return false;
            }

            if (!Double.TryParse(txtGoalWeight.Text, out weight))
            {
                showMessage("Weight is not the correct format, please correct and try again.");
                return false;
            }

            if (!Double.TryParse(txtGoalBodyFat.Text, out fat))
            {
                showMessage("Body Fat is not the correct format, please correct and try again.");
                return false;
            }

            if (fat == 0 && weight == 0)
            {
                showMessage("You must enter either a weight or bodfat percentage.");
                return false;
            }

            return true;
        }

        private void DatePicker(TextView element)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                element.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void btnGoalDate_Click()
        {
            DatePicker(txtGoalDate);
        }

        private void showMessage(string message, bool home = false)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);

            if (home)
                builder.SetPositiveButton("OK", (s, e) => { });
            else
                builder.SetPositiveButton("OK", (s, e) => { });
            builder.Create().Show();
        }

        private void Home()
        {
            // Creating intent for main activity
            Intent ActivityMain = new Intent(this, typeof(MainActivity));

            // Starting Add weight Activity
            StartActivity(ActivityMain);
        }
    }
}