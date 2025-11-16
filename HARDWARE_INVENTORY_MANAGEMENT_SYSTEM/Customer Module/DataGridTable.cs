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
            //dgvCustomer.Rows.Add("TechWorld Corp", "Danielle Rivera", "danielle@techworld.com", "09171234567", Action_Set("EditBtn"),Action_Set1("DeactivateBtn"));
        }



        private Image Action_Set(string EditColBtn)
        {
            
                  return Properties.Resources.edit_rectangle1;
              

        }

        private Image Action_Set1(string DeActiColBtn)
        {

            return Properties.Resources.do_not_disturb;

        }


    }
}
