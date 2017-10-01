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
using PasswordKeeper.Adapters;
using PasswordKeeper.Core.Models;
using PasswordKeeper.Core.Repository;
using SQLite;

namespace PasswordKeeper
{
    [Activity(Label = "Passwords List")]
    public class PasswordListActivity : Activity
    {
        private ListView passwordList;
        private List<Password> items;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PasswordListView);
            passwordList = FindViewById<ListView>(Resource.Id.passwordListViewli);

            try
            {
                var db = new SQLiteConnection(AddPasswordActivity.dbPath);
                var table = db.Table<Password>();
                items = table.AsParallel().ToList();
                passwordList.Adapter = new PasswordListAdapter(items, this);
                passwordList.FastScrollEnabled = true;
                passwordList.ItemClick += PasswordList_ItemClick;
            }
            catch 
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("First Time");
                dialog.SetMessage("Since this is your first time you have to create a password first");
                dialog.SetNeutralButton("Create a New Password", 
                    delegate { var intent = new Intent(this, typeof(AddPasswordActivity));
                        StartActivity(intent);
                    });
                dialog.SetNeutralButton("Okay", delegate { });
            }
          
        }

        private void PasswordList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var password = items[e.Position];
            Intent intent = new Intent(this, typeof(PasswordDetailActivity));
            intent.PutExtra("passwordId", password.Id);
            StartActivityForResult(intent, 100);
        }
    }
}