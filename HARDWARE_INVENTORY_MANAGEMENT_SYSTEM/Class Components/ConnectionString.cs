using System.Data.SqlClient;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    internal class ConnectionString
    {
        // Primary local DB connection string
        //public static readonly string DataSource = @"Data Source=JCELGFRANCISCO\\SQLEXPRESS;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";

        // Alternative local DB connection strings
        // public static readonly string DataSource = @"Data Source=ACHILLES\\SQLEXPRESS;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";
         public static readonly string DataSource = @"Data Source=localhost;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";
    }
}
