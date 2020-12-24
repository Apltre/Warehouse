using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Protocol;
using Warehouse.Protocol.Models;
using Warehouse.Queries.Abstractions;
using Warehouse.WebApi.Mappers;
using System.Net;
using System.Threading.Tasks;

namespace Warehouse.WebApi.Controllers
{
    [Route(Uris.Cells)]
    public class CellsController : Controller
    {
        public CellsController(IItineraryQueryService itineraryQueryService)
        {
            ItineraryQueryService = itineraryQueryService;
        }

        public IItineraryQueryService ItineraryQueryService { get; }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CellInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            if (!(await this.ItineraryQueryService.StorageCellExistsAsync(id)))
            {
                return this.NotFound(new Error() { Code = HttpStatusCode.NotFound, Reason = "Cell not found!" });
            }

            var data = await this.ItineraryQueryService.SearchCellItineraryInfo(id);

            return this.Ok(data.Map());
        }
    }
}
