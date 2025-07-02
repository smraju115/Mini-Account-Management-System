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
            if (string.IsNullOrEmpty(roleName) || string.IsNullOrEmpty(pageName))
                return false;

            using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                using (var cmd = new SqlCommand("sp_GetPageAccess", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleName", roleName);
                    cmd.Parameters.AddWithValue("@PageName", pageName);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }
    }
}
