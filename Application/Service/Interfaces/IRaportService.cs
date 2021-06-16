using System;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface IRaportService
    {
        Task<byte[]> GetDataToExcelDocument(DateTime startDate, DateTime endDate, string name);
    }
}
