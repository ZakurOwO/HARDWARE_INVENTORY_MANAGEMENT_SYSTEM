using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public class VehicleRecord
    {
        public int VehicleInternalID { get; set; }
        public int VehicleId
        {
            get { return VehicleInternalID; }
            set { VehicleInternalID = value; }
        }
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

        public VehicleRecord()
        {
            Status = "Available";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

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