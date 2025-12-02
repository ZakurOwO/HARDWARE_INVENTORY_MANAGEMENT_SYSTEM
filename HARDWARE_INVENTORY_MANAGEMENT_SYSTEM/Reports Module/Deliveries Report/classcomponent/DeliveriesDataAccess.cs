using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data
{
    public class DeliveriesDataAccess
    {
        private string connectionString;

        public DeliveriesDataAccess()
        {
            // Fixed: Use ConnectionString.DataSource instead of hardcoded string
            this.connectionString = ConnectionString.DataSource;
        }

        public DataTable GetDeliverySummary()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT
                        d.DeliveryID,
                        CONVERT(VARCHAR, d.delivery_date, 101) AS DeliveryDate,
                        CASE WHEN d.delivery_type = 'PO_Delivery' THEN COALESCE(s.supplier_name, 'Purchase Order Delivery')
                             ELSE COALESCE(c.customer_name, d.customer_name, 'Walk-in Customer') END AS Customer,
                        COALESCE(v.plate_number, 'Not Assigned') AS VehicleUsed,
                        (SELECT COUNT(*) FROM DeliveryItems di WHERE di.delivery_id = d.delivery_id) AS QuantityItems,
                        d.status AS Status
                    FROM Deliveries d
                    LEFT JOIN VehicleAssignments va ON d.delivery_id = va.delivery_id
                    LEFT JOIN Vehicles v ON va.vehicle_id = v.vehicle_id
                    LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                    LEFT JOIN Customers c ON t.customer_id = c.customer_id
                    LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                    LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                    ORDER BY d.delivery_date DESC";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetDeliverySummary: {ex.Message}");
                    throw new Exception($"Error retrieving delivery summary: {ex.Message}");
                }
            }
            return dt;
        }

        public DataTable GetDeliveryDetails()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT
                        d.DeliveryID,
                        d.delivery_number AS DeliveryNumber,
                        d.delivery_date AS DeliveryDate,
                        CASE WHEN d.delivery_type = 'PO_Delivery' THEN COALESCE(s.supplier_name, 'Purchase Order Delivery')
                             ELSE COALESCE(c.customer_name, d.customer_name, 'Walk-in Customer') END AS CustomerName,
                        d.delivery_address AS DeliveryAddress,
                        d.contact_number AS ContactNumber,
                        COALESCE(v.plate_number, 'Not Assigned') AS VehicleUsed,
                        COALESCE(va.driver_name, 'Not Assigned') AS DriverName,
                        d.status AS Status,
                        d.notes AS Notes
                    FROM Deliveries d
                    LEFT JOIN VehicleAssignments va ON d.delivery_id = va.delivery_id
                    LEFT JOIN Vehicles v ON va.vehicle_id = v.vehicle_id
                    LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                    LEFT JOIN Customers c ON t.customer_id = c.customer_id
                    LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                    LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                    ORDER BY d.delivery_date DESC";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetDeliveryDetails: {ex.Message}");
                    throw new Exception($"Error retrieving delivery details: {ex.Message}");
                }
            }
            return dt;
        }

        public int GetTotalDeliveries()
        {
            int total = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Deliveries";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        total = result != null ? Convert.ToInt32(result) : 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetTotalDeliveries: {ex.Message}");
                }
            }
            return total;
        }

        public int GetPendingDeliveries()
        {
            int pending = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(*)
                    FROM Deliveries
                    WHERE status IN ('Scheduled', 'In Transit', 'Pending')";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        pending = result != null ? Convert.ToInt32(result) : 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetPendingDeliveries: {ex.Message}");
                }
            }
            return pending;
        }

        public int GetTotalVehicles()
        {
            int total = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Vehicles WHERE status != 'Inactive'";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        total = result != null ? Convert.ToInt32(result) : 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetTotalVehicles: {ex.Message}");
                }
            }
            return total;
        }

        public int GetActiveVehicles()
        {
            int active = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Vehicles 
                    WHERE status IN ('Available', 'On Delivery')";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        active = result != null ? Convert.ToInt32(result) : 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetActiveVehicles: {ex.Message}");
                }
            }
            return active;
        }

        public DataTable GetVehicleUtilization()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        v.VehicleID AS VehicleID,
                        v.plate_number + ' - ' + COALESCE(v.brand, '') + ' ' + COALESCE(v.model, '') AS VehicleNamePlateNo,
                        COALESCE(v.vehicle_type, 'N/A') AS Type,
                        COUNT(va.assignment_id) AS TotalDeliveries,
                        v.status AS Status
                    FROM Vehicles v
                    LEFT JOIN VehicleAssignments va ON v.vehicle_id = va.vehicle_id
                    WHERE v.status != 'Inactive'
                    GROUP BY v.VehicleID, v.plate_number, v.brand, v.model, v.vehicle_type, v.status
                    ORDER BY TotalDeliveries DESC";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetVehicleUtilization: {ex.Message}");
                    throw new Exception($"Error retrieving vehicle utilization: {ex.Message}");
                }
            }
            return dt;
        }

        // Filter by date range
        public DataTable GetDeliveriesByDateRange(DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT
                        d.DeliveryID,
                        CONVERT(VARCHAR, d.delivery_date, 101) AS DeliveryDate,
                        CASE WHEN d.delivery_type = 'PO_Delivery' THEN COALESCE(s.supplier_name, 'Purchase Order Delivery')
                             ELSE COALESCE(c.customer_name, d.customer_name, 'Walk-in Customer') END AS Customer,
                        COALESCE(v.plate_number, 'Not Assigned') AS VehicleUsed,
                        (SELECT COUNT(*) FROM DeliveryItems di WHERE di.delivery_id = d.delivery_id) AS QuantityItems,
                        d.status AS Status
                    FROM Deliveries d
                    LEFT JOIN VehicleAssignments va ON d.delivery_id = va.delivery_id
                    LEFT JOIN Vehicles v ON va.vehicle_id = v.vehicle_id
                    LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                    LEFT JOIN Customers c ON t.customer_id = c.customer_id
                    LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                    LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                    WHERE d.delivery_date >= @StartDate
                    AND d.delivery_date <= @EndDate
                    ORDER BY d.delivery_date DESC";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetDeliveriesByDateRange: {ex.Message}");
                    throw new Exception($"Error retrieving deliveries by date range: {ex.Message}");
                }
            }
            return dt;
        }

        // Get deliveries by status
        public DataTable GetDeliveriesByStatus(string status)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT
                        d.DeliveryID,
                        CONVERT(VARCHAR, d.delivery_date, 101) AS DeliveryDate,
                        CASE WHEN d.delivery_type = 'PO_Delivery' THEN COALESCE(s.supplier_name, 'Purchase Order Delivery')
                             ELSE COALESCE(c.customer_name, d.customer_name, 'Walk-in Customer') END AS Customer,
                        COALESCE(v.plate_number, 'Not Assigned') AS VehicleUsed,
                        (SELECT COUNT(*) FROM DeliveryItems di WHERE di.delivery_id = d.delivery_id) AS QuantityItems,
                        d.status AS Status
                    FROM Deliveries d
                    LEFT JOIN VehicleAssignments va ON d.delivery_id = va.delivery_id
                    LEFT JOIN Vehicles v ON va.vehicle_id = v.vehicle_id
                    LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                    LEFT JOIN Customers c ON t.customer_id = c.customer_id
                    LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                    LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                    WHERE d.status = @Status
                    ORDER BY d.delivery_date DESC";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetDeliveriesByStatus: {ex.Message}");
                    throw new Exception($"Error retrieving deliveries by status: {ex.Message}");
                }
            }
            return dt;
        }
    }
}