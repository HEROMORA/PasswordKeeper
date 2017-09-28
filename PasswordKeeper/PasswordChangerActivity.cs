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
using PasswordKeeper.Utility;

namespace PasswordKeeper
{
    [Activity(Label = "PasswordChangerActivity")]
    public class PasswordChangerActivity : Activity
    {
        private EditText currentPassword;
        private EditText newPassword;
        private EditText repeatNewPassword;
        private Button changePasswordButton;
        private ISharedPreferences prefs;
        private ISharedPreferencesEditor edit;
          
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PasswordChangerView);
            ActionBar.Hide();

            FindViews();

            changePasswordButton.Click += ChangePasswordButton_Click;
            
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {            
            try
            {
                if (currentPassword.Text == "" || newPassword.Text == "" || repeatNewPassword.Text == "")
                    throw new NullReferenceException();
                if (newPassword.Text != repeatNewPassword.Text)
                    throw new MatchPasswordsException();
         
                prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
                string RealCurrent = prefs.GetString("currentpassword", "123456");

                if (currentPassword.Text != RealCurrent)
                    throw new NotTheSameException();

                if (currentPassword.Text == RealCurrent)
                {
                    if (newPassword.Text == repeatNewPassword.Text)
                    {
                        string NewPassword = newPassword.Text;
                        edit = prefs.Edit();
                        edit.PutString("currentpassword", NewPassword);
                        edit.Apply();
                        var dialog = new AlertDialog.Builder(this);
                        dialog.SetTitle("Success");
                        dialog.SetMessage("Password have been changed Successfully !");
                        dialog.SetNeutralButton("Okay", delegate { Finish(); });
                        dialog.Show();
                    }
                }
            }
            
            catch (NullReferenceException)
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Empty Boxes");
                dialog.SetMessage("Please Fill all of the Boxes");
                dialog.SetNeutralButton("Okay", delegate { });
                dialog.Show();
            }

            catch (MatchPasswordsException)
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Not the same");
                dialog.SetMessage("New passwords doesn't match !");
                dialog.SetNeutralButton("Okay", delegate { });
                dialog.Show();
            }

            catch (NotTheSameException)
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Not the same");
                dialog.SetMessage("Your Current Password is wrong");
                dialog.SetNeutralButton("Okay", delegate { currentPassword.Text = ""; });
                dialog.Show();
            }
        }

        private void FindViews()
        {
            currentPassword = FindViewById<EditText>(Resource.Id.currentPasswordBox);
            newPassword = FindViewById<EditText>(Resource.Id.newPasswordBox);
            repeatNewPassword = FindViewById<EditText>(Resource.Id.repeatPasswordBox);
            changePasswordButton = FindViewById<Button>(Resource.Id.changePasswordButton);
        }
    }
}