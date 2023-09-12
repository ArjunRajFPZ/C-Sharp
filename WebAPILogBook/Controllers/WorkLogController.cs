using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using WebAPILogBook.Models;

namespace WebAPILogBook.Controllers
{
    public class WorkLogController : Controller
    {
        HttpClient client = new HttpClient();

        #region WorkLog View
        public ActionResult WorkLogView()
        {
            List<WorkLogModel> logs = new List<WorkLogModel>();
            client.BaseAddress = new Uri("https://localhost:44378/api/LogAPI/WorkLogView");
            var responseMessage = client.GetAsync("WorkLogView");
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var logData = result.Content.ReadAsAsync<List<WorkLogModel>>();
                logs = logData.Result;
            }
            return View(logs);
        }
        #endregion

        #region WorkLog Add View
        public ActionResult WorkLogAddView()
        {
            return View();
        }
        #endregion

        #region WorkLog Add
        [HttpPost]
        public ActionResult WorkLogAdd(WorkLogModel workLog)
        {
            client.BaseAddress = new Uri("https://localhost:44378/api/LogAPI/WorkLogAdd");
            var responseMessage = client.PostAsJsonAsync("WorkLogAdd", workLog);
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "success";
                return RedirectToAction("WorkLogView");
            }
            else
            {
                TempData["errorMessage"] = "error";
                return RedirectToAction("WorkLogAddView");
            }
        }
        #endregion
    }
}