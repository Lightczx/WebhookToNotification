using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebhookToNotification.Services;

namespace WebhookToNotification;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Host.UseWindowsService();

        builder.Services.AddSingleton<ToastNotificationService>();
        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
