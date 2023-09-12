using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using WebAPILogBook.Models;

namespace WebAPILogBook.Controllers
{
    public class UserController : Controller
    {
        HttpClient client = new HttpClient();

        #region Add View
        [AllowAnonymous]
        public ActionResult UserAddView()
        {
            return View();
        }
        #endregion

        #region POST User Data
        [AllowAnonymous]
        public ActionResult UserAdd(UserModel userModel)
        {
            client.BaseAddress = new Uri("https://localhost:44378/api/API/InsertUserData");
            var responseMessage = client.PostAsJsonAsync("InsertUserData", userModel);
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "success";
                return RedirectToAction("UserAddView");
            }
            else
            {
                TempData["errorMessage"] = "error";
                return RedirectToAction("UserAddView");
            }
        }
        #endregion

        #region GET User Data
        public ActionResult UserView()
        {
            List<UserModel> users = new List<UserModel>();
            client.BaseAddress = new Uri("https://localhost:44378/api/API/RetriveUserData");
            var responseMessage = client.GetAsync("RetriveUserData");
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var userData = result.Content.ReadAsAsync<List<UserModel>>();
                users = userData.Result;
            }
            return View(users);
        }
        #endregion

        #region Login View
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region LoginCheck
        [AllowAnonymous]
        public ActionResult LoginCheck(UserModel userModel)
        {
            var users = "";
            client.BaseAddress = new Uri("https://localhost:44378/api/Login/LoginCheck");
            var responseMessage = client.PostAsJsonAsync("LoginCheck", userModel);
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var userData = result.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<string>(userData);
                FormsAuthentication.SetAuthCookie(users, false);
            }
            if (users != "")
            {                
                TempData["successMessage"] = "Login success";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["errorMessage"] = "error";
                return RedirectToAction("Login");
            }
        }
        #endregion

        public string GetTokenEmail()
        {
            var idtoken = User.Identity.Name.Split()[0];
            if (idtoken != "")
            {
                var token = new JwtSecurityToken(jwtEncodedString: idtoken);
                var Email = token.Claims.First(c => c.Type == "Email").Value;
                return Email;
            }
            else
            {
                return null;
            }
        }
        public string GetTokenUserType()
        {
            var idtoken = User.Identity.Name.Split('|')[0];
            if (idtoken != "")
            {
                var token = new JwtSecurityToken(jwtEncodedString: idtoken);
                var UserType = token.Claims.First(c => c.Type == "UserType").Value;
                return UserType;
            }
            else
            {
                return null;
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}