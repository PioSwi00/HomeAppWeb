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
        await _next(context);
        var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
        var dbContext = context.RequestServices.GetRequiredService<DatabaseContext>();

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
