using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace task_manager.Controllers
{
    using Pages;
    [ApiController]
    [Route("[action]")]
    public class TaskControllers : ControllerBase
    {
        [Route("{id:int}/{newDescription}")]

        public string ChangeDescriptionById(int id, string newDescription)
        {
            var task = IndexModel.tasks.FirstOrDefault(t => t.Id == id);
            task.Description = newDescription;
            return "Task Description has been changed.";
        }

        [Route("{id:int}")]
        public string GetTaskById(int id)
        {
            var task = IndexModel.tasks.FirstOrDefault(t => t.Id == id);
            return task.Id.ToString() + " " + task.Description + " " + task.DueDate + " " + task.IsCompleted + " " + task.CompletionDate;
        }

        public string GetAllTasks()
        {
            string final = "";
            for (int i = 1; i <= IndexModel.tasks.Count; i++)
            {
                var task = IndexModel.tasks.FirstOrDefault(t => t.Id == i);
                string task_string = task.Id.ToString() + " " + task.Description + " " + task.DueDate + " " + task.IsCompleted + " " + task.CompletionDate + "\n";
                final += task_string;
            }
            return final;
        }

        public string GetAllCompletedTasks()
        {
            string final = "";
            for (int i = 1; i <= IndexModel.tasks.Count; i++)
            {
                var task = IndexModel.tasks.FirstOrDefault(t => t.Id == i);
                string task_string = task.Id.ToString() + " " + task.Description + " " + task.DueDate + " " + task.IsCompleted + " " + task.CompletionDate + "\n";
                if (task.IsCompleted)
                {
                    final += task_string;
                }
            }
            return final;
        }

        [Route("{id:int}")]
        public string MarkTaskAsCompleted(int id)
        {
            var task = IndexModel.tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                task.CompletionDate = DateTime.Now;
                return "Task marked as complete.";
            }
            return "Task not found";
        }
    }

}
