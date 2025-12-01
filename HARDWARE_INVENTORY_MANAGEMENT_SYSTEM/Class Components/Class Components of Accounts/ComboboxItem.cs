namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    /// <summary>
    /// Custom ComboBox item class for storing text-value pairs
    /// </summary>
    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public ComboboxItem()
        {
        }

        public ComboboxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            if (obj is ComboboxItem other)
            {
                return Value == other.Value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }
    }
}