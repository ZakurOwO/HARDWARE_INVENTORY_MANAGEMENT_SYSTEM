using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public class VehicleRecord
    {
        // Primary key - INTEGER
        public int VehicleInternalID { get; set; }

        // For backward compatibility and database operations
        public int VehicleId
        {
            get { return VehicleInternalID; }
            set { VehicleInternalID = value; }
        }

        // Computed column - STRING (VEH-00001 format)
        public string VehicleID { get; set; }

        public string PlateNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VehicleType { get; set; }
        public string Capacity { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Constructor
        public VehicleRecord()
        {
            Status = "Available";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        // Display format for UI
        public string DisplayName
        {
            get { return $"{Brand} {Model} - {PlateNumber}"; }
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}