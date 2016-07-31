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
    static class DalUser
    {
        //List used to store user data, to be replaced by SqlLite if time permits
        private static List<User> users = new List<User>();

        public static int UserCount
        { get { return users.Count(); } }

        public static void Add(User user)
        {
            users.Add(user);
        }

        public static void Remove(User user)
        {
            users.Remove(user);
        }
    }
}