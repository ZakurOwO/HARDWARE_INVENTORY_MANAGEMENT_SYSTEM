using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class DeliveryComboBoxes : UserControl
    {
        public DeliveryComboBoxes()
        {
            InitializeComponent();
            
        }

        private void DeliveryComboBoxes_Load(object sender, EventArgs e)
        {
            cbxChooseCustomer.ForeColor = Color.Gray;
            cbxChooseCustomer.Text = "Choose Customer";
        }

          }
}
