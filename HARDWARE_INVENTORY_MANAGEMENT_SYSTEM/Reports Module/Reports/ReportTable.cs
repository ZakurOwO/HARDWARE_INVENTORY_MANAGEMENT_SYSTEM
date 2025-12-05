using System.Collections.Generic;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public class ReportTable
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public List<string> Headers { get; set; }
        public List<List<string>> Rows { get; set; }

        public ReportTable()
        {
            Headers = new List<string>();
            Rows = new List<List<string>>();
        }
    }
}
