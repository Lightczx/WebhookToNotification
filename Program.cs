using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebhookToNotification.Services;

namespace WebhookToNotification;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<ToastNotificationService>();
        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}