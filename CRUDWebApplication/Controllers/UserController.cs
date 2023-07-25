using CRUDWebApplication.Data;
using CRUDWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<IActionResult> UserView()
        {
            #region User View
            var users = await databaseContext.UserData.ToListAsync();
            return View(users);
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> UserAdd(UserAddModel userAdd)
        {
            #region User Add
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
            return RedirectToAction("UserView"); 
            #endregion
        }
        [HttpGet]
        public async Task<IActionResult> UserEdit(Guid id)
        {
            #region User Edit 
            var user = await databaseContext.UserData.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                var userEdit = new UserEditModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Dateofbirth = user.Dateofbirth,
                    Phone = user.Phone,
                    Address = user.Address,
                    Email = user.Email,
                    Password = user.Password,
                };
                return View(userEdit);
            }            
            return RedirectToAction("UserView"); 
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditModel userEdit)
        {
            #region User Update
            var user = await databaseContext.UserData.FindAsync(userEdit.Id);
            if (user != null)
            {
                user.Name = userEdit.Name;
                user.Dateofbirth = userEdit.Dateofbirth;
                user.Phone = userEdit.Phone;
                user.Address = userEdit.Address;
                user.Email = userEdit.Email;
                user.Password = userEdit.Password;

                await databaseContext.SaveChangesAsync();
                return RedirectToAction("UserView");
            }
            return View("UserView"); 
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(UserEditModel userDelete)
        {
            #region User Delete
            var user = await databaseContext.UserData.FindAsync(userDelete.Id);
            if (user != null)
            {
                databaseContext.UserData.Remove(user);
                await databaseContext.SaveChangesAsync();

                return RedirectToAction("UserView");
            }
            return View("UserView");
            #endregion
        }
    }
}
