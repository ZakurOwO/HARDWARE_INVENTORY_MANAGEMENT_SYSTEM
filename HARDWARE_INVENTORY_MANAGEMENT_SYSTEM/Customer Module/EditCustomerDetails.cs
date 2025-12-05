using System;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    /// <summary>
    /// Simple container for customer details shared by the add and edit workflows.
    /// The UI surface remains unchanged; this class only provides strongly typed
    /// accessors and helper methods used by the forms and containers.
    /// </summary>
    public class CustomerDetailsModel
    {
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Status { get; set; }

        public string BuildFullAddress()
        {
            string full = AddressLine?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(City))
            {
                full = string.IsNullOrWhiteSpace(full) ? City : $"{full}, {City}";
            }
            if (!string.IsNullOrWhiteSpace(Province))
            {
                full = string.IsNullOrWhiteSpace(full) ? Province : $"{full}, {Province}";
            }
            return full;
        }
    }

    public partial class EditCustomerDetails : UserControl
    {
        public EditCustomerDetails()
        {
            InitializeComponent();
        }
    }
}
