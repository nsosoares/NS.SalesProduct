using Npgsql;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Infra.Data.DAOs;
using System.Data;

namespace NS.SalesProduct.Infra.Data.Repositorys
{
    public class SaleItemDao : Dao, ISaleItemDao
    {
        public async Task<IReadOnlyCollection<SaleItem>> GetAllAsync()
        {
            var saleitems = new List<SaleItem>();
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"saleItem\"", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var saleItem = new SaleItem(
                            Guid.Parse(reader["Id"].ToString()),
                            Guid.Parse(reader["ProductId"].ToString()),
                            Guid.Parse(reader["SaleId"].ToString()),
                            int.Parse(reader["Amount"].ToString())
                            );

                        saleitems.Add(saleItem);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
                
            }
            return saleitems;
        }

        public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId)
        {
            var saleitems = new List<SaleItem>();
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM \"saleItem\" WHERE \"SaleId\" = '{saleId}' ", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var saleItem = new SaleItem(
                            Guid.Parse(reader["Id"].ToString()),
                            Guid.Parse(reader["ProductId"].ToString()),
                            Guid.Parse(reader["SaleId"].ToString()),
                            int.Parse(reader["Amount"].ToString())
                            );

                        saleitems.Add(saleItem);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }

            }
            return saleitems;
        }

        public async Task<SaleItem> GetByIdAsync(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM \"saleItem\" WHERE \"Id\" = '{id}'", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var saleItem = new SaleItem(
                           Guid.Parse(reader["Id"].ToString()),
                           Guid.Parse(reader["ProductId"].ToString()),
                           Guid.Parse(reader["SaleId"].ToString()),
                           int.Parse(reader["Amount"].ToString())
                           );
                        con.Close();
                        return saleItem;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
            return null;
        }

        public async Task RegisterItemsAsync(IReadOnlyCollection<SaleItem> items)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    try
                    {
                        var sqlCommand = $"INSERT INTO \"saleItem\" (\"Id\", \"SaleId\", \"Amount\") VALUES(@Id, @SaleId, @Amount)";
                        NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@SaleId", item.SaleId);
                        cmd.Parameters.AddWithValue("@Amount", item.Amount);
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
        }

        public async Task RegisterAsync(SaleItem entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"INSERT INTO \"saleItem\" (\"Id\", \"SaleId\", \"Amount\") VALUES(@Id, @SaleId, @Amount)";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@SaleId", entity.SaleId);
                    cmd.Parameters.AddWithValue("@Amount", entity.Amount);
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

        public async Task UpdateAsync(SaleItem entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"UPDATE \"saleItem\" SET \"SaleId\" = @SaleId, \"Amount\" = @Amount";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Name", entity.SaleId);
                    cmd.Parameters.AddWithValue("@Price", entity.Amount);
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

        public async Task DeleteAsync(SaleItem entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"DELETE from \"saleItem\" WHERE \"Id\" = '{entity.Id}'";
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

        public async Task DeleteBySaleIdAsync(Guid saleId)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var sqlCommand = $"DELETE from \"saleItem\" WHERE \"SaleId\" = '{saleId}'";
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
