using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using PasswordKeeper.Core.Models;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace PasswordKeeper.Adapters
{
    class PasswordListAdapter : BaseAdapter<Password>
    {
        List<Password> items;
        Activity context;

        public PasswordListAdapter(List<Password> items, Activity context)
        {
            this.items = items;
            this.context = context;
        }

        public override Password this[int position]
        {
            get { return items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var pass = items[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = pass.AppName;
            

            return convertView;
        }


    }
}