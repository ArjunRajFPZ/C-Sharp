using CRUDWebApplication.Data;
using CRUDWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public UserController(DatabaseContext _databaseContext) 
        {
            databaseContext = _databaseContext;
        }
        [HttpGet]
        public IActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserAdd(UserAddModel userAdd)
        {
            var user = new UserModel()
            {
                Id = new Guid(),
                Name = userAdd.Name,
                Dateofbirth = userAdd.Dateofbirth,
                Phone = userAdd.Phone,
                Address = userAdd.Address,
                Email = userAdd.Email,
                Password = userAdd.Password,
            };
            await databaseContext.UserData.AddAsync(user);
            await databaseContext.SaveChangesAsync();
            return RedirectToAction("UserAdd");
        }
    }
}
