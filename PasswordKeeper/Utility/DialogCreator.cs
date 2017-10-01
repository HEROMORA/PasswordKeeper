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
using System.Runtime.InteropServices;

namespace PasswordKeeper.Utility
{
    public static class DialogCreator
    {
        public static void CreateDialog
            (Context context, string title, string message, string neturalbuttontext,
            EventHandler<DialogClickEventArgs> neturaldele,
            string negativebuttontext = null,
            EventHandler<DialogClickEventArgs> negativedele = null)
        {
            var dialog = new AlertDialog.Builder(context);
            dialog.SetTitle(title);
            dialog.SetMessage(message);
            dialog.SetNeutralButton(neturalbuttontext, neturaldele);

            if (negativebuttontext != null && negativedele != null)
                dialog.SetNegativeButton(negativebuttontext, negativedele);

            dialog.Show();           
        }
    }
}