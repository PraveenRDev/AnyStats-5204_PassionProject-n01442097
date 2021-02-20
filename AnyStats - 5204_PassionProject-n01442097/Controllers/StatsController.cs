using AnyStats___5204_PassionProject_n01442097.Models;
using AnyStats___5204_PassionProject_n01442097.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static AnyStats___5204_PassionProject_n01442097.Models.Coordinate;
using Microsoft.AspNet.Identity;

namespace AnyStats___5204_PassionProject_n01442097.Controllers
{
    public class StatsController : Controller
    {

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        static StatsController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44317/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Stats
        public ActionResult Index()
        {
            return View();
        }

        // GET: Stats/List
        public ActionResult List()
        {
            string url;
            // if a user is authenticated send request to bring private and public stats
            if(User.Identity.IsAuthenticated)
            {
                url = "StatsData/GetAllStats/" + User.Identity.GetUserId();
            }
            else
            {
                // bring only public stats
                url = "StatsData/GetPublicStats";
            }
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<StatDto> PublicStats = response.Content.ReadAsAsync<IEnumerable<StatDto>>().Result;
                return View(PublicStats);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Stats/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stats/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(StatDto Stats)
            
        {
            Stats.AuthorId = User.Identity.GetUserId();
            string url = "StatsData/AddStat";
            HttpContent content = new StringContent(jss.Serialize(Stats));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int statId = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = statId });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Stats/Details/1
        public ActionResult Details(int id)
        {
            ShowStats ViewModel = new ShowStats();
            string url = "StatsData/GetStat/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                //Put data into stat data transfer object
                StatDto Statistic = response.Content.ReadAsAsync<StatDto>().Result;
                ViewModel.stat = Statistic;

                // find associated coordinates of the stat
                url = "CoordinatesData/FindCoordinatesForStats/" + id;
                response = client.GetAsync(url).Result;
                CoordinateDto Coordinates = response.Content.ReadAsAsync<CoordinateDto>().Result;

                ViewModel.coordinates = Coordinates;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Stats/Delete/5
        [HttpGet]
        [Authorize]
        public ActionResult DeleteConfirm(int id, string authorId)
        {
            if (!authorId.Equals(User.Identity.GetUserId()))
            {
                return RedirectToAction("Error", new { message = "Access Denied. Author Permission needed", statId=id });
            }

            string url = "StatsData/GetStat/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                StatDto SelectedStat = response.Content.ReadAsAsync<StatDto>().Result;
                return View(SelectedStat);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Stats/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            string url = "StatsData/DeleteStat/" + id;
            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Stats/Edit/5
        [Authorize]
        public ActionResult Edit(int id, string authorId)
        {
            if (!authorId.Equals(User.Identity.GetUserId()))
            {
                return RedirectToAction("Error", new { message = "Access Denied. Author Permission needed", statId = id });
            }

            UpdateStats ViewModel = new UpdateStats();

            string url = "StatsData/GetStat/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                StatDto Statistic = response.Content.ReadAsAsync<StatDto>().Result;
                ViewModel.stat = Statistic;

                url = "CoordinatesData/FindCoordinatesForStats/" + id;
                response = client.GetAsync(url).Result;
                CoordinateDto Coordinates = response.Content.ReadAsAsync<CoordinateDto>().Result;

                ViewModel.coordinates = Coordinates;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Stats/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, StatDto StatInfo)
        {
            string url = "StatsData/UpdateStat/" + id;
            HttpContent content = new StringContent(jss.Serialize(StatInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error(string message, int statId)
        {
            ErrorModel ViewModel = new ErrorModel();
            ViewModel.ErrorMessage = message;
            ViewModel.StatId = statId;
            return View(ViewModel);
        }

    }
}