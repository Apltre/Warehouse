using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Protocol;
using Warehouse.Protocol.Models;
using Warehouse.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.WebApi.Controllers
{
    [Route(Uris.Warehouses)]
    public class WarehouseController : Controller
    {
        public WarehouseController(UnitOfWork uow)
        {
            Uow = uow;
        }

        public UnitOfWork Uow { get; }

        [HttpGet]
        [ProducesResponseType(typeof(WarehousesList), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetAsync()
        {
            var warehouses = await this.Uow.WarehouseStore.GetWarehousesAsync();
            return this.Ok(new WarehousesList()
            { 
                Warehouses = warehouses.Select(w => new Protocol.Models.Warehouse() { 
                     Id = w.Id,
                     Name = w.Name
                })
            });
        }

        [HttpGet("{id}/routes")]
        [Authorize]
        [ProducesResponseType(typeof(WarehouseRoutes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Error), 404)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetRoutesAsync([FromRoute]string id)
        {
            var warehouse = await this.Uow.WarehouseStore.SearchWarehouseAsync(id);

            if (warehouse == null)
            {
                return this.NotFound(Error.WarehouseNotFound404);
            }

            var routes = await warehouse.GetRoutesAsync(this.Uow.RouteStore);
            return this.Ok(new WarehouseRoutes
            {
                Routes = routes.Select(r => new Route() { 
                    Id = r.Id,
                    Name = r.Name
                })
            });
        }
    }
}