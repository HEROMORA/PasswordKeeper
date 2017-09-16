using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Core.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordValue { get; set; }
    }
}
