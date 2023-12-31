﻿using CRUDWebApplication.Data;
using CRUDWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRUDWebApplication.Controllers
{
    public class TaskbookController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public TaskbookController(DatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext;
        }

        [HttpGet]
        public IActionResult TaskbookAdd()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> TaskbookView()
        {
            #region Taskbook View
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            List<TaskbookModel> taskbooks = await databaseContext.TaskbookData.Where(x => x.Email == userEmail).ToListAsync();
            return View(taskbooks);
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> TaskbookAdd(TaskbookAddModel TaskAdd)
        {
            #region Taskbook Add 
            var taskbook = new TaskbookModel()
            {
                Id = new Guid(),
                Name = TaskAdd.Name,
                Assignedfrom = TaskAdd.Assignedfrom,
                Assignedto = TaskAdd.Assignedto,
                Assigneddate = TaskAdd.Assigneddate,
                Status = TaskAdd.Status,
                Email = TaskAdd.Email
            };
            await databaseContext.TaskbookData.AddAsync(taskbook);
            await databaseContext.SaveChangesAsync();
            TempData["addSuccessMessage"] = "Task Added To Taskbook Successfully.";
            return RedirectToAction("TaskbookView");
            #endregion
        }
        [HttpGet]
        public async Task<IActionResult> TaskbookEdit(Guid id)
        {
            #region Taskbook Edit 
            var taskbook = await databaseContext.TaskbookData.FirstOrDefaultAsync(x => x.Id == id);
            if (taskbook != null)
            {
                var taskbookEdit = new TaskbookEditModel()
                {
                    Id = taskbook.Id,
                    Name = taskbook.Name,
                    Assignedfrom = taskbook.Assignedfrom,
                    Assignedto = taskbook.Assignedto,
                    Assigneddate = taskbook.Assigneddate,
                    Status = taskbook.Status,
                    Email = taskbook.Email
                };
                return View(taskbookEdit);
            }
            return RedirectToAction("TaskbookView");
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> TaskbookEdit(TaskbookEditModel taskbookEdit)
        {
            #region Taskbook Update
            var taskbook = await databaseContext.TaskbookData.FindAsync(taskbookEdit.Id);
            if (taskbook != null)
            {
                taskbook.Name = taskbookEdit.Name;
                taskbook.Assignedfrom = taskbookEdit.Assignedfrom;
                taskbook.Assignedto = taskbookEdit.Assignedto;
                taskbook.Assigneddate = taskbookEdit.Assigneddate;
                taskbook.Status = taskbookEdit.Status;

                await databaseContext.SaveChangesAsync();
                TempData["updateSuccessMessage"] = "Task Updated To Taskbook Successfully.";
                return RedirectToAction("TaskbookView");
            }
            TempData["updateErrorMessage"] = "Task Updation To Taskbook Failed!";
            return View("TaskbookView");
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> TaskbookDelete(TaskbookEditModel taskbookDelete)
        {
            #region Taskbook Delete
            var taskbook = await databaseContext.TaskbookData.FindAsync(taskbookDelete.Id);
            if (taskbook != null)
            {
                databaseContext.TaskbookData.Remove(taskbook);
                await databaseContext.SaveChangesAsync();
                TempData["deleteSuccessMessage"] = "Task Deleted From Taskbook Successfully.";
                return RedirectToAction("TaskbookView");
            }
            TempData["deleteErrorMessage"] = "Task Deletion From Taskbook Failed!";
            return View("TaskbookView");
            #endregion
        }
    }
}
