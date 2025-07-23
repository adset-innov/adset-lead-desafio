namespace ADSET.DESAFIO.APPLICATION.Interfaces
{
    public interface IExportFileService
    {
        Task<byte[]> ExportToExcelAsync();
    }
}