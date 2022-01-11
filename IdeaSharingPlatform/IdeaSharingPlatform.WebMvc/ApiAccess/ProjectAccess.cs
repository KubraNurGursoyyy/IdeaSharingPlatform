using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.WebApi.Controllers;
using IdeaSharingPlatform.WebMvc.Models;
using java.lang;
using java.net;
using java.util;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http.Description;
using Exception = System.Exception;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

namespace IdeaSharingPlatform.WebMvc.ApiAccess
{
    public class ProjectAccess:IDisposable
    {
        CommentAccess commentAccess;
        public async Task<List<Projects>> GetAllProjectsAsync()
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/GetAllProject");
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;
            var projects = new List<Projects>();

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    projects = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Projects>>(await responseStream);
                }
                foreach (var proj in projects)
                {
                    proj.ProjectOwner = GetProjectOwnerAsync(proj.ProjectID).Result;
                    proj.ProjectsCategory = GetProjectCategoryAsync(proj.ProjectID).Result;
                    proj.ProjectsComments = GetProjectCommentsAsync(proj.ProjectID).Result;
                }
            }
            else
            {
                projects = (List<Projects>)Enumerable.Empty<Projects>();

            }
            return projects;

        }

        protected async Task<int> GetProjectLikeNumberAsync(int projid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/GetProjectLikeNumber/" + projid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            int like = 0;

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    like = await System.Text.Json.JsonSerializer.DeserializeAsync<int>(await responseStream);
                }
            }
            else
            {
                like = 0;

            }
            return like;

        }

        protected async Task<int> GetProjectSaveNumberAsync(int projid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/GetProjectSaveNumber/" + projid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            int save = 0;

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    save = await System.Text.Json.JsonSerializer.DeserializeAsync<int>(await responseStream);
                }
            }
            else
            {
                save = 0;

            }
            return save;

        }
        
        protected async Task<List<Comments>> GetProjectCommentsAsync(int projid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/GetProjectComments/" + projid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            commentAccess = new CommentAccess();
            List<Comments> comm = new List<Comments>();

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    comm = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Comments>>(await responseStream);
                }
                foreach (var item in comm)
                {
                    item.CommentedUser = commentAccess.GetProjectOwnerAsync(item.CommentedUsersID).Result;
                }
            }
            else
            {
                comm = (List<Comments>)Enumerable.Empty<List<Comments>>();

            }
            return comm;

        }
        public async Task<Projects> GetProjectByidAsync(int projectid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/GetProject/" + projectid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            var project = new Projects();

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    project = await System.Text.Json.JsonSerializer.DeserializeAsync<Projects>(await responseStream);
                }
            }
            else
            {
                project = (Projects)Enumerable.Empty<Projects>();

            }
            return project;
        }
        public async Task<Projects> SeeProjectDetailsAsync(int projid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/GetProject/" + projid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            var project = new Projects();

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    project = await System.Text.Json.JsonSerializer.DeserializeAsync<Projects>(await responseStream);
                }
            }
            else
            {
                project = (Projects)Enumerable.Empty<Projects>();

            }
            project.ProjectOwner = GetProjectOwnerAsync(project.ProjectID).Result;
            project.ProjectsCategory = GetProjectCategoryAsync(project.ProjectID).Result;
            project.ProjectsComments = GetProjectCommentsAsync(project.ProjectID).Result;
            return project;
        }

        protected async Task<Users> GetProjectOwnerAsync(int projid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/GetProjectOwner/" + projid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            var owner = new Users();

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    owner = await System.Text.Json.JsonSerializer.DeserializeAsync<Users>(await responseStream);
                }
            }
            else
            {
                owner = (Users)Enumerable.Empty<Users>();

            }
            return owner;

        }

        protected async Task<Categories> GetProjectCategoryAsync(int projid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/GetProjectCategory/" + projid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            var cat = new Categories();

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    cat = await System.Text.Json.JsonSerializer.DeserializeAsync<Categories>(await responseStream);
                }
            }
            else
            {
                cat = (Categories)Enumerable.Empty<Categories>();

            }
            return cat;

        }


        public bool CreateProject(Projects project)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/CreateProject");
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(project);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var client = HttpClientFactory.Create();
            var response = client.PostAsync(message.RequestUri, data);
            var result = response.Result;

            bool iscreated = false;

            if (result.IsSuccessStatusCode)
            {
                if (result.ReasonPhrase == "OK")
                {
                    iscreated = true;
                }
                else
                {
                    iscreated = false;
                }
            }
            return iscreated;
        }

        public bool DeleteProject(int id)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Delete;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/DeleteProject/" + id);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            bool delete = false;

            if (result.IsSuccessStatusCode)
            {
                if (result.ReasonPhrase == "OK")
                {
                    delete = true;
                }
            }
            return delete;
        }
        public bool UpdateProject(Projects project, int id)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Put;
            message.RequestUri = new Uri("https://localhost:44330/api/Project/UpdateProject" + id);
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(project);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var client = HttpClientFactory.Create();
            var response = client.PutAsync(message.RequestUri, data);
            var result = response.Result;

            bool update = false;

            if (result.IsSuccessStatusCode)
            {
                if (result.ReasonPhrase == "OK")
                {
                    update = true;
                }
                else
                {
                    update = false;
                }
            }
            return update;
        }

        public bool LikeProject(int projid, int userid)
        {
            var liked = false;
            UserLikes userLikes = new UserLikes();
            userLikes.LikedProjectID = projid;
            userLikes.LikedProjectsUsersID = userid;
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.RequestUri = new Uri("https://localhost:44330/api/LikedProject/LikeProject");
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(userLikes);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var client = HttpClientFactory.Create();
            var response = client.PostAsync(message.RequestUri, data);
            var result = response.Result;

            if (result.IsSuccessStatusCode)
            {
                liked = true;
            }
            return liked;
        }
        public bool RemoveLikeProject(int likeid )
        {
            var removed = false;
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/RemoveLikeProject/LikeProject/" + likeid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            if (result.IsSuccessStatusCode)
            {
                removed = true;
            }
            return removed;
        }
        public bool SaveProject(int projid, int userid)
        {
            var saved = false;
            UserSaves userSaves = new UserSaves();
            userSaves.SavedProjectsUsersID = projid;
            userSaves.SavedProjectsUsersID = userid;
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.RequestUri = new Uri("https://localhost:44330/api/SavedProject/SaveProject");
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(userSaves);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var client = HttpClientFactory.Create();
            var response = client.PostAsync(message.RequestUri, data);
            var result = response.Result;

            if (result.IsSuccessStatusCode)
            {
                saved = true;
            }
            return saved;
        }
        public bool RemoveSaveProject(int saveid)
        {
            var removed = false;
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/SavedProject/RemoveSaveProject/" + saveid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            if (result.IsSuccessStatusCode)
            {
                removed = true;
            }
            return removed;
        }
        public List<MiniProjectInfo> GetMiniProjects()
        {
            List<MiniProjectInfo> miniprojects = new List<MiniProjectInfo>();
            foreach (var proj in GetAllProjectsAsync().Result)
            {
                MiniProjectInfo mini = new MiniProjectInfo();
                mini.ProjectID = proj.ProjectID;
                mini.ProjectName = proj.ProjectName;
                mini.ProjectOwner = proj.ProjectOwner;
                mini.ProjectBlurb = proj.ProjectBlurb;
                mini.ProjectCreationDate = proj.ProjectCreationDate;
                mini.ProjectsCategory = proj.ProjectsCategory;
                mini.ProjectSaveNumber = GetProjectSaveNumberAsync(proj.ProjectID).Result;
                mini.ProjectLikeNumber = GetProjectLikeNumberAsync(proj.ProjectID).Result;
                miniprojects.Add(mini);
            }
            return miniprojects;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}