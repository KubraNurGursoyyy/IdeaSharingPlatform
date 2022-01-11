using IdeaSharingPlatform.Models.Concretes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace IdeaSharingPlatform.WebMvc.ApiAccess
{
    public class UserAccess : IDisposable
    {
        ProjectAccess projectAccess;
        public async Task<List<Projects>> GetUserProjects(int id) 
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/User/GetUserProjects/" + id);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            projectAccess = new ProjectAccess();

            var usersprojects = new List<Projects>();
            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    usersprojects = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Projects>>(await responseStream);
                }
                for (int i = 0; i < usersprojects.Count; i++)
                {
                    usersprojects[i] = projectAccess.SeeProjectDetailsAsync(usersprojects[i].ProjectID).Result;
                }
            }
            else
            {
                usersprojects = (List<Projects>)Enumerable.Empty<Projects>();

            }
            return usersprojects;

        }
        public async Task<List<Projects>> GetUserLikedProjects(int id) 
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/User/GetUserLikedProjects/" + id);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            projectAccess = new ProjectAccess();

            var userslikedprojects = new List<Projects>();
            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    userslikedprojects = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Projects>>(await responseStream);
                }
                for (int i = 0; i < userslikedprojects.Count; i++)
                {
                    userslikedprojects[i] = projectAccess.SeeProjectDetailsAsync(userslikedprojects[i].ProjectID).Result;
                }
            }
            else
            {
                userslikedprojects = (List<Projects>)Enumerable.Empty<Projects>();

            }
            return userslikedprojects;
        }
        public async Task<List<Projects>> GetUserSavedProjects(int id) 
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/User/GetUserSavedProjects/" + id);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            projectAccess = new ProjectAccess();

            var userssavedprojects = new List<Projects>();
            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    userssavedprojects = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Projects>>(await responseStream);
                }
                for (int i = 0; i < userssavedprojects.Count; i++)
                {
                   userssavedprojects[i] = projectAccess.SeeProjectDetailsAsync(userssavedprojects[i].ProjectID).Result;
                }
            }
            else
            {
                userssavedprojects = (List<Projects>)Enumerable.Empty<Projects>();

            }
            return userssavedprojects;
        }
        public bool DeleteUser(int id)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Delete;
            message.RequestUri = new Uri("https://localhost:44330/api/User/DeleteUser/" + id);
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
        public bool UpdateUser(Users user, int id)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Put;
            message.RequestUri = new Uri("https://localhost:44330/api/User/PutUser/" + id);
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(user);
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



        public async Task<Users> GetUserAsync(int userid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/User/GetUser/"+ userid);
            message.Headers.Add("Accept", "application/json");

            var client = HttpClientFactory.Create();
            var response = client.SendAsync(message);
            var result = response.Result;

            var users = new Users();
            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    users = await System.Text.Json.JsonSerializer.DeserializeAsync<Users>(await responseStream);
                }
                users.UsersProjects = GetUserProjects(userid).Result;
            }
            else
            {
                users = (Users)Enumerable.Empty<Users>();

            }
            return users;

        }

        public async Task<int> GetUserIDByEmail(string email)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.RequestUri = new Uri("https://localhost:44330/api/User/GetUserIDByEmail");
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(email);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = HttpClientFactory.Create();
            var response = client.PostAsync(message.RequestUri, data);
            var result = response.Result;

            int userid = 0;

            if (result.IsSuccessStatusCode)
            {
                using (var responseStream = result.Content.ReadAsStreamAsync())
                {
                    userid = await System.Text.Json.JsonSerializer.DeserializeAsync<int>(await responseStream);
                }
            }
           
            return userid;

        }

        public void Dispose()
        {
           GC.SuppressFinalize(true);
        }
        /*
public async Task<List<Users>> GetAllUsersAsync()
{
   var message = new HttpRequestMessage();
   message.Method = HttpMethod.Get;
   message.RequestUri = new Uri("https://localhost:44330/api/User/GetAllUsers");
   message.Headers.Add("Accept", "application/json");

   var client = HttpClientFactory.Create();
   var response = client.SendAsync(message);
   var result = response.Result;

   var users = new List<Users>();

   if (result.IsSuccessStatusCode)
   {
       using (var responseStream = result.Content.ReadAsStreamAsync())
       {
           users = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Users>>(await responseStream);
       }
       foreach (var u in users)
       {
           u.UsersProjects = GetUserProjects(u.UserID).Result;
       }
   }
   else
   {
       users = (List<Users>)Enumerable.Empty<List<Users>>();

   }
   return users;
}
*//*
public Users GetUserByEmail(string email)
{
    Users users = new Users();
    foreach (var user in GetAllUsersAsync().Result)
    {
        if (user.UserEmail == email)
        {
            users = user;
        }
    }
    return users;
}*/

    }
}