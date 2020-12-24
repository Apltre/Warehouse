using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Domain.StoreAbstractions
{
    public interface ITerminalStore
    {
        Task<Terminal> SearchTerminalAsync(string id);

        Task<IEnumerable<Terminal>> GetTerminalsAsync();

        void Save(Terminal terminal);
    }
}
