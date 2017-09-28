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
using static Android.Renderscripts.Sampler;

namespace PasswordKeeper
{
    [Activity(Label = "Password Keeper", MainLauncher = true)]
    public class PasswordEntranceActivity : Activity
    {
        private Button letmein;
        private EditText passwordbox;
        public static ISharedPreferences outprefs;
        public static ISharedPreferencesEditor edit;
        private int count = 0;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PasswordEntranceView);
            ActionBar.Hide();

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            bool previouslyStarted = prefs.GetBoolean("pref_previously_started", false);
            if (!previouslyStarted)
            {
                edit = prefs.Edit();
                edit.PutBoolean("pref_previously_started", true);

                edit.PutString("currentpassword", "123456");
                edit.Apply();
                //showHelp();
            }

            FindViews();
            
            letmein.Click += delegate
            {
                string realpassword = prefs.GetString("currentpassword", "123456");
                if (passwordbox.Text == realpassword)
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    Finish();
                    StartActivity(intent);
                }
                else
                {
                    count++;
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Wrong Password");
                    dialog.SetMessage
                        (string.Format("The Password You've entered is incorrect, Attempts: {0}", count.ToString()));
                    dialog.SetNeutralButton("Okay", delegate { passwordbox.Text = ""; });
                    dialog.Show();
                }
            };

        }

        private void FindViews()
        {
            letmein = FindViewById<Button>(Resource.Id.letmeinButton);
            passwordbox = FindViewById<EditText>(Resource.Id.userLockBox);
        }

    }
}