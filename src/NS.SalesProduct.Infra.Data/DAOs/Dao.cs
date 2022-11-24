using Microsoft.Extensions.Configuration;

namespace NS.SalesProduct.Infra.Data.DAOs
{
    public abstract class Dao
    {
        protected readonly string _connectionString;
        protected readonly IConfiguration _configuration;
        protected Dao(IConfiguration configuration)
        {
            _configuration = configuration; 
            _connectionString = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }
    }
}
