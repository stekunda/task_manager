using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Reflection;

namespace task_manager.Pages
{
    public class IndexModel : PageModel
    {
        public static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Description = "Build resume", DueDate = new DateTime(2024, 06, 010, 00, 00, 00)},
            new TaskItem { Id = 2, Description = "Find Interview", DueDate = new DateTime(2024, 07, 010, 00, 00, 00)},
            new TaskItem { Id = 3, Description = "Get Job", DueDate = new DateTime(2024, 08, 010, 00, 00, 00)}
        };

        public List<TaskItem> Tasks => tasks;

        public IActionResult OnPostAddTask(TaskItem addTask)
        {
            addTask.Id = tasks.Count + 1;
            tasks.Add(addTask);
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveTask(int taskId)
        {
            var taskToRemove = tasks.FirstOrDefault(t => t.Id == taskId);
            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostEditTask(int id, string newDescription, DateTime newDueDate, bool newIsCompleted, DateTime? newCompletionDate)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            task.Description = newDescription;
            task.DueDate = newDueDate;
            task.IsCompleted = newIsCompleted;
            task.CompletionDate = newCompletionDate;


            return RedirectToPage();
        }

        public IActionResult OnPostMarkCompleted(int taskId)
        {
            var taskToComplete = tasks.FirstOrDefault(t => t.Id == taskId);
            if (taskToComplete != null)
            {
                taskToComplete.IsCompleted = true;
                taskToComplete.CompletionDate = DateTime.Now;
            }
            return RedirectToPage();
        }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public class TaskItem
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
            public bool IsCompleted { get; set; }
            public DateTime? CompletionDate { get; set; }
        }
    }
}