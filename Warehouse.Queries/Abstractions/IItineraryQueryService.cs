using Warehouse.Queries.Models;
using System.Threading.Tasks;

namespace Warehouse.Queries.Abstractions
{
    public interface IItineraryQueryService
    {
        Task<bool> StorageCellExistsAsync(string storageCellId);
        Task<CellInfo> SearchCellItineraryInfo(string storageCellId);
    }
}