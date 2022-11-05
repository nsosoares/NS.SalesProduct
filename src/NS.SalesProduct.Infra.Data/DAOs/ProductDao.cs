using Npgsql;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Infra.Data.DAOs;
using System.Data;

namespace NS.SalesProduct.Infra.Data.Repositorys
{
    public class ProductDao : Dao, IProductDao
    {
        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            var products = new List<Product>();
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"product\"", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Product product = new Product(
                            Guid.Parse(reader["Id"].ToString()),
                            reader["Name"].ToString(),
                            decimal.Parse(reader["Price"].ToString()),
                            decimal.Parse(reader["CostPrice"].ToString()));
                        products.Add(product);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
                
            }
            return products;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM \"product\" WHERE \"Id\" = '{id}'", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Product product = new Product(
                            Guid.Parse(reader["Id"].ToString()),
                            reader["Name"].ToString(),
                            decimal.Parse(reader["Price"].ToString()),
                            decimal.Parse(reader["CostPrice"].ToString()));
                        con.Close();
                        return product;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
            return null;
        }

        public async Task RegisterAsync(Product entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"INSERT INTO \"product\" (\"Id\", \"Name\", \"Price\", \"CostPrice\") VALUES(@Id, @Name, @Price, @CostPrice)";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Price", entity.Price);
                    cmd.Parameters.AddWithValue("@CostPrice", entity.CostPrice);
                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
        }

        public async Task UpdateAsync(Product entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"UPDATE \"product\" SET \"Name\" = @Name, \"Price\" = @Price, \"CostPrice\" = @CostPrice";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Price", entity.Price);
                    cmd.Parameters.AddWithValue("@CostPrice", entity.CostPrice);
                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
        }

        public async Task DeleteAsync(Product entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"DELETE from \"product\" WHERE \"Id\" = '{entity.Id}'";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
        }


        public void Dispose()
        {
            
        }
    }
}
