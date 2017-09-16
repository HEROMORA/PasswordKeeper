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
        private Password selectedPassword;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PasswordDetailView);

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
        }

        private void RevealButton_Click(object sender, EventArgs e)
        {
            PasswordTextView.Text = selectedPassword.PasswordValue;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            ClipboardManager clipboard = (ClipboardManager)GetSystemService(Context.ClipboardService);
            clipboard.Text = selectedPassword.PasswordValue;
        }
    }
}