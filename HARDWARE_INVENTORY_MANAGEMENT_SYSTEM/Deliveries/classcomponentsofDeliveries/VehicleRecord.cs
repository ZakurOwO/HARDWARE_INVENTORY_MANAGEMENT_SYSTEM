using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public class VehicleRecord
    {
        public int VehicleInternalID { get; set; }
        public string VehicleID { get; set; }
        public string PlateNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VehicleType { get; set; }
        public string Capacity { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Remarks { get; set; } // Added for the form

        // Additional properties for your UI
        public string VehicleName => $"{Brand} {Model}"; // Combined name
    }
}