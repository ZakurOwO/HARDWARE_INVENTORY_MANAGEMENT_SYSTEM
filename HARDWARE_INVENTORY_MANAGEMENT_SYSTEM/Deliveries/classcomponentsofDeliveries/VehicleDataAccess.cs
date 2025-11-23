using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    throw new Exception($"Error retrieving vehicles: {ex.Message}");
                }
            }

            return vehicles;
        }

        // Search vehicles by plate number, brand, model, or type
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
                    WHERE plate_number LIKE @SearchTerm 
                       OR brand LIKE @SearchTerm 
                       OR model LIKE @SearchTerm 
                       OR vehicle_type LIKE @SearchTerm
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
                    ORDER BY created_at DESC";

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

        // Add new vehicle
        public void AddVehicle(VehicleRecord vehicle)
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
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error adding vehicle: {ex.Message}");
                }
            }
        }

        // Update existing vehicle
        public void UpdateVehicle(VehicleRecord vehicle)
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
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating vehicle: {ex.Message}");
                }
            }
        }

        // Delete vehicle (soft delete by setting status to Inactive)
        public void DeleteVehicle(int vehicleInternalId)
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
                    command.ExecuteNonQuery();
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
    }
}