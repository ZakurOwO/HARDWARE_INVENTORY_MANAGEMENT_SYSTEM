namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public interface IReportExportable
    {
        ReportTable BuildReportForExport();
    }
}
