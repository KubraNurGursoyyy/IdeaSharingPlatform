using IdeaSharingPlatform.BusinessLogic.Concretes;
using IdeaSharingPlatform.Commons.Concretes.Encryption;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace IdeaSharingPlatform.WebApi.Controllers
{
    public class LogInAndSignUpController : ApiController
    {
        // POST api/User
        //AddUSer
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LogInAndSignUp/LogInUser")]
        public IHttpActionResult LogInUser(LogInModel loginModel)
        {
            try
            {
                if (!(loginModel.UserEmail == null) && !(loginModel.UserPassword == null))
                {
                    loginModel.UserPassword = Encryption(loginModel.UserPassword);
                    using (var userBusiness = new UsersBusiness())
                    {
                        if (userBusiness.LogIn(loginModel.UserEmail, loginModel.UserPassword))
                            return Ok();
                        else
                            return NotFound();
                    }
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        // POST api/User
        //AddUSer
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LogInAndSignUp/SignUpUser")]
        public IHttpActionResult SignUpUser(Users user)
        {
            try
            {
                user.UserPassword = Encryption(user.UserPassword);
                using (var userBusiness = new UsersBusiness())
                {
                    if (userBusiness.Insert(user))
                        return Ok();
                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }


        //encription
        private string Encryption(string password)
        {
            return Sha1Encryption.SHA1(password);
        }
    }
}