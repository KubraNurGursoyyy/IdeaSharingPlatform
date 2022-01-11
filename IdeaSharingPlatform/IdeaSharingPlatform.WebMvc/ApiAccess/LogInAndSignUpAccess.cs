using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.WebApi.Models;
using Newtonsoft.Json;

namespace IdeaSharingPlatform.WebMvc.ApiAccess
{
    public class LogInAndSignUpAccess
    {
        
        public async Task<bool> LogInAsync(LogInModel logInModel)
        {
            var message = new HttpRequestMessage();
            message.RequestUri = new Uri("https://localhost:44330/api/LogInAndSignUp/LogInUser");
            message.Headers.Add("Accept", "application/json");
            message.Method = HttpMethod.Post;

            var json = JsonConvert.SerializeObject(logInModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = HttpClientFactory.Create();
            var response = client.PostAsync(message.RequestUri, data);
            var result = response.Result;

            bool logın = false;

            if (result.IsSuccessStatusCode)
            {
                if(result.ReasonPhrase == "OK")
                {
                    logın = true;
                }
                else
                {
                    logın = false;
                }
            }
            return logın;
        }

        public async Task<bool> SignUpAsync(Users user)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.RequestUri = new Uri("https://localhost:44330/api/LogInAndSignUp/SignUpUser");
            message.Headers.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var client = HttpClientFactory.Create();
            var response = client.PostAsync(message.RequestUri, data);
            var result = response.Result;

            bool signup = false;

            if (result.IsSuccessStatusCode)
            {
                if (result.ReasonPhrase == "OK")
                {
                    signup = true;
                }
                else
                {
                    signup = false;
                }
            }
            return signup;
        }


    }
}