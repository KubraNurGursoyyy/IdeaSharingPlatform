using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using System.Web.Mvc;
using Exception = System.Exception;
using IdeaSharingPlatform.WebMvc.ApiAccess;
using IdeaSharingPlatform.Models.Concretes;

namespace IdeaSharingPlatform.WebMvc.Controllers
{
    public class GuestController : Controller
    {
        ProjectAccess projectaccess;
        UserAccess useraccess;

        [AllowAnonymous]
        [HttpGet]
        public ViewResult GuestMainPageView()
        {
            projectaccess = new ProjectAccess();
            return View(projectaccess.GetMiniProjects());
               
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult GuestSeeProjectView(int id)
        {
            projectaccess = new ProjectAccess();
            return View(projectaccess.SeeProjectDetailsAsync(id).Result);
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult GuestListAllProjectsView()
        {
            projectaccess = new ProjectAccess();
            return View(projectaccess.GetAllProjectsAsync().Result);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuestMainPageView(FormCollection collection)
        {
            try
            {
                switch (collection["GuestMainPage"])
                {
                    case "GuestListAllProjectsView":
                        return RedirectToAction("GuestListAllProjectsView", "Guest");
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

        /*
        [HttpGet]
        public async Task<ViewResult> GuestListAllProjectsView(FormCollection collection)
        {

            IEnumerable<Projects> projects = new List<Projects>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("/Project/GetAllProject");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsAsync<IList<Projects>>();
                    readTask.Wait();
                    projects = readTask.Result;
                }
                else
                {
                    projects = Enumerable.Empty<Projects>();
                    ModelState.AddModelError(System.String.Empty, "No Reccords Found");
                }
                return View(projects);
            }
        }
        */


        /*

        protected async Task<List<Projects>> GetAllProjectsAsync()
        {
            List<Projects> projects = new List<Projects>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/Project/GetAllProject");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsAsync<List<Projects>>();
                    readTask.Wait();
                    projects = readTask.Result;
                }
                else
                {
                    projects = (List<Projects>)Enumerable.Empty<Projects>();
                    ModelState.AddModelError(System.String.Empty, "No Reccords Found");
                }
                return projects;
            }
        }
        */

        /*
        protected List<MiniProjectInfo> GetMiniProjects()
        {
            List<MiniProjectInfo> miniprojects = new List<MiniProjectInfo>();
            foreach (var proj in UserController.GetAllProjectsAsync().Result)
            {
                MiniProjectInfo mini = new MiniProjectInfo();
                mini.ProjectID = proj.ProjectID;
                mini.ProjectName = proj.ProjectName;
                mini.ProjectOwnerID = proj.ProjectOwnerID;
                mini.ProjectBlurb = proj.ProjectBlurb;
                mini.ProjectCreationDate = proj.ProjectCreationDate;
                mini.ProjectsCategoryId = proj.ProjectsCategoryId;
                miniprojects.Add(mini);
            }
            return miniprojects;
        }*/


        /*
        protected List<Projects> GetAllProjects()
        {
            URL url = new URL("api/Project/GetAllProject");
            HttpURLConnection conn = (HttpURLConnection)url.openConnection();
            conn.setRequestMethod("GET");
            conn.connect();
            int responsecode = conn.getResponseCode();
            if (responsecode != 200)
                throw new RuntimeException("HttpResponseCode: " +responsecode);
            else
            {
                Scanner sc = new Scanner(url.openStream());
                string inline = "";
                while (sc.hasNext())
                {
                    inline += sc.nextLine();
                }
                sc.close();
            }




        }
        */
        //[HttpPost]
        //[ResponseType(typeof(List<Projects>))]
        //public List<Projects> ListProjectsForGuest()
        //{
        //    try
        //    {
        //        List<Projects> projectList;
        //        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Projects").Result;
        //        empList = response.Content.ReadAsAsync<IEnumerable<Projects>>().Result;
        //        return projectList;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
        //        throw new Exception("LogInAndSignUpController::SignUpAs::Error occured.", ex);
        //    }
        //}


        //protected List<MiniProjectInfo> GetMiniProjectList()
        //{

        //}
    }
}