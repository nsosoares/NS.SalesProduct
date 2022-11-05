using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Notifications;
using NS.SalesProduct.Business.Services;
using NS.SalesProduct.Infra.Data.Repositorys;

namespace NS.SalesProduct.Services.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //SERVICES
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISaleService, SaleService>();

            //DAOs
            services.AddScoped<IProductDao, ProductDao>();
            services.AddScoped<ICustomerDao, CustomerDao>();
            services.AddScoped<ISaleItemDao, SaleItemDao>();
            services.AddScoped<ISaleDao, SaleDao>();

            //ANY
            services.AddScoped<INotificationHandler, NotificationHandler>();

            return services;
        }
    }
}
