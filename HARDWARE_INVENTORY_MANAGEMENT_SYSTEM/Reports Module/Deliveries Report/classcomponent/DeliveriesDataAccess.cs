using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data
{
    public class DeliveriesDataAccess
    {
        private string connectionString;

        public DeliveriesDataAccess()
        {
            this.connectionString = "Your_Connection_String_Here";
        }

        public DataTable GetDeliverySummary()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        d.DeliveryID,
                        d.delivery_date AS DeliveryDate,
                        COALESCE(c.customer_name, d.customer_name) AS Customer,
                        v.plate_number AS VehicleUsed,
                        (SELECT COUNT(*) FROM DeliveryItems di WHERE di.delivery_id = d.delivery_id) AS QuantityItems,
                        d.status AS Status
                    FROM Deliveries d
                    LEFT JOIN VehicleAssignments va ON d.delivery_id = va.delivery_id
                    LEFT JOIN Vehicles v ON va.vehicle_id = v.vehicle_id
                    LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                    LEFT JOIN Customers c ON t.customer_id = c.customer_id
                    ORDER BY d.delivery_date DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetDeliveryDetails()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        d.DeliveryID,
                        d.delivery_number AS DeliveryNumber,
                        d.delivery_date AS DeliveryDate,
                        COALESCE(c.customer_name, d.customer_name) AS CustomerName,
                        d.delivery_address AS DeliveryAddress,
                        d.contact_number AS ContactNumber,
                        v.plate_number AS VehicleUsed,
                        va.driver_name AS DriverName,
                        d.status AS Status,
                        d.notes AS Notes
                    FROM Deliveries d
                    LEFT JOIN VehicleAssignments va ON d.delivery_id = va.delivery_id
                    LEFT JOIN Vehicles v ON va.vehicle_id = v.vehicle_id
                    LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                    LEFT JOIN Customers c ON t.customer_id = c.customer_id
                    ORDER BY d.delivery_date DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public int GetTotalDeliveries()
        {
            int total = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Deliveries";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    total = (int)cmd.ExecuteScalar();
                }
            }
            return total;
        }

        public int GetPendingDeliveries()
        {
            int pending = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Deliveries WHERE status IN ('Scheduled', 'In Transit')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    pending = (int)cmd.ExecuteScalar();
                }
            }
            return pending;
        }

        public int GetTotalVehicles()
        {
            int total = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Vehicles";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    total = (int)cmd.ExecuteScalar();
                }
            }
            return total;
        }

        public int GetActiveVehicles()
        {
            int active = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Vehicles WHERE status IN ('Available', 'On Delivery')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    active = (int)cmd.ExecuteScalar();
                }
            }
            return active;
        }

        public DataTable GetVehicleUtilization()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        v.plate_number AS PlateNumber,
                        v.brand AS Brand,
                        v.model AS Model,
                        v.vehicle_type AS VehicleType,
                        v.status AS Status,
                        COUNT(va.assignment_id) AS TotalAssignments
                    FROM Vehicles v
                    LEFT JOIN VehicleAssignments va ON v.vehicle_id = va.vehicle_id
                    GROUP BY v.plate_number, v.brand, v.model, v.vehicle_type, v.status
                    ORDER BY TotalAssignments DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}