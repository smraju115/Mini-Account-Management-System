using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniAccountManagementSystem.Services
{
    public class PageAccessService
    {
        private readonly IConfiguration _config;

        public PageAccessService(IConfiguration config)
        {
            _config = config;
        }

        public bool HasAccess(string roleName, string pageName)
        {
            using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                using (var cmd = new SqlCommand("sp_GetPageAccess", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleName", roleName);
                    cmd.Parameters.AddWithValue("@PageName", pageName);

                    conn.Open();
                    int result = (int)cmd.ExecuteScalar();
                    return result > 0;
                }
            }
        }
    }
}
