using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Astor.JsonHttp;
using Newtonsoft.Json;
using Warehouse.Protocol.Models;

namespace Warehouse.Protocol
{
    public class WarehouseClient : RestApiClient
    {
        public WarehouseClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<About> GetAboutAsync()
        {
            var response = await this.HttpClient.GetAsync(Uris.About);
            return await this.ReadAsync<About>(response);
        }

        public async Task<Terminal> GetTerminalByIdAsync(string id)
        {
            var response = await this.HttpClient.GetAsync($"{Uris.Terminals}/{id}");
            return await this.ReadAsync<Terminal>(response);
        }

        public async Task<Terminal> CreateTerminalAsync(TerminalCandidate request)
        {
            var response = await this.HttpClient.PostJsonAsync($"{Uris.Terminals}", request);
            return await this.ReadAsync<Terminal>(response);
        }

        public async Task<IEnumerable<Models.Warehouse>> GetWarehousesAsync()
        {
            var response = await this.HttpClient.GetAsync(Uris.Warehouses);
            return (await this.ReadAsync<WarehousesList>(response)).Warehouses;
        }

        public async Task<IEnumerable<TerminalWithoutWarehouse>> GetTerminalsAsync()
        {
            var response = await this.HttpClient.GetAsync(Uris.Terminals);
            return (await this.ReadAsync<TerminalsList>(response)).Terminals;
        }

        public async Task<WarehouseRoutes> GetWarehouseRoutesAsync(string warehouseId)
        {
            var response = await this.HttpClient.GetAsync($"{Uris.Warehouses}/{warehouseId}/routes");
            return await this.ReadAsync<WarehouseRoutes>(response);
        }

        public async Task<Terminal> PatchTerminalAsync(string terminalId, TerminalPatch request)
        {
            var response = await this.HttpClient.PatchJsonAsync($"{Uris.Terminals}/{terminalId}", request);
            return await this.ReadAsync<Terminal>(response);
        }

        public async Task<CellInfo> GetItineraryCellInfoAsync(string StorageCellId)
        {
            var response = await this.HttpClient.GetAsync($"{Uris.Cells}/{StorageCellId}");
            return await this.ReadAsync<CellInfo>(response);
        }
    }
}