using DocStorage.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocStore.Auth
{
    public class UserManager : UserManager<User, long>
    {
        public UserManager(IUserStore<User, long> store)
            : base(store)
        {
            UserValidator = new UserValidator<User, long>(this);
            PasswordValidator = new PasswordValidator { RequiredLength = 6 };
        }
    }
}