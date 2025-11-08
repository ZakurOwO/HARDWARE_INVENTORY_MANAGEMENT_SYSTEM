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
                case "Available":
                    return Properties.Resources.AvailableStatus;
                case "Pending":
                    return Properties.Resources.Pending;
                case "Canceled":
                    return Properties.Resources.Canceled;
                default:
                    return Properties.Resources.AvailableStatus;
            }
          
        }


        private void dgvHistory_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DatGridTableHistory_Load(object sender, EventArgs e)
        {
            dgvHistory.Rows.Add("001", "Laptop", "Dell XPS 13", "John Doe", "2023-09-01", "2023-09-10", status_set("Pending"));
        }
    }
}
