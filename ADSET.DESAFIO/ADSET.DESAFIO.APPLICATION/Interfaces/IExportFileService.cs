using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSET.DESAFIO.APPLICATION.Interfaces
{
    public interface IExportFileService
    {
        Task<byte[]> ExportToExcelAsync();
    }
}