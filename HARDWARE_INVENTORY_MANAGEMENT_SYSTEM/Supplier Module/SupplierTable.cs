using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class SupplierTable: UserControl
    {
        public SupplierTable()
        {
            InitializeComponent();
        }

        private Image Action_Set(string EditColBtn)
        {
            switch (EditColBtn)
            {
                case "EditBtn":
                default:
                    return Properties.Resources.Edit_Blue;
            }
           


        }

        private Image Action_Set1(string DeActiColBtn)
        {
            switch (DeActiColBtn)
            {
                case "DeactivateBtn":
                default:
                    return Properties.Resources.Deactivate_Circle;
            }



        }

        private Image status_set(string status)
        {
            switch (status)
            {
                case "Active":
                    return Properties.Resources.Active1;
                case "Inactive":
                    return Properties.Resources.Inactive2;
                default:
                    return Properties.Resources.ActiveStatus;
            }

        }

        private void SupplierTable_Load(object sender, EventArgs e)
        {

            dgvSupplier.Rows.Add(status_set("Active"),"DPWH","dpwhgmail.com","Quezon City, Manila", "2023-05-21", Action_Set("EditBtn"), Action_Set1("DeactivateBtn"));
            dgvSupplier.Rows.Add(status_set("Active"), "Department of Agriculture", "doagmail.com", "Makati City, Manila", "2023-05-21", Action_Set("EditBtn"), Action_Set1("DeactivateBtn"));
            dgvSupplier.Rows.Add(status_set("Active"), "Department of Education", "doedgmail.com", "Pasig City, Manila", "2023-05-21", Action_Set("EditBtn"), Action_Set1("DeactivateBtn"));
            dgvSupplier.Rows.Add(status_set("Active"), "Department of Health", "dohgmail.com", "Taguig City, Manila", "2023-05-21", Action_Set("EditBtn"), Action_Set1("DeactivateBtn"));
            dgvSupplier.Rows.Add(status_set("Active"), "Department of Interior and Local Government", "dilgmail.com", "Caloocan City, Manila", "2023-05-21", Action_Set("EditBtn"), Action_Set1("DeactivateBtn"));
            dgvSupplier.Rows.Add(status_set("Inactive"), "Department of Transportation", "dotrgmail.com", "Pasay City, Manila", "2023-05-21", Action_Set("EditBtn"), Action_Set1("DeactivateBtn"));
            dgvSupplier.ClearSelection();
        }
        
    }
}
