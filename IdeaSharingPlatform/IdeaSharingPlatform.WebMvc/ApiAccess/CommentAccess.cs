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
    public class CommentAccess
    {
        public bool CreateComment(Comments comment)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.RequestUri = new Uri("https://localhost:44330/api/Comments/CreateComment");
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(comment);
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

        public bool DeleteComment(int id)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Delete;
            message.RequestUri = new Uri("https://localhost:44330/api/Comments/DeleteComment/" + id);
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
        public bool UpdateComment(Comments comment, int id)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Put;
            message.RequestUri = new Uri("https://localhost:44330/api/Comments/UpdateComment" + id);
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(comment);
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
        public async Task<Users> GetProjectOwnerAsync(int commentid)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:44330/api/Comments/GetCommentOwner/" + commentid);
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
    }
}