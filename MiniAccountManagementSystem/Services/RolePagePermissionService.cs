//using Microsoft.Data.SqlClient;

//namespace MiniAccountManagementSystem.Services
//{
//    public class RolePagePermissionService
//    {
//        private readonly IConfiguration _config;

//        public RolePagePermissionService(IConfiguration config)
//        {
//            _config = config;
//        }

//        public List<string> GetAllPages()
//        {
//            var pages = new List<string>();

//            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
//            using var cmd = new SqlCommand("SELECT PageName FROM PageList ORDER BY PageName", conn);
//            conn.Open();

//            using var reader = cmd.ExecuteReader();
//            while (reader.Read())
//            {
//                pages.Add(reader.GetString(0));
//            }

//            return pages;
//        }

//        public List<string> GetPagesByRole(string role)
//        {
//            var pages = new List<string>();

//            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
//            using var cmd = new SqlCommand(@"SELECT pl.PageName 
//                    FROM RolePagePermission rp 
//                    INNER JOIN PageList pl ON rp.PageId = pl.PageId 
//                    WHERE rp.RoleName = @RoleName", conn);

//            cmd.Parameters.AddWithValue("@RoleName", role);
//            conn.Open();

//            using var reader = cmd.ExecuteReader();
//            while (reader.Read())
//            {
//                pages.Add(reader.GetString(0));
//            }

//            return pages;
//        }

//        public void UpdateRolePermissions(string role, List<string> pages)
//        {
//            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
//            conn.Open();

//            var tran = conn.BeginTransaction();

//            try
//            {
//                // Delete old permissions
//                var delCmd = new SqlCommand("DELETE FROM RolePagePermission WHERE RoleName = @Role", conn, tran);
//                delCmd.Parameters.AddWithValue("@Role", role);
//                delCmd.ExecuteNonQuery();

//                // Insert new permissions
//                foreach (var page in pages)
//                {
//                    var insertCmd = new SqlCommand(@"INSERT INTO RolePagePermission (RoleName, PageId)
//                                                     SELECT @RoleName, PageId FROM PageList WHERE PageName = @PageName", conn, tran);

//                    insertCmd.Parameters.AddWithValue("@RoleName", role);
//                    insertCmd.Parameters.AddWithValue("@PageName", page);
//                    insertCmd.ExecuteNonQuery();
//                }

//                tran.Commit();
//            }
//            catch
//            {
//                tran.Rollback();
//                throw;
//            }
//        }
//    }
//}
