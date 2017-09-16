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


            items = PasswordRepository.GetAllPasswords();
            passwordList.Adapter = new PasswordListAdapter(items, this);

            passwordList.ItemClick += PasswordList_ItemClick;
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