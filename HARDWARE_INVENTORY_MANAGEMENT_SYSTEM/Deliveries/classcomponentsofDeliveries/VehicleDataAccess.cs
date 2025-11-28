using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public class VehicleDataAccess
    {
        private string connectionString;

        public VehicleDataAccess()
        {
            connectionString = ConnectionString.DataSource;
        }

        // Test database connection
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection failed: {ex.Message}", "Connection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Get all active vehicles
        public List<VehicleRecord> GetActiveVehicles()
        {
            var vehicles = new List<VehicleRecord>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                vehicle_id,
                VehicleID,
                plate_number,
                brand,
                model,
                vehicle_type,
                capacity,
                status,
                image_path,
                created_at,
                updated_at
            FROM Vehicles 
            WHERE status != 'Inactive'
            ORDER BY created_at DESC";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        vehicles.Add(new VehicleRecord
                        {
                            VehicleInternalID = reader.GetInt32(0),
                            VehicleID = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            PlateNumber = reader.GetString(2),
                            Brand = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Model = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            VehicleType = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Capacity = reader.IsDBNull(6) ? "" : reader.GetString(6),
                            Status = reader.IsDBNull(7) ? "Available" : reader.GetString(7),
                            ImagePath = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            CreatedAt = reader.GetDateTime(9),
                            UpdatedAt = reader.GetDateTime(10)
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log the error but return empty list instead of throwing
                    Console.WriteLine($"Error in GetActiveVehicles: {ex.Message}");
                    return new List<VehicleRecord>(); // Return empty list instead of null
                }
            }

            return vehicles;
        }

        // Get all vehicles for DataTable
        public DataTable GetAllVehiclesDataTable()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        vehicle_id as VehicleInternalID,
                        VehicleID,
                        plate_number as PlateNumber,
                        brand as Brand,
                        model as Model,
                        vehicle_type as VehicleType,
                        capacity as Capacity,
                        status as Status,
                        image_path as ImagePath
                    FROM Vehicles 
                    WHERE status != 'Inactive'
                    ORDER BY created_at DESC";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving vehicles data table: {ex.Message}");
                }
            }

            return dataTable;
        }

        // Get vehicles with pagination support
        public DataTable GetVehiclesPaginated(int pageNumber, int pageSize, out int totalRecords)
        {
            DataTable dataTable = new DataTable();
            totalRecords = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    -- Get total count
                    SELECT COUNT(*) FROM Vehicles WHERE status != 'Inactive';
                    
                    -- Get paginated data
                    SELECT 
                        vehicle_id as VehicleInternalID,
                        VehicleID,
                        plate_number as PlateNumber,
                        brand as Brand,
                        model as Model,
                        vehicle_type as VehicleType,
                        capacity as Capacity,
                        status as Status,
                        image_path as ImagePath,
                        created_at as CreatedAt,
                        updated_at as UpdatedAt
                    FROM Vehicles 
                    WHERE status != 'Inactive'
                    ORDER BY created_at DESC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // First result set: total count
                        if (reader.Read())
                        {
                            totalRecords = reader.GetInt32(0);
                        }

                        // Second result set: paginated data
                        if (reader.NextResult())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving paginated vehicles: {ex.Message}");
                }
            }

            return dataTable;
        }

        // Get total vehicle count
        public int GetTotalVehicleCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Vehicles WHERE status != 'Inactive'";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error getting vehicle count: {ex.Message}");
                }
            }
        }

        // Add new vehicle
        public bool AddVehicle(VehicleRecord vehicle)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Vehicles 
                    (plate_number, brand, model, vehicle_type, capacity, status, image_path)
                    VALUES 
                    (@PlateNumber, @Brand, @Model, @VehicleType, @Capacity, @Status, @ImagePath)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
                command.Parameters.AddWithValue("@Brand", vehicle.Brand ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Model", vehicle.Model ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Capacity", vehicle.Capacity ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Status", vehicle.Status ?? "Available");
                command.Parameters.AddWithValue("@ImagePath", vehicle.ImagePath ?? (object)DBNull.Value);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
                    {
                        throw new Exception("Plate number already exists. Please use a different plate number.");
                    }
                    throw new Exception($"Error adding vehicle: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error adding vehicle: {ex.Message}");
                }
            }
        }

        // Update existing vehicle
        public bool UpdateVehicle(VehicleRecord vehicle)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Vehicles 
                    SET plate_number = @PlateNumber,
                        brand = @Brand,
                        model = @Model,
                        vehicle_type = @VehicleType,
                        capacity = @Capacity,
                        status = @Status,
                        image_path = @ImagePath,
                        updated_at = GETDATE()
                    WHERE vehicle_id = @VehicleInternalID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VehicleInternalID", vehicle.VehicleInternalID);
                command.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
                command.Parameters.AddWithValue("@Brand", vehicle.Brand ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Model", vehicle.Model ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Capacity", vehicle.Capacity ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Status", vehicle.Status ?? "Available");
                command.Parameters.AddWithValue("@ImagePath", vehicle.ImagePath ?? (object)DBNull.Value);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        throw new Exception("Plate number already exists. Please use a different plate number.");
                    }
                    throw new Exception($"Error updating vehicle: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating vehicle: {ex.Message}");
                }
            }
        }

        // Delete vehicle (soft delete by setting status to Inactive)
        public bool DeleteVehicle(int vehicleInternalId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Vehicles 
                    SET status = 'Inactive',
                        updated_at = GETDATE()
                    WHERE vehicle_id = @VehicleInternalID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VehicleInternalID", vehicleInternalId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deleting vehicle: {ex.Message}");
                }
            }
        }

        // Get vehicle by ID
        public VehicleRecord GetVehicleById(int vehicleInternalId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        vehicle_id,
                        VehicleID,
                        plate_number,
                        brand,
                        model,
                        vehicle_type,
                        capacity,
                        status,
                        image_path,
                        created_at,
                        updated_at
                    FROM Vehicles 
                    WHERE vehicle_id = @VehicleInternalID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VehicleInternalID", vehicleInternalId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new VehicleRecord
                        {
                            VehicleInternalID = reader.GetInt32(0),
                            VehicleID = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            PlateNumber = reader.GetString(2),
                            Brand = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Model = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            VehicleType = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Capacity = reader.IsDBNull(6) ? "" : reader.GetString(6),
                            Status = reader.IsDBNull(7) ? "Available" : reader.GetString(7),
                            ImagePath = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            CreatedAt = reader.GetDateTime(9),
                            UpdatedAt = reader.GetDateTime(10)
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving vehicle: {ex.Message}");
                }
            }

            return null;
        }

        // Check if plate number already exists
        public bool PlateNumberExists(string plateNumber, int excludeVehicleId = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Vehicles 
                    WHERE plate_number = @PlateNumber 
                    AND vehicle_id != @ExcludeVehicleId
                    AND status != 'Inactive'";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlateNumber", plateNumber);
                command.Parameters.AddWithValue("@ExcludeVehicleId", excludeVehicleId);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking plate number: {ex.Message}");
                }
            }
        }

        // Search vehicles
        public List<VehicleRecord> SearchVehicles(string searchTerm)
        {
            var vehicles = new List<VehicleRecord>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        vehicle_id,
                        VehicleID,
                        plate_number,
                        brand,
                        model,
                        vehicle_type,
                        capacity,
                        status,
                        image_path,
                        created_at,
                        updated_at
                    FROM Vehicles 
                    WHERE (plate_number LIKE @SearchTerm 
                       OR brand LIKE @SearchTerm 
                       OR model LIKE @SearchTerm 
                       OR vehicle_type LIKE @SearchTerm
                       OR VehicleID LIKE @SearchTerm)
                       AND status != 'Inactive'
                    ORDER BY created_at DESC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        vehicles.Add(new VehicleRecord
                        {
                            VehicleInternalID = reader.GetInt32(0),
                            VehicleID = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            PlateNumber = reader.GetString(2),
                            Brand = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Model = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            VehicleType = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Capacity = reader.IsDBNull(6) ? "" : reader.GetString(6),
                            Status = reader.IsDBNull(7) ? "Available" : reader.GetString(7),
                            ImagePath = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            CreatedAt = reader.GetDateTime(9),
                            UpdatedAt = reader.GetDateTime(10)
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error searching vehicles: {ex.Message}");
                }
            }

            return vehicles;
        }

        // Get vehicles by status
        public List<VehicleRecord> GetVehiclesByStatus(string status)
        {
            var vehicles = new List<VehicleRecord>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        vehicle_id,
                        VehicleID,
                        plate_number,
                        brand,
                        model,
                        vehicle_type,
                        capacity,
                        status,
                        image_path,
                        created_at,
                        updated_at
                    FROM Vehicles 
                    WHERE status = @Status 
                    ORDER BY brand, model";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        vehicles.Add(new VehicleRecord
                        {
                            VehicleInternalID = reader.GetInt32(0),
                            VehicleID = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            PlateNumber = reader.GetString(2),
                            Brand = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Model = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            VehicleType = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Capacity = reader.IsDBNull(6) ? "" : reader.GetString(6),
                            Status = reader.IsDBNull(7) ? "Available" : reader.GetString(7),
                            ImagePath = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            CreatedAt = reader.GetDateTime(9),
                            UpdatedAt = reader.GetDateTime(10)
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving vehicles by status: {ex.Message}");
                }
            }

            return vehicles;
        }
    }
}