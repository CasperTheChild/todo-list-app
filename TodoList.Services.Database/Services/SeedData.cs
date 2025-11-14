using TodoList.Services.Database.Context;
using TodoList.Services.Database.Entities;

namespace TodoList.Services.Database.Services;

public static class SeedData
{
    public static async Task InitializeAsync(TodoListDbContext context)
    {
        if (context.TodoLists.Any())
        {
            return;
        }

        var todoLists = new List<TodoListEntity>
    {
        new () { Title = "Personal", Description = "Personal goals and routines" },
        new () { Title = "Work", Description = "Work-related projects" },
        new () { Title = "Shopping", Description = "Groceries and errands" },
        new () { Title = "Fitness", Description = "Workout and health plans" },
        new () { Title = "Travel", Description = "Trips and planning" },
    };

        context.TodoLists.AddRange(todoLists);
        await context.SaveChangesAsync();

        var allLists = context.TodoLists.ToList();

        var tasks = new List<TaskEntity>
        {
            new () { Title = "Morning run", Description = "5km jog", IsCompleted = false, TodoListId = allLists.First(l => l.Title == "Fitness").Id },
            new () { Title = "Buy groceries", Description = "Milk, eggs, bread", IsCompleted = true, TodoListId = allLists.First(l => l.Title == "Shopping").Id },
            new () { Title = "Finish report", Description = "End-of-day work report", IsCompleted = false, TodoListId = allLists.First(l => l.Title == "Work").Id },
            new () { Title = "Book hotel", Description = "Book hotel for summer vacation", IsCompleted = false, TodoListId = allLists.First(l => l.Title == "Travel").Id },
            new () { Title = "Call mom", Description = "Weekly catch-up", IsCompleted = true, TodoListId = allLists.First(l => l.Title == "Personal").Id },
        };

        context.Tasks.AddRange(tasks);
        await context.SaveChangesAsync();
    }
}
