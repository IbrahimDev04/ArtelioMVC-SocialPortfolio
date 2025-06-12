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
            builder.Services.AddServicesRegistration();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.MapControllerRoute("defaut", "{controller=account}/{action=login}/{id?}");

            app.Run();
        }
    }
}
