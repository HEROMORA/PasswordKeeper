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
using Android.Preferences;

namespace PasswordKeeper
{
    [Activity(Label = "Password Keeper", MainLauncher = true)]
    public class PasswordEntranceActivity : Activity
    {
        public static ISharedPreferences prefs;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PasswordEntranceView);
            ActionBar.Hide();
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("localpassword", "123456");
            editor.Apply();

        }

        protected override void OnResume()
        {
            base.OnResume();
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(getBaseContext());
            bool previouslyStarted = prefs.GetBoolean(GetString(R.string.pref_previously_started), false);
            if (!previouslyStarted)
            {
                ISharedPreferencesEditor edit = prefs.Edit();
                edit.PutBoolean(getString(R.string.pref_previously_started), Boolean.TRUE);
                edit.Apply();
                showHelp();
            }
        }
    }
}