using Warehouse.LegacyData.Entity;
using Warehouse.LegacyData.Enums;
using SpeciVacation;
using System;
using System.Linq.Expressions;

namespace Warehouse.Queries.Specifications.ForItineraryInvoice
{
    public class HasTakenTypeSpecification : Specification<ItineraryInvoice>
    {
        public HasTakenTypeSpecification(ScanState type)
        {
            Type = type;
        }

        public ScanState Type { get; }

        public override Expression<Func<ItineraryInvoice, bool>> ToExpression()
        {
            return m => m.ScanState == Type;
        }
    }
}
