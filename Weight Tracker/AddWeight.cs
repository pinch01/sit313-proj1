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
    [Activity(Label = "Add Weight")]
    public class AddWeight : Activity
    {
        //Screen variables
        Button btnAdd;
        Button btnDate;
        TextView txtDatePicker;
        TextView txtWeight;
        TextView txtFat;

        //Variables
        private DateTime date;
        private decimal weight;
        private decimal fat;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Initialize();
        }

        #region Private Methods

        private void Initialize()
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddWeight);

            //Adding button press action
            btnAdd = FindViewById<Button>(Resource.Id.btnAddWeighIn);
            btnDate = FindViewById<Button>(Resource.Id.btnSelectDate);
            txtDatePicker = FindViewById<TextView>(Resource.Id.txtDate);
            txtWeight = FindViewById<TextView>(Resource.Id.txtWeight);
            txtFat = FindViewById<TextView>(Resource.Id.txtFat);
            txtDatePicker.InputType = Android.Text.InputTypes.ClassDatetime;

            //Setting action for clicking on date field - want to display date time picker
            btnDate.Click += delegate { DatePicker(); };

            btnAdd.Click += delegate { Save(); };

            //Setting values for screen
            SetValues();
        }

        private void Save()
        {
            if (!ValidateFields())
                return;

            //Instantiating Weight object
            Weight newWeight = new Weight();

            //If fields are valid - save the weight instance with the new values
            newWeight.weightTime = date;
            newWeight.weight = weight;
            newWeight.bodyFat = fat;

            //Adding newWeight value to list of measurements
            newWeight.Add();

            if (fat == 0) //message when fat is left at 0
            {
                showMessage(String.Format("You have added {0}kg on {1}.", weight, date.ToShortDateString()));
            }
            else if (weight == 0) //message for when weight is 0
            {
                showMessage(String.Format("You have added {0}% bodyfat on {1}.", fat, date.ToShortDateString()));
            }
            else //message when both fat and weight are entered
            {
                showMessage(String.Format("You have added {0}kg at {1}% on {2}.", weight, fat, date.ToShortDateString()));
            }

            //After message shown need to return to main activity
            Home();

        }

        //Show date time picker and return value
        private void DatePicker()
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                                                                            {
                                                                                txtDatePicker.Text = time.ToShortDateString();
                                                                            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void SetValues()
        {
            //Setting text view to current date
            txtDatePicker.Text = DateTime.Now.ToShortDateString();

        }

        // To be called before adding values to weight
        private bool ValidateFields()
        {
            DateTime temp;
            decimal tempDec;

            if (!DateTime.TryParse(txtDatePicker.Text, out date))
            {
                showMessage("Date is not the correct format, please correct and try again.");
                return false;
            }

            if (!Decimal.TryParse(txtWeight.Text, out weight))
            {
                showMessage("Weight is not the correct format, please correct and try again.");
                return false;
            }


            if (!Decimal.TryParse(txtFat.Text, out fat))
            {
                showMessage("Body Fat is not the correct format, please correct and try again.");
                return false;
            }

            if (fat == 0 && weight == 0)
            {
                showMessage("You must enter either a weight or bodfat percentage.");
                return false;
            }

            //All fields have been parsed successfully
            return true;
        }

        private void showMessage(string message, bool home = false)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);

            if (home)
                builder.SetPositiveButton("OK", (s, e) => { Home(); });
            else
                builder.SetPositiveButton("OK", (s, e) => { });
            builder.Create().Show();
        }

        private void Home()
        {
            // Creating intent for main activity
            Intent ActivityAddWeight = new Intent(this, typeof(MainActivity));

            // Starting Add weight Activity
            StartActivity(ActivityAddWeight);
        }
        #endregion
    }
}