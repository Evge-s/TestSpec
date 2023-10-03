using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Net.Identity.API.APIs;
using Server.Net.Identity.Infrastructure.DataBase;
using Server.Net.Identity.Infrastructure.Interfaces;
using Server.Net.Identity.Infrastructure.Repositories;

namespace Server.Net.Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string ConnectionString = builder.Configuration.GetConnectionString("IdentityDB")!;
            builder.Services.AddDbContext<IdentityDbContext>(c => c.UseSqlServer(ConnectionString));

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddFastEndpoints();
            //builder.Services.AddAuthorization();

            var app = builder.Build();

            //app.UseAuthorization();
            app.UseFastEndpoints();

            app.Run();
        }
    }
}