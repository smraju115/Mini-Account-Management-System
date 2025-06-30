using Microsoft.Data.SqlClient;
using MiniAccountManagementSystem.Models;
using System.Data;

namespace MiniAccountManagementSystem.DataAccrss
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<ChartOfAccountModel> GetChartOfAccounts()
        {
            var list = new List<ChartOfAccountModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GET");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new ChartOfAccountModel
                    {
                        AccountID = Convert.ToInt32(reader["AccountID"]),
                        AccountName = reader["AccountName"].ToString(),
                        ParentAccountID = reader["ParentAccountID"] as int?,
                        AccountType = reader["AccountType"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                    });
                }
            }

            return list;
        }

        public void AddChartAccount(ChartOfAccountModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@AccountName", model.AccountName);
                cmd.Parameters.AddWithValue("@ParentAccountID", model.ParentAccountID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AccountType", model.AccountType);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public ChartOfAccountModel GetById(int id)
        {
            ChartOfAccountModel account = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@AccountID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    account = new ChartOfAccountModel
                    {
                        AccountID = Convert.ToInt32(reader["AccountID"]),
                        AccountName = reader["AccountName"].ToString(),
                        ParentAccountID = reader["ParentAccountID"] as int?,
                        AccountType = reader["AccountType"].ToString(),

                    };
                }
            }

            return account;

        }


        public void UpdateChartAccount(ChartOfAccountModel model)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@AccountID", model.AccountID);
                cmd.Parameters.AddWithValue("@AccountName", model.AccountName);
                cmd.Parameters.AddWithValue("@ParentAccountID", model.ParentAccountID ??(object)DBNull.Value );
                cmd.Parameters.AddWithValue("@AccountType", model.AccountType);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }


        public void DeleteChartAccount(int id)
        {
            using(SqlConnection conn =new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@AccountID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
































    }
}
