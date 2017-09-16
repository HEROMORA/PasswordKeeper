using PasswordKeeper.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Core.Repository
{
    public class PasswordRepository
    {
        public PasswordRepository()
        {
        
        }

        public static List<Password> GetAllPasswords()
        {
            return Passwords;
        }

        public static Password GetPasswordId(int id)
        {
            var password = Passwords.Where(x => x.Id == id).FirstOrDefault();
            return password;
        }

        private static List<Password> Passwords = new List<Password>()
        {
            new Password()
            {
                Id = 1,
                AppName = "Facebook",
                Username = "iceshadow03",
                Email = "omar.skillful@gmail.com",
                PasswordValue = "skill"              
            },

            new Password()
            {
                Id = 2,
                AppName = "Twitter",
                Username = "omarskillful",
                Email = "omar.skillful@gmail.com",
                PasswordValue = "twitt"
            },

            new Password()
            {
                Id = 3,
                AppName = "instagram",
                Username = "heromora",
                Email = "omar.skillful@gmail.com",
                PasswordValue = "instahero"
            }
        };
            

    }
}
