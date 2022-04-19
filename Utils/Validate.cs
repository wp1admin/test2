using producer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace producer.Utils
{
    public class Validate
    {
        public static APIresult CheckUserInfo(Userinfo user)
        {
            using (var client = new HttpClient())
            {
                // Setting Base address.  
                Uri uri = new Uri("https://reqres.in/api/login");

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Clear();

                APIresult apiResult = new APIresult();

                try
                {
                    var userinfo = JsonSerializer.Serialize(user);
                    var requestContent = new StringContent(userinfo, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(uri, requestContent).Result;

                    //HttpResponseMessage response = client.PostAsync($"api/login").Result;
                    response.EnsureSuccessStatusCode();

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  

                        apiResult.hasErr = false;

                    }
                    else
                    {
                        apiResult.hasErr = true;


                    }
                    return apiResult;
                }
                catch (Exception ex)
                {
                    apiResult.hasErr = true;
                    apiResult.email = "";

                    return apiResult;
                }
            }
        }
    }
}
