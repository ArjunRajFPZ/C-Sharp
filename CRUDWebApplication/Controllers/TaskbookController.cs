using CRUDWebApplication.Data;
using CRUDWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult> TaskbookAdd(TaskbookAddModel addTask) 
        {
            var taskbook = new TaskbookModel()
            {
                Id = new Guid(),
                Name = addTask.Name,
                Assignedfrom = addTask.Assignedfrom,
                Assignedto = addTask.Assignedto,
                Assigneddate = addTask.Assigneddate,
                Status = addTask.Status
            };
            await databaseContext.TaskbookData.AddAsync(taskbook);
            await databaseContext.SaveChangesAsync();
            return RedirectToAction("TaskbookAdd");
        }
    }
}
