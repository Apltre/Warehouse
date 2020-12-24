using Warehouse.Domain.StoreAbstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Domain
{
    public class Warehouse
    {
        protected Warehouse(){ }

        public Warehouse(string id, string name, DateTime createdOn)
        {
            this.Id = id;
            this.Name = name;
            this.CreatedOn = createdOn;
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public DateTime CreatedOn { get; protected set; }


        public async Task<IEnumerable<Route>> GetRoutesAsync(IRouteStore routeStore)
        {
            return await routeStore.GetRoutesByWarehouseAsync(this);
        }
    }
}