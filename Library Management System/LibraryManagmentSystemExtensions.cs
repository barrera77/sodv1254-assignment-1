using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Library_Management_System.BLL;
using Library_Management_System.DAL;

namespace Library_Management_System
{
    public static class LibraryManagmentSystemExtensions
    {
        public static void LibraryManagementSystemDependencies(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<LibrarydbContext>(options);

            services.AddTransient<BorrowerServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<LibrarydbContext>();
                return new BorrowerServices(context!);
            });

            services.AddTransient<MediaServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<LibrarydbContext>();
                return new MediaServices(context!);
            });

            services.AddTransient<TransactionServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<LibrarydbContext>();
                return new TransactionServices(context!);
            });
        }

    }
}
