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
using System.IO;
using SQLite;
using PasswordKeeper.Core.Models;

namespace PasswordKeeper
{
    [Activity(Label = "Add Password")]
    public class AddPasswordActivity : Activity
    {
        private EditText appName;
        private EditText username;
        private EditText email;
        private EditText password;
        private Button submit;
        public static string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbText.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPasswordView);
            FindViews();
            submit.Click += Submit_Click;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            
            var db = new SQLiteConnection(dbPath);

            db.CreateTable<Password>();

            try
            {
                var pass = new Password()
                {
                    AppName = appName.Text,
                    Username = username.Text,
                    Email = email.Text,
                    PasswordValue = password.Text
                };

                if (pass.AppName == "" || pass.Email == "" || pass.PasswordValue == "" || pass.Username == "")
                    throw new NullReferenceException();

                db.Insert(pass);

                var intent = new Intent(this, typeof(PasswordDetailActivity));
                intent.PutExtra("passwordId", pass.Id);
                this.Finish();
                StartActivity(intent);
            }
            catch (NullReferenceException)
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Empty Box Detected");
                dialog.SetMessage("Please make sure that you've filled all boxes");
                dialog.SetNeutralButton("Okay", delegate { });
                dialog.Show();                     
            }          
        }

        private void FindViews()
        {
            appName = FindViewById<EditText>(Resource.Id.appNameBox);
            username = FindViewById<EditText>(Resource.Id.usernameBox);
            email = FindViewById<EditText>(Resource.Id.emailBox);
            password = FindViewById<EditText>(Resource.Id.passwordbox);
            submit = FindViewById<Button>(Resource.Id.submitPasswordButton);
        }
    }
}