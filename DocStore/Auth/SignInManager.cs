using DocStorage.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocStore.Auth
{
    public class SignInManager : SignInManager<User, long>
    {
        public SignInManager(UserManager<User, long> userManager, IAuthenticationManager autentificationManager)
            : base(userManager, autentificationManager)
        {

        }
        public void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}