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
            services.AddDbContext<LibraryDBdbContext>(options);

            services.AddTransient<BorrowerServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<LibraryDBdbContext>();
                return new BorrowerServices(context!);
            });

            services.AddTransient<MediaServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<LibraryDBdbContext>();
                return new MediaServices(context!);
            });

            services.AddTransient<TransactionServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<LibraryDBdbContext>();
                return new TransactionServices(context!);
            });
        }

    }
}
