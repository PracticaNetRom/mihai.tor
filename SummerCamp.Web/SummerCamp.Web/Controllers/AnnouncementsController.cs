using Newtonsoft.Json;
using SummerCamp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SummerCamp.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        // GET: announcements
            //Hosted web API REST Service base url  
            string Baseurl = "http://api.summercamp.stage02.netromsoftware.ro";
        public async Task<ActionResult> Get()
        {
            List<Announcements> EmpInfo = new List<Announcements>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnnouncementss using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("/api/announcements");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Announcements list  
                    EmpInfo = JsonConvert.DeserializeObject<List<Announcements>>(EmpResponse);

                }
                //returning the Announcements list to view  
                return View(EmpInfo);
            }
        }
    
    }
}