using Artelio.MVC.Hubs;
using Artelio.MVC.Middlewares;

namespace Artelio.MVC
{
    public class Program
    {
        public static void Main(string[] args)
       {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddMicrosoftSql(builder.Configuration.GetConnectionString("Default")!);
            builder.Services.AddIdentity();

            builder.Services.AddCorsService();
            builder.Services.AddSignalR();

            builder.Services.AddServicesRegistration();

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseCors();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.MapControllerRoute("defaut", "{controller=account}/{action=login}/{id?}");

            app.MapHub<ChatHub>("/chathub");


            app.Run();
        }
    }
}
