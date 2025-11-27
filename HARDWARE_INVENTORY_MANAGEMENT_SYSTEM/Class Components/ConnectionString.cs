using System.Data.SqlClient;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    internal class ConnectionString
    {
        // public static readonly string DataSource = @"Data Source=ACHILLES\SQLEXPRESS;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";

        //Janel's Local DB Connection String
        public static readonly string DataSource = @"Data Source=JCELGFRANCISCO\SQLEXPRESS;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";


        //Danielle's Local DB Connection String
        //public static readonly string DataSource = @"Data Source=localhost;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";

        //karl's Local DB Connection String
        //public static readonly string DataSource = @"Data Source=ACHILLES\SQLEXPRESS;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";

    }
}