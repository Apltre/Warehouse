using Microsoft.EntityFrameworkCore;
using Warehouse.LegacyData;
using Warehouse.LegacyData.Enums;
using Warehouse.Queries.Abstractions;
using Warehouse.Queries.FilterConverters;
using Warehouse.Queries.Filters;
using Warehouse.Queries.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Common;

namespace Warehouse.Queries
{
    public class ItineraryQueryService : IItineraryQueryService
    {
        public ItineraryQueryService(LegacyWarehouseContext legacyWarehouseContext)
        {
            LegacyWarehouseContext = legacyWarehouseContext;
        }

        public LegacyWarehouseContext LegacyWarehouseContext { get; }

        public async Task<bool> StorageCellExistsAsync(string storageCellId)
        {
            var cellKey = LegacyKey.Parse(storageCellId);
            return await this.LegacyWarehouseContext.StorageCells.AnyAsync(c => c.Key1 == cellKey.Key1 && c.Key2 == cellKey.Key2);
        }

        public async Task<CellInfo> SearchCellItineraryInfo(string storageCellId)
        {
            var cellKey = LegacyKey.Parse(storageCellId);

            var filter = new CellItinararyFilter()
            {
                StorageCellId = storageCellId,
                TakeType = ScanState.NotScanned
            };

            var result = await this.LegacyWarehouseContext.ItineraryInvoices
                .Where(filter.ToExpression())
                .Select(m => new CellInfo.UnscannedItinenaryEnclose()
                {
                    BarCode = m.Enclose.Barcode,
                    InvoiceNumber = m.Enclose.Invoice.Number,
                    Client = m.Enclose.Invoice.Contract.Client.Name,
                    Priority = m.Enclose.DeliveryPoint.Type == DeliveryPointType.AutomatedDeliveryPoint ? Convert.ToBoolean(m.Priority) : false,
                    Length = m.Enclose.Length,
                    Width = m.Enclose.Width,
                    Height = m.Enclose.Height
                }).ToListAsync();

            return new CellInfo()
            {
                StorageCellId = storageCellId,
                UnscannedItinenaryEncloses = result
            };
        }
    }
}
