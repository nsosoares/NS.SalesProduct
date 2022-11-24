using Microsoft.Extensions.Configuration;
using Npgsql;
using NS.SalesProduct.Business.Enuns;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Infra.Data.DAOs;
using System.Data;

namespace NS.SalesProduct.Infra.Data.Repositorys
{
    public class SaleDao : Dao, ISaleDao
    {
        public SaleDao(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IReadOnlyCollection<Sale>> GetAllAsync()
        {
            var sales = new List<Sale>();
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"sale\"", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var sale = new Sale(
                            Guid.Parse(reader["Id"].ToString()),
                            Guid.Parse(reader["CustomerId"].ToString()),
                            decimal.Parse(reader["TotalPrice"].ToString()),
                            int.Parse(reader["Discount"].ToString()),
                            (EPaymentMethod)reader["PaymentMethod"]
                            );
                        sales.Add(sale);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
                
            }
            return sales;
        }

        public async Task<Sale> GetByIdAsync(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM \"sale\" WHERE \"Id\" = '{id}'", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var sale = new Sale(
                            Guid.Parse(reader["Id"].ToString()),
                            Guid.Parse(reader["CustomerId"].ToString()),
                            decimal.Parse(reader["TotalPrice"].ToString()),
                            int.Parse(reader["Discount"].ToString()),
                            (EPaymentMethod)reader["PaymentMethod"]
                            );
                        return sale;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
            return null;
        }

        public async Task RegisterAsync(Sale entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"INSERT INTO \"sale\" (\"Id\", \"CustomerId\", \"TotalPrice\", \"Discount\", \"PaymentMethod\") VALUES(@Id, @CustomerId, @TotalPrice, @Discount, @PaymentMethod)";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                    cmd.Parameters.AddWithValue("@TotalPrice", entity.TotalPrice);
                    cmd.Parameters.AddWithValue("@Discount", entity.Discount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", entity.PaymentMethod);
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

        public async Task UpdateAsync(Sale entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"UPDATE \"sale\" SET \"CustomerId\" = @CustomerId, \"TotalPrice\" = @TotalPrice, \"Discount\" = @Discount, \"PaymentMethod\" = @PaymentMethod";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                    cmd.Parameters.AddWithValue("@TotalPrice", entity.TotalPrice);
                    cmd.Parameters.AddWithValue("@Discount", entity.Discount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", entity.PaymentMethod);
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

        public async Task DeleteAsync(Sale entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"DELETE from \"sale\" WHERE \"Id\" = '{entity.Id}'";
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
