namespace NS.SalesProduct.Infra.Data.DAOs
{
    public abstract class Dao
    {
        protected readonly string _connectionString;
        protected Dao()
        {
            _connectionString = "Host=localhost:5433;Username=postgres;Password=123456789;Database=NsSaleProducts";
        }
    }
}
