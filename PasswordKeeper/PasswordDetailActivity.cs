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
using PasswordKeeper.Core.Repository;
using PasswordKeeper.Core.Models;
using SQLite;

namespace PasswordKeeper
{
    [Activity(Label = "Password Details")]
    public class PasswordDetailActivity : Activity
    {
        private TextView AppNameTextView;
        private TextView EmailTextView;
        private TextView UsernameTextView;
        private TextView PasswordTextView;
        private Button RevealButton;
        private Button CopyButton;
        private Button EditButton;
        private Button DeleteButton;
        private Password selectedPassword;
        private bool Reveal = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PasswordDetailView);
            //ActionBar.Hide();

            //DB connection

            var db = new SQLiteConnection(AddPasswordActivity.dbPath);
            var table = db.Table<Password>();
            int passwordid = (Intent.Extras.GetInt("passwordId"));
            selectedPassword = table.Where(x => x.Id == passwordid).First();


            FindViews();
            BindData();
            HandleEvents();
        }

        private void FindViews()
        {
            AppNameTextView = FindViewById<TextView>(Resource.Id.appNameTextView);
            EmailTextView = FindViewById<TextView>(Resource.Id.emailTextView);
            UsernameTextView = FindViewById<TextView>(Resource.Id.userNameTextView);
            PasswordTextView = FindViewById<TextView>(Resource.Id.passwordTextView);
            RevealButton = FindViewById<Button>(Resource.Id.revealButton);
            CopyButton = FindViewById<Button>(Resource.Id.copyButton);
            DeleteButton = FindViewById<Button>(Resource.Id.deleteButton);
            EditButton = FindViewById<Button>(Resource.Id.editButton);
        }

        private void BindData()
        {
            AppNameTextView.Text = selectedPassword.AppName;
            EmailTextView.Text = selectedPassword.Email;
            UsernameTextView.Text = selectedPassword.Username;           
        }

        private void HandleEvents()
        {
            CopyButton.Click += CopyButton_Click;
            RevealButton.Click += RevealButton_Click;
            EditButton.Click += EditButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PasswordEditActivity));
            intent.PutExtra("editpasswordid", selectedPassword.Id);
            Finish();
            StartActivity(intent);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Confirmation");
            dialog.SetMessage("Are you sure you Want to Delete this Password ?");
            dialog.SetNegativeButton("Cancel", delegate { });
            dialog.SetNeutralButton("Yes", delegate {
                var db = new SQLiteConnection(AddPasswordActivity.dbPath);
                var table = db.Table<Password>();
                table.Delete(x => x.Id == selectedPassword.Id);
                var intent = new Intent(this, typeof(PasswordListActivity));
                this.Finish();
                StartActivity(intent);
            });
            dialog.Show();
        }

        private void RevealButton_Click(object sender, EventArgs e)
        {
            if (Reveal == false)
            {
                RevealButton.Text = "Hide";
                PasswordTextView.Text = selectedPassword.PasswordValue;
                Reveal = true;
            }
            else if (Reveal == true)
            {
                RevealButton.Text = "Reveal";
                PasswordTextView.Text = "****";
                Reveal = false;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            ClipboardManager clipboard = (ClipboardManager)GetSystemService(Context.ClipboardService);
            clipboard.Text = selectedPassword.PasswordValue;
        }
    }
}