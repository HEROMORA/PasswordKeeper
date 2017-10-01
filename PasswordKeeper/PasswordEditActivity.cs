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
using PasswordKeeper.Core.Models;
using PasswordKeeper.Utility;
using SQLite;

namespace PasswordKeeper
{
    [Activity(Label = "PasswordEditActivity")]
    public class PasswordEditActivity : Activity
    {
        private EditText editApplicationName;
        private EditText editUsername;
        private EditText editEmail;
        private EditText editPassword;
        private Button submit;
        private Password selectedPassword;
        private SQLiteConnection db;
        private TableQuery<Password> table;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PasswordEditView);
            
            

            //Db Initilaztion
            db = new SQLiteConnection(AddPasswordActivity.dbPath);
            table = db.Table<Password>();
            int passwordId = Intent.Extras.GetInt("editpasswordid");
            selectedPassword = table.Where(x => x.Id == passwordId).First();

            FindViews();
            BindData();
            HandleEvents();

        }

        private void FindViews()
        {
            editApplicationName = FindViewById<EditText>(Resource.Id.editApplicationName);
            editUsername = FindViewById<EditText>(Resource.Id.editUsername);
            editEmail = FindViewById<EditText>(Resource.Id.editEmail);
            editPassword = FindViewById<EditText>(Resource.Id.editPassword);
            submit = FindViewById<Button>(Resource.Id.submitButton);
        }

        private void BindData()
        {
            editApplicationName.Text = selectedPassword.AppName;
            editUsername.Text = selectedPassword.Username;
            editEmail.Text = selectedPassword.Email;
            editPassword.Text = selectedPassword.PasswordValue;
        }

        private void HandleEvents()
        {
            submit.Click += Submit_Click;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (editApplicationName.Text == "" || editUsername.Text == "" ||
                    editPassword.Text == "" || editEmail.Text == "")
                {
                    throw new NullReferenceException();
                }

                var ExistUser = db.Query<Password>("SELECT * FROM Password WHERE Id = ?", selectedPassword.Id).FirstOrDefault();

                if (ExistUser != null)
                {
                    ExistUser.AppName = editApplicationName.Text;
                    ExistUser.Username = editUsername.Text;
                    ExistUser.Email = editEmail.Text;
                    ExistUser.PasswordValue = editPassword.Text;
                }
                
                db.RunInTransaction(() =>
                {
                    db.Update(ExistUser);
                });

                DialogCreator.CreateDialog(this, "Success", "Password have been changed Successfully",
                    "Okay", delegate { 
                        var intent = new Intent(this, typeof(PasswordDetailActivity));
                        intent.PutExtra("passwordId", ExistUser.Id);
                        this.Finish();
                        StartActivity(intent);
                    });

            }

            catch(NullReferenceException)
            {
                DialogCreator.CreateDialog(this, "Error", "You must fill all the boxes !",
                    "Okay", delegate { });
            }

            catch
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Error");
                dialog.SetMessage("Something wrong Happened");
                dialog.SetNeutralButton("Okay", delegate { });
                dialog.Show();                             
            }

        }

    }
}