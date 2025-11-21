using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class CustomerMainPage: UserControl
    {
        public CustomerMainPage()
        {
            InitializeComponent();
        }
        private AddCustomerContainer addCustomerContainer = new AddCustomerContainer();

        private void customerTopBar1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridTable1_Load(object sender, EventArgs e)
        {

        }

        private void btnMainButtonIcon_Click(object sender, EventArgs e)
        {
            var main = this.FindForm() as MainDashBoard;

            if (main != null)
            {
                addCustomerContainer.ShowAddCustomerForm(main);
            }
        }
    }
}
