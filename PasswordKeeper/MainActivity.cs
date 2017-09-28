using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace PasswordKeeper
{
    [Activity(Label = "PasswordKeeper" ,Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button allPasswords;
        private Button addPassword;
        private Button userLock;
         
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            ActionBar.Hide();

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            allPasswords = FindViewById<Button>(Resource.Id.allPasswordsButton);
            addPassword = FindViewById<Button>(Resource.Id.addPasswordButton);
            userLock = FindViewById<Button>(Resource.Id.userlockButton);
        }

        private void HandleEvents()
        {
            allPasswords.Click += AllPasswords_Click;
            addPassword.Click += AddPassword_Click;
            userLock.Click += UserLock_Click;
        }

        private void UserLock_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PasswordChangerActivity));
            StartActivity(intent);
        }

        private void AddPassword_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddPasswordActivity));
            StartActivity(intent);
        }

        private void AllPasswords_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PasswordListActivity));
            StartActivity(intent);
        }
    }
}

