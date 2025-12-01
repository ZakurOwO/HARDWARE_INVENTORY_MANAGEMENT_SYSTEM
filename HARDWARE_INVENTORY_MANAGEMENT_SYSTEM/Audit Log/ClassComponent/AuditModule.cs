namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    /// <summary>
    /// Centralized module name strings used by the audit logger.
    /// Using constants keeps callers from accidentally passing the static class itself,
    /// which triggers the “static types cannot be used as parameters” compiler error.
    /// </summary>
    public static class AuditModule
    {
        public const string AUTHENTICATION = "Authentication";
        public const string LOGIN = "Login";
        public const string SIGN_OUT = "Sign Out";
        public const string PRODUCTS = "Products";
        public const string INVENTORY = "Inventory";
        public const string SALES = "Sales";
        public const string PURCHASE_ORDERS = "Purchase Orders";
        public const string DELIVERIES = "Deliveries";
        public const string SUPPLIERS = "Suppliers";
        public const string CUSTOMERS = "Customers";
        public const string VEHICLES = "Vehicles";
        public const string ACCOUNTS = "Accounts";
        public const string REPORTS = "Reports";
        public const string SETTINGS = "Settings";
        public const string AUDIT_LOG = "Audit Log";
    }
}
