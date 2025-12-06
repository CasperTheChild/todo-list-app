using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TodoList.Services.Database.Context;
using TodoList.Services.Database.Entities;
using TodoList.Services.Database.Identity;

public static class SeedData
{
    public static async Task InitializeAsync(
        TodoListDbContext context,
        UserManager<ApplicationUser> userManager,
        ILogger logger)
    {
        try
        {
            var defaultEmail = "demo@demo.com";
            var defaultPassword = "Password123!";

            var user = await userManager.FindByEmailAsync(defaultEmail);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = defaultEmail,
                    Email = defaultEmail
                };

                var result = await userManager.CreateAsync(user, defaultPassword);
                if (!result.Succeeded)
                {
                    logger.LogError("Failed to create default user. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new Exception("Could not create default user for seeding.");
                }
                logger.LogInformation("Default user created: {Email}", defaultEmail);
            }
            else
            {
                logger.LogInformation("Default user already exists: {Email}", defaultEmail);
            }

            if (context.TodoLists.Any())
            {
                logger.LogInformation("TodoLists already exist — skipping seeding todo lists.");
                return;
            }

            // verify the type of user.Id and of TodoListEntity.UserId!
            logger.LogInformation("Seeding todo lists for user id {UserId}", user.Id);

            var todoLists = new List<TodoListEntity>
            {
                new () { Title = "Personal", Description = "Personal goals", UserId = user.Id },
                new () { Title = "Work", Description = "Work projects", UserId = user.Id },
                new () { Title = "Shopping", Description = "Groceries and errands", UserId = user.Id },
                new () { Title = "Fitness", Description = "Workout and health plans", UserId = user.Id },
                new () { Title = "Travel", Description = "Trips and planning", UserId = user.Id },
            };

            await context.TodoLists.AddRangeAsync(todoLists);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} todo lists.", todoLists.Count);

            // tasks...
            var allLists = context.TodoLists.ToList();
            var tasks = new List<TaskEntity>
            {
                new () { Title = "Morning run", Description = "5km jog", TodoListId = allLists.First(l => l.Title == "Fitness").Id, UserId = user.Id },
                new () { Title = "Buy groceries", Description = "Milk, eggs, bread", TodoListId = allLists.First(l => l.Title == "Shopping").Id, UserId = user.Id },
                new () { Title = "Finish report", Description = "End-of-day work report", TodoListId = allLists.First(l => l.Title == "Work").Id, UserId = user.Id },
                new () { Title = "Book hotel", Description = "Summer vacation hotel", TodoListId = allLists.First(l => l.Title == "Travel").Id, UserId = user.Id },
                new () { Title = "Call mom", Description = "Weekly catch-up", TodoListId = allLists.First(l => l.Title == "Personal").Id, UserId = user.Id },
            };

            await context.Tasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} tasks.", tasks.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "SeedData.InitializeAsync failed.");
            throw;
        }
    }
}
