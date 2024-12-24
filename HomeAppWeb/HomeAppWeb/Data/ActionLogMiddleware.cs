using HomeAppWeb.Data;
using HomeAppWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class ActionLogMiddleware
{
    private readonly RequestDelegate _next;

    public ActionLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Call the next middleware in the pipeline
        await _next(context);

        // Resolve the UserManager service from the request scope
        var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
        var dbContext = context.RequestServices.GetRequiredService<DatabaseContext>();

        // Log the action after the request has been processed
        var userEmail = context.User.FindFirstValue(ClaimTypes.Email);
        if (!string.IsNullOrEmpty(userEmail))
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {
                var actionLog = new ActionLog
                {
                    ActionLogId = Guid.NewGuid(),
                    Action = $"{context.Request.Method} {context.Request.Path}",
                    Timestamp = DateTime.UtcNow,
                    UserId = user.Id,
                    User = user
                };

                dbContext.ActionLogs.Add(actionLog);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
