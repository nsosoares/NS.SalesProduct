using Npgsql;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Infra.Data.DAOs;
using System.Data;

namespace NS.SalesProduct.Infra.Data.Repositorys
{
    public class CustomerDao : Dao, ICustomerDao
    {
        public async Task<IReadOnlyCollection<Customer>> GetAllAsync()
        {
            var customers = new List<Customer>();
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"customer\"", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var customer = new Customer(
                            Guid.Parse(reader["Id"].ToString()),
                            reader["Name"].ToString(),
                            reader["Cpf"].ToString(),
                            Convert.ToDateTime(reader["BirthDate"]));
                        customers.Add(customer);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
                
            }
            return customers;
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM \"customer\" WHERE \"Id\" = '{id}'", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var customer = new Customer(
                            Guid.Parse(reader["Id"].ToString()),
                            reader["Name"].ToString(),
                            reader["Cpf"].ToString(),
                            Convert.ToDateTime(reader["BirthDate"]));
                        con.Close();
                        return customer;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
            return null;
        }

        public async Task<Customer> GetByCpfAsync(string cpf)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM \"customer\" WHERE \"Cpf\" ilike '{cpf}'", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var customer = new Customer(
                            Guid.Parse(reader["Id"].ToString()),
                            reader["Name"].ToString(),
                            reader["Cpf"].ToString(),
                            Convert.ToDateTime(reader["BirthDate"]));
                        con.Close();
                        return customer;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
            return null;
        }

        public async Task RegisterAsync(Customer entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"INSERT INTO \"customer\" (\"Id\", \"Name\", \"Cpf\", \"BirthDate\") VALUES(@Id, @Name, @Cpf, @BirthDate)";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Cpf", entity.Cpf);
                    cmd.Parameters.AddWithValue("@BirthDate", entity.BirthDate);
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

        public async Task UpdateAsync(Customer entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"UPDATE \"customer\" SET \"Name\" = @Name, \"Cpf\" = @Cpf, \"BirthDate\" = @BirthDate";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Cpf", entity.Cpf);
                    cmd.Parameters.AddWithValue("@BirthDate", entity.BirthDate);
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

        public async Task DeleteAsync(Customer entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"DELETE from \"customer\" WHERE \"Id\" = '{entity.Id}'";
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
