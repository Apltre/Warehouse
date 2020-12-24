using Warehouse.LegacyData.Entity;
using SpeciVacation;
using System;
using System.Linq.Expressions;
using Warehouse.Common;

namespace Warehouse.Queries.Specifications.ForItineraryInvoice
{
    public class TargetStorageCellSpecification : Specification<ItineraryInvoice>
    {
        public LegacyKey StorageCellKey { get; }

        public TargetStorageCellSpecification(string StorageCellId)
        {
            this.StorageCellKey = LegacyKey.Parse(StorageCellId);
        }

        public override Expression<Func<ItineraryInvoice, bool>> ToExpression()
        {
            return m => m.StorageCellEnclose.StorageCell.Key1 == StorageCellKey.Key1 
                     && m.StorageCellEnclose.StorageCell.Key2 == StorageCellKey.Key2;
        }
    }
}
