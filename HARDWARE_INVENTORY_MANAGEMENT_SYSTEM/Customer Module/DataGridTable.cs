using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class DataGridTable : UserControl
    {
        public DataGridTable()
        {
            InitializeComponent();
        }

        private void DataGridTable_Load(object sender, EventArgs e)
        {
            dgvCustomer.Rows.Add(Action_Set("menu_circle_vertical"));
        }



        private Image Action_Set(string Action)
        {
            
                  return Properties.Resources.menu_circle_vertical;
              

        }



        

      

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }
    }
}
