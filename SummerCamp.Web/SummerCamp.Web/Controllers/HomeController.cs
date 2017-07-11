
using Newtonsoft.Json;
using SummerCamp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SummerCamp.Web.Controllers
{
    public class HomeController : Controller
    {
        private Task<HttpResponseMessage> client;


        public string Baseurl { get; private set; }


        //Hosted web API REST Service base url  

        public async Task<ActionResult> Index()
        {
            List<Announcement> AnnouncementInfo = new List<Announcement>();
            string Baseurl = "http://api.summercamp.stage02.netromsoftware.ro";
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnnouncements using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/announcements");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AnnouncementResp = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Announcement list  
                    AnnouncementInfo = JsonConvert.DeserializeObject<List<Announcement>>(AnnouncementResp);

                }
                //returning the Announcement list to view  
                return View(AnnouncementInfo);
            }

        }


        //public async Task<ActionResult> Categories()
        //{
        //    List<Categories> CategoriesInfo = new List<Categories>();
        //    string Baseurl = "http://api.summercamp.stage02.netromsoftware.ro";
        //    using (var client = new HttpClient())
        //    {
        //        //Passing service base url  
        //        client.BaseAddress = new Uri(Baseurl);

        //        client.DefaultRequestHeaders.Clear();
        //        //Define request data format  
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        //Sending request to find web api REST service resource GetAllAnnouncements using HttpClient  
        //        HttpResponseMessage Res = await client.GetAsync("api/categories");

        //        //Checking the response is successful or not which is sent using HttpClient  
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            //Storing the response details recieved from web api   
        //            var CategoriesResp = Res.Content.ReadAsStringAsync().Result;

        //            //Deserializing the response recieved from web api and storing into the Announcement list  
        //            CategoriesInfo = JsonConvert.DeserializeObject<List<Categories>>(CategoriesResp);

        //        }
        //        //returning the Announcement list to view  
        //        return View(CategoriesInfo);
        //    }

        //}
        

        public ActionResult Categories()
        {
            List<SelectListItem> ObjItem = new List<SelectListItem>()
            {
          new SelectListItem {Text="Select",Value="0",Selected=true },
          new SelectListItem {Text="Auto",Value="1" },
          new SelectListItem {Text="Imobiliare",Value="2"},
          new SelectListItem {Text="Electronice",Value="3"},
          new SelectListItem {Text="Arta",Value="4" },
            };
            ViewBag.ListItem = ObjItem;
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            AnnouncementDetails AnnouncementInfo = new AnnouncementDetails();
            string Baseurl = "http://api.summercamp.stage02.netromsoftware.ro/";
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnnouncements using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/announcements/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AnnouncementResp = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Announcement list  
                    AnnouncementInfo = JsonConvert.DeserializeObject<AnnouncementDetails>(AnnouncementResp);

                }

               
                return View(AnnouncementInfo);
            }

        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(AnnouncementCreate nou)
        {
            string url = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/NewAnnouncement";
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync(url, nou).Result;
                if (result.IsSuccessStatusCode)
                {
                    nou = result.Content.ReadAsAsync<AnnouncementCreate>().Result;
                    ViewBag.Result = "Succesfully saved!";
                    ModelState.Clear();

                    return View(new AnnouncementCreate());
                }
                else
                {
                    ViewBag.Result = "Error!Please try again with valid data";
                }
            }
            return View(nou);
        }

        public ActionResult Close(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Close(int id,AnnouncementClose nou)
        {
            string url = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/CloseAnnouncement?announcementId="+id;
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync(url, nou).Result;       
            }
            return RedirectToAction("Index");
        }


        public ActionResult Extend(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Extend(int id, AnnouncementExtend nou)
        {
            string url = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/ExtendAnnouncement?announcementId=" + id;
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync(url, nou).Result;
            }
            return RedirectToAction("Index");
        }


        public ActionResult Activate(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Activate(int id, AnnouncementActivate nou)
        {
            string url = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/ActivateAnnouncement?announcementId=" + id;
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync(url, nou).Result;
            }
            return RedirectToAction("Index");
        }




        public ActionResult Review()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Review(AnnouncementNewReview nou)
        {
            string url = "http://api.summercamp.stage02.netromsoftware.ro/api/reviews/NewReview";
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync(url, nou).Result;
                if (result.IsSuccessStatusCode)
                {
                    nou = result.Content.ReadAsAsync<AnnouncementNewReview>().Result;
                    ViewBag.Result = "Succesfully saved!";
                    ModelState.Clear();

                    return View(new AnnouncementNewReview());
                }
                else
                {
                    ViewBag.Result = "Error!Please try again with valid data";
                }

            }
            return View(nou);

        }


        public async Task<ActionResult> GetReview(int id)
        {
            List<AnnouncementGetReview> AnnouncementInfo = new List<AnnouncementGetReview>();
            string Baseurl = "http://api.summercamp.stage02.netromsoftware.ro/";
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnnouncements using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("/api/reviews/GetByAnnouncementId?announcementId=" + id);

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AnnouncementResp = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Announcement list  
                    AnnouncementInfo = JsonConvert.DeserializeObject<List<AnnouncementGetReview>>(AnnouncementResp);

                }

                //AnnouncementInfo.Id = id;
                //AnnouncementInfo.Title = Title;
                //AnnouncementInfo.Description = Description;
                //returning the Announcement list to view  
                return View(AnnouncementInfo);
            }

        }

       





    }
}