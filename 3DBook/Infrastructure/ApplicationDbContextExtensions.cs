using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _3DBook.Infrastructure;

public static class ApplicationDbContextExtensions
{
    public static void AddApplicationDbContex(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));
}