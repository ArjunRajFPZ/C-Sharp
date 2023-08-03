using CRUDWebApplication.Areas.Identity.Data;
using CRUDWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRUDWebApplication.Controllers
{
    public class SPTaskbookController : Controller
    {
        SPTaskbookConnection _connection = new SPTaskbookConnection();

        public IActionResult TaskbookAdd()
        {
            return View();
        }

        public IActionResult TaskbookView()
        {
            #region Taskbook View
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var taskbookList = _connection.TaskbookDataView(userEmail);
            return View(taskbookList);
            #endregion
        }

        [HttpPost]
        public IActionResult TaskbookAdd(SPTaskbookModel taskbook)
        {
            #region Taskbook Add
            if (!ModelState.IsValid)
            {
                TempData["addErrorMessage"] = "Task Addition To Taskbook Failed!";
                return RedirectToAction("TaskbookAdd");
            }
            else
            {
                bool result = _connection.TaskbookDataAdd(taskbook);
                if (result == true)
                {
                    TempData["addSuccessMessage"] = "Task Added To Taskbook Successfully.";
                    return RedirectToAction("TaskbookView");
                }
                else
                {
                    TempData["addErrorMessage"] = "Task Addition To Taskbook Failed!";
                    return RedirectToAction("TaskbookAdd");
                }
            }
            #endregion
        }

        [HttpGet]
        public IActionResult TaskbookEdit(int id)
        {
            #region Taskbook Edit
            var taskbook = _connection.TaskbookDataEdit(id);
            return View(taskbook);
            #endregion
        }
        [HttpPost]
        public IActionResult TaskbookEdit(SPTaskbookModel taskbook)
        {
            #region Taskbook Update
            if (!ModelState.IsValid)
            {
                TempData["updateErrorMessage"] = "Task Updation To Taskbook Failed!";
                return RedirectToAction("TaskbookView");
            }
            else
            {
                bool result = _connection.TaskbookDataUpdate(taskbook);
                if (result == true)
                {
                    TempData["updateSuccessMessage"] = "Task Updated To Taskbook Successfully.";
                    return RedirectToAction("TaskbookView");
                }
                else
                {
                    TempData["updateErrorMessage"] = "Task Updation To Taskbook Failed!";
                    return RedirectToAction("TaskbookView");
                }
            }
            #endregion            
        }
        public IActionResult TaskbookDelete(int id)
        {
            #region Taskbook Delete
            var taskbook = _connection.TaskbookDataDelete(id);
            if (taskbook == true)
            {
                TempData["deleteSuccessMessage"] = "Task Deleted From Taskbook Successfully.";
                return RedirectToAction("TaskbookView");
            }
            else
            {
                TempData["deleteErrorMessage"] = "Task Deletion From Taskbook Failed!";
                return RedirectToAction("TaskbookAdd");
            }
            #endregion
        }
    }
}
