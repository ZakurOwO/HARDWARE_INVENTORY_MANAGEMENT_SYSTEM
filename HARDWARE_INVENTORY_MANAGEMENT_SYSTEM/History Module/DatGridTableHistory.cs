using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module
{
    public partial class DatGridTableHistory : UserControl
    {
        public DatGridTableHistory()
        {
            InitializeComponent();



        }

      
        private Image status_set(string status)
        {
            switch (status)
            {
                case "Completed":
                    return Properties.Resources.Completed;
                case "Pending":
                    return Properties.Resources.Pending1;
                case "Canceled":
                    return Properties.Resources.Canceled1;
                default:
                    return Properties.Resources.Completed;
            }
          
        }


        private void dgvHistory_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DatGridTableHistory_Load(object sender, EventArgs e)
        {
            dgvHistory.Rows.Add("HTY-001","Woods","2025-01-15","John Doe", "Plywood 1/2mm", 20 ,status_set("Completed"));
            dgvHistory.Rows.Add("HTY-002", "Stone", "2025-02-20", "Jane Smith", "Granite Slab", 10, status_set("Pending"));
            dgvHistory.Rows.Add("HTY-003", "Metal", "2025-03-10", "Mike Johnson", "Steel Beams", 15, status_set("Canceled"));
            dgvHistory.Rows.Add("HTY-004", "Glass", "2025-04-05", "Emily Davis", "Tempered Glass Sheets", 25, status_set("Completed"));
            dgvHistory.Rows.Add("HTY-005", "Concrete", "2025-05-12", "David Wilson", "Precast Concrete Blocks", 30, status_set("Pending"));

            dgvHistory.ClearSelection();
        }
    }
}
