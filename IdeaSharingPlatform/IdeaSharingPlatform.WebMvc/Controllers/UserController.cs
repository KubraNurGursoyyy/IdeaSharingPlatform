using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using System;
using System.Linq;
using System.Web.Mvc;
using IdeaSharingPlatform.Models.Concretes;
using Exception = System.Exception;
using IdeaSharingPlatform.WebMvc.ApiAccess;
using System.Collections.Generic;
using IdeaSharingPlatform.WebMvc.Models;

namespace IdeaSharingPlatform.WebMvc.Controllers
{
    public class UserController : Controller
    {

        ProjectAccess projectaccess;
        //giriş sayfasından sign up ve miafir açılır
        UserAccess userAccess;
       // int currentprojectid = 0;

        [AllowAnonymous]
        [HttpGet]
        public ViewResult EntryPageView()
        {
            return View();
        }

        [HttpGet]
        public ViewResult UserMainPageView()
        {
            projectaccess = new ProjectAccess();
            return View(projectaccess.GetMiniProjects());
        }
        [HttpGet]
        public ViewResult ListUsersProjectsView()
        {
            List<MiniProjectInfo> userMiniProjects = new List<MiniProjectInfo>();   
            using (var projectAcces = new ProjectAccess())
            {
                List<MiniProjectInfo> allMiniProjects = allMiniProjects = projectAcces.GetMiniProjects();
               foreach  (MiniProjectInfo mini in allMiniProjects)
               {
                    if (mini.ProjectOwner.UserEmail==HttpContext.User.Identity.Name)
                    {
                        userMiniProjects.Add(mini);
                    }
               }
            }
            return View(userMiniProjects);
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ViewResult EditUsersProjectView(int projectid)
        {
            Projects project = new Projects();
            using (var projectAccess = new ProjectAccess())
            {
                project = projectAccess.GetProjectByidAsync(projectid).Result;
                
            }
            return View(project);
        }
        [HttpPost]
        
        public ActionResult EditUsersProject(Projects updateProject)
        {
            bool update;
            using (ProjectAccess projectAccess = new ProjectAccess())
            {
                update = projectAccess.UpdateProject(updateProject, updateProject.ProjectID);
            }
            if (update)
            {
                return RedirectToAction("SeeUsersProjectView", new { updateProject = updateProject });
            }
            else
            {
                return RedirectToAction("EditUsersProjectView", new { updateProject = updateProject });
            }
        }
        [HttpGet]
        public ViewResult SeeProjectView(int id)
        {
           // currentprojectid = id;
            projectaccess = new ProjectAccess();
            Projects projects = projectaccess.SeeProjectDetailsAsync(id).Result;
            return View(projects);
        }
        [HttpGet]
        public ViewResult CreateProjectPageView()
        {

            return View();
        }

        [HttpGet]
        public ViewResult ListUserSavedProjectView()
        {
            return View();
        }

        [HttpGet]
        public ViewResult ListUserLikedProjectView()
        {
            List<Projects> likedProjects = new List<Projects>();
            using (var userAccess = new UserAccess())
            {
                likedProjects = userAccess.GetUserLikedProjects(userAccess.GetUserIDByEmail(HttpContext.User.Identity.Name).Result).Result;
            }
            return View(likedProjects);
        }
        [HttpGet]
        public ViewResult UserProfileEditPageView()
        {
            Users user =  new Users();
            using (var userAccess = new UserAccess())
            {
                user = userAccess.GetUserAsync(userAccess.GetUserIDByEmail(HttpContext.User.Identity.Name).Result).Result;
            }
            return View(user);
        }
        [HttpGet]
        public ViewResult UserProfilePageView()
        {
            Users users = new Users();

            using (var userAccess = new UserAccess())
            {
               users = userAccess.GetUserAsync(userAccess.GetUserIDByEmail(HttpContext.User.Identity.Name).Result).Result;
            }
            
            return View(users);
            //burada giriş yapan kullanıcınınki görülecek direkt
        }
        [HttpPost]
        public ActionResult UpdateUser(Users users)
        {
           
            using (var userAccess = new UserAccess())
            {
                int userId = userAccess.GetUserIDByEmail(HttpContext.User.Identity.Name).Result;
                users.UserID = userId;
                if (userAccess.UpdateUser(users, userId))
                {
                    return RedirectToAction("UserProfilePageView");
                }
                else
                {

                    return RedirectToAction("UserProfileEditPageView");
                }
            }
        }
        [HttpGet]
        public ViewResult SeeUsersProjectView(int projectid)
        {
            // currentprojectid = id;
            projectaccess = new ProjectAccess();
            Projects projects = projectaccess.SeeProjectDetailsAsync(projectid).Result;
            return View(projects);
        }

        public ActionResult LikeProject(int currentprojectid)
        {
            projectaccess = new ProjectAccess();
            userAccess = new UserAccess();
            bool success = false;
            success = projectaccess.LikeProject(currentprojectid, userAccess.GetUserIDByEmail(HttpContext.User.Identity.Name).Result);
           // if (success)
              
            return RedirectToAction("SeeProjectView", new { id = currentprojectid });
        }
        /*
        public bool SaveProject()
        {

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeeProjectView(FormCollection collection, int projectid)
        {
            try
            {
               
                switch (collection["SeeProjectView"])
                {
                    case "LikeProject":
                        LikeProject(projectid);
                    case "SaveProject":
                        return RedirectToAction("SaveProject");
                }
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("LogInAndSignUpController::SignUpAs::Error occured.", ex);
            }
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProjectPageView(IdeaSharingPlatform.Models.Concretes.Projects projects)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            projects.ProjectCreationDate = DateTime.Now;
            projects.UpdateTime = DateTime.Now;
            // projects.ProjectOwnerID = Session["CurrentUserID"];
            userAccess = new UserAccess();
            projects.ProjectOwnerID = userAccess.GetUserIDByEmail(HttpContext.User.Identity.Name).Result;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateProjectPageView");
            }

            try
            {
                projectaccess = new ProjectAccess();
                bool response = projectaccess.CreateProject(projects);
                if (response)
                {
                    return RedirectToAction("SeeUsersProjectView");
                }
                else
                {
                    return RedirectToAction("CreateProjectPageView");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("LogInAndSignUpController::SignUpAs::Error occured.", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserMainPageView(FormCollection collection)
        {
            try
            {
                switch (collection["UserMainPageView"])
                {
                    case "CreateProjectPageView":
                        return RedirectToAction("CreateProjectPageView");
                    case "ListUsersProjectsView":
                        return RedirectToAction("ListUsersProjectsView");
                    case "ListUserLikedProjectView":
                        return RedirectToAction("ListUserLikedProjectView");
                    case "ListUserSavedProjectView":
                        return RedirectToAction("ListUserSavedProjectView");
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
        public ActionResult EntryPageView(FormCollection collection)
        {
            try
            {
                switch (collection["EntryPage"])
                {
                    case "ListProjectsForGuest":
                        return RedirectToAction("GuestMainPageView", "Guest");
                    case "LogIn":
                        return RedirectToAction("LogInPageView", "LogInAndSignUp");
                }
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("LogInAndSignUpController::SignUpAs::Error occured.", ex);
            }
        }


        

        #region 
        //misafir için get project by ıd
        //get all rpoject,
        //yorumyapma ve beğenme kaydetme olmayacak
        #endregion
        #region
        //kullanıcı için get project by ıd 
        //get all 
        //yorum beğenme kaydetme var
        // kullanıcıdan anasayfadan proje oluşturmaya ve bepenilenlerle kaydedilenleri görmeye gidilir
        //yani getuserlikes ve saves
        //burada projeler de olabilir getuserprojects

        //sign upda sign up işlemleri
        //loginde kullanıcı ekleme add user yani
        //sonra loginden anasayfa

        //profilde de yine anasayfadaki beğenilenlerle kaydedilenler projeler olur
        //get user by ıd olur profil sayfası görünsün diye bir tuşla da update sayfasına gidilir

        //updatein altında da delete olur

        //prje görüntülede beğenme yorum yapma olur
        //kednisi ise update de olur ve sil de bu ekrana projelerimden ulaşsın
        //get project by ıd olur burada

        //ayarlarda hesabı sil olur

        //projeleri listelede arama da olacak ama henüz hiç bilmiyorum onu
        //projelerin listelendiği yerde tüm kategorler üstte görünür böylece kategorilerden birini seçip filtreleyebilir
        //bunda get categories project olur
        //aramayı ada göre yapabiliriz.

        #endregion


    }
}