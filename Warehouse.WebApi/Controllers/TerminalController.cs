using Warehouse.Protocol;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Warehouse.Protocol.Models;
using System.Linq;
using Warehouse.Repositories;
using Microsoft.AspNetCore.Http;

namespace Warehouse.WebApi.Controllers
{
    [Route(Uris.Terminals)]
    public class TerminalController : Controller
    {
        public UnitOfWork Uow { get; }

        public TerminalController(UnitOfWork uow)
        {
            Uow = uow;
        }

        [HttpGet]
        [ProducesResponseType(typeof(TerminalsList), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetAsync()
        {
            var terminals = await this.Uow.TerminalStore.GetTerminalsAsync();

            return this.Ok(new TerminalsList
            {
                Terminals = terminals.Select(t => new TerminalWithoutWarehouse() {
                    Id = t.Id,
                    IsActive = t.IsActive
                })
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Terminal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), 404)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")]string id)
        {
            var terminal = await this.Uow.TerminalStore.SearchTerminalAsync(id);

            if (terminal == null)
            {
                return this.NotFound(Error.TerminalNotFound404);
            }

            return this.Ok(this.map(terminal));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Terminal), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), 409)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> PostAsync([FromBody]TerminalCandidate request)
        {
            var existingTerminal  = await this.Uow.TerminalStore.SearchTerminalAsync(request.Id);

            if (existingTerminal != null)
            {
                return this.Conflict(Error.TerminalAlreadyExists);
            }

            var warehouse = await this.Uow.WarehouseStore.SearchWarehouseAsync(request.WarehouseId);

            if (warehouse == null)
            {
                return this.BadRequest(Error.WarehouseNotFound400);
            }

            var newTerminal = new Domain.Terminal(request.Id, warehouse, request.IsActive);

            this.Uow.TerminalStore.Save(newTerminal);
            await this.Uow.CommitAsync();

            return this.Created(string.Empty, new Terminal()
            {
                Id = newTerminal.Id,
                IsActive = newTerminal.IsActive,
                Warehouse = new Protocol.Models.Warehouse()
                {
                    Id = warehouse.Id,
                    Name = warehouse.Name
                }
            });
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Terminal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), 404)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> PatchAsync([FromRoute(Name = "id")]string id, [FromBody]TerminalPatch request)
        {
            var terminal = await this.Uow.TerminalStore.SearchTerminalAsync(id);

            if (terminal == null)
            {
               return this.NotFound(Error.TerminalNotFound404);
            }

            if (request != null)
            {
                if (request.WarehouseId != null)
                {
                    var warehouse = await this.Uow.WarehouseStore.SearchWarehouseAsync(request.WarehouseId);

                    if (warehouse == null)
                    {
                        return this.BadRequest(Error.WarehouseNotFound400);
                    }

                    terminal.ChangeWarehouse(warehouse);
                }

                if (request.IsActive.HasValue)
                {
                    terminal.SetActiveState(request.IsActive.Value);
                }

                this.Uow.TerminalStore.Save(terminal);
            }

            await this.Uow.CommitAsync();

            return this.Ok(this.map(terminal));
        }

        private Terminal map(Domain.Terminal terminal)
        {
            return new Terminal
            {
                Id = terminal.Id,
                IsActive = terminal.IsActive,
                Warehouse = new Protocol.Models.Warehouse()
                {
                    Id = terminal.Warehouse.Id,
                    Name = terminal.Warehouse.Name
                }
            };
        }
    }
}