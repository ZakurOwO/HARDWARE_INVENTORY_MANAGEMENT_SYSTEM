using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class VehiclesInfoBox: UserControl
    {
        public VehiclesInfoBox()
        {
            InitializeComponent();
        }

        #region Properties

        private Image image;
        private string vehicleType;
        private string vehicleName;
        private string plateNo;
        private String status;
        private string vehicleID;

        [Category("Custom Properties")]
        public Image Image
        {
            get { return image; }
            set { image = value; ptbVehiclePic.BackgroundImage = value; }
        }


        [Category("Custom Properties")]
        public string Type
        {
            get { return vehicleType; }
            set { vehicleType = value; lblVehicleType.Text = value; }
        }


        [Category("Custom Properties")]
        public string Vehicle_Name
        {
            get { return vehicleName; }
            set { vehicleName = value; VehicleBrandModel.Text = value; }
        }


        [Category("Custom Properties")]
        public string Plate_No
        {
            get { return plateNo; }
            set { plateNo = value; lblPlateNo.Text = "Plate No:" + value; }
        }


        [Category("Custom Properties")]
        public string Vehicle_ID
        {
            get { return vehicleID; }
            set { vehicleID = value; lblVehicleID.Text = "Vehicle ID: " + value; }
        }

        [Category("Custom Properties")]

        public String Status
        {
            get { return status; }
            set { status = value; btnStatus.Text = value; UpdateStatusColor(); }
        }


        #endregion

        private void UpdateStatusColor()
        {
            // Default
            btnStatus.FillColor = Color.LightGray;
            btnStatus.ForeColor = Color.Black;

            switch (status?.ToLower())
            {
                case "available":
                    btnStatus.FillColor = Color.FromArgb(219, 255, 232); // light green
                    btnStatus.ForeColor = Color.FromArgb(47, 164, 73);
                    break;
                case "in use":
                    btnStatus.FillColor = Color.FromArgb(225, 245, 255); // light blue
                    btnStatus.ForeColor = Color.FromArgb(0, 102, 204);
                    break;
                case "inactive":
                    btnStatus.FillColor = Color.FromArgb(255, 230, 230); // light red
                    btnStatus.ForeColor = Color.FromArgb(190, 38, 38);
                    break;
                case "maintenance":
                    btnStatus.FillColor = Color.FromArgb(255, 248, 208); // light orange
                    btnStatus.ForeColor = Color.FromArgb(209, 118, 17);
                    break;
            }

        }
    }
}
