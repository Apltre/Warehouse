using Warehouse.LegacyData.Entity;
using Warehouse.Queries.Filters;
using Warehouse.Queries.Specifications.ForItineraryInvoice;
using SpeciVacation;
using System;
using System.Linq.Expressions;

namespace Warehouse.Queries.FilterConverters
{
    public static class CellItinararyInfoFilterConverter
    {
        public static Expression<Func<ItineraryInvoice, bool>> ToExpression(this CellItinararyFilter filter)
        {
            ISpecification<ItineraryInvoice> spec = Specification<ItineraryInvoice>.All;

            if (!string.IsNullOrWhiteSpace(filter.StorageCellId))
            {
                spec = spec.And(new TargetStorageCellSpecification(filter.StorageCellId));
            }

            if (filter.TakeType.HasValue)
            {
                spec = spec.And(new HasTakenTypeSpecification(filter.TakeType.Value));
            }

            return spec.ToExpression();
        }
    }
}
