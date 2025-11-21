using System.Data.SqlClient;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    internal class ConnectionString
    {
        public static readonly string DataSource = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";
    }
}