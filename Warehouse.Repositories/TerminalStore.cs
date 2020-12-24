using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Domain;
using Warehouse.Domain.StoreAbstractions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Repositories
{
    public class TerminalStore : ITerminalStore
    {
        public TerminalStore(WarehouseContext warehouseContext)
        {
            WarehouseContext = warehouseContext;
        }

        protected WarehouseContext WarehouseContext { get; }

        public async Task<Terminal> SearchTerminalAsync(string id)
        {
            return await this.WarehouseContext.Terminals.Include(p => p.Warehouse).FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Save(Terminal terminal)
        {
            var checkedTerminal = this.WarehouseContext.Terminals.Find(terminal.Id);
            if (checkedTerminal == null)
            {
                this.WarehouseContext.Terminals.Add(terminal);
            }
        }
        public async Task<IEnumerable<Terminal>> GetTerminalsAsync()
        {
            return await this.WarehouseContext.Terminals.ToListAsync();
        }
    }
}
