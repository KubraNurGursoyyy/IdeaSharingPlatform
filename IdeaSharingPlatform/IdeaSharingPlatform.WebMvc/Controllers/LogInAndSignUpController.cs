using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using IdeaSharingPlatform.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaSharingPlatform.WebMvc.ApiAccess;
using System.Web.Security;
using IdeaSharingPlatform.Models.Concretes;

namespace IdeaSharingPlatform.WebMvc.Controllers
{
    
    public class LogInAndSignUpController : Controller
    {
        LogInAndSignUpAccess logacces;
        UserAccess userAccess;
        int UserId = 0;

        [AllowAnonymous]
        [HttpGet]
        public ViewResult LogInPageView()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult SignUpPageView()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogInPageView(FormCollection collection, LogInModel loginModel)
        {
            try
            {
                if (collection["LogInPage"] == "Kaydol")
                {
                    return RedirectToAction("SignUpPageView");
                }
                else if (!(loginModel.UserEmail == null) && !(loginModel.UserPassword == null))
                {
                    logacces = new LogInAndSignUpAccess();
                    bool response = logacces.LogInAsync(loginModel).Result;
                    if (response)
                    {
                        FormsAuthentication.SetAuthCookie(loginModel.UserEmail, false);
                        userAccess = new UserAccess();
                        UserId = userAccess.GetUserIDByEmail(loginModel.UserEmail).Result;
                        Users user = userAccess.GetUserAsync(UserId).Result;
                        Session["CurrentUserID"] = user.UserID;
                        Session["CurrentUserName"] = user.UserFirstName + " " + user.UserLastName; 
                        return RedirectToAction("UserMainPageView", "User");
                    }
                    else
                    {
                        return RedirectToAction("LogInPageView", "LogInAndSignUp");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("LogInAndSignUpController::SignUpAs::Error occured.", ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUpPageView(IdeaSharingPlatform.Models.Concretes.Users user)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            user.UserJoinDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("SignUpPageView");
            }
            
            try
            {
                logacces = new LogInAndSignUpAccess();
                bool response = logacces.SignUpAsync(user).Result;
                if (response)
                {
                    /*
                    FormsAuthentication.SetAuthCookie(user.UserEmail, true);
                    Session["CurrentUserID"] = user.UserID;
                    Session["CurrentUserName"] = user.UserFirstName + " " + user.UserLastName;-*/
                    return RedirectToAction("LogInPageView", "LogInAndSignUp");
                }
                else
                {
                    return RedirectToAction("SignUpPageView", "LogInAndSignUp");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("LogInAndSignUpController::SignUpAs::Error occured.", ex);
            }
        }



    }
}