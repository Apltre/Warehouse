using Microsoft.Extensions.DependencyInjection;
using Warehouse.LegacyData;
using Warehouse.Data;
using System;
using System.Threading.Tasks;

namespace Warehouse.Tests
{
	public class Test
	{
		public readonly WebApplicationFactory Factory = new WebApplicationFactory();
		public readonly string TestJwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJvcGVyYXRvcl9pZCI6IjExMTExMTExMS0wMDAwIiwibmJmIjoxNjAwNzY4NDM2LCJleHAiOjE2NjA3Njg0MzYsImlhdCI6MTYwMDc2ODQzNiwiaXNzIjoiVmFsaWRVc2VyQ29tcCIsImF1ZCI6IndhcmVob3VzZSJ9.dKRyUMeEw-J3JKrWF25ug3GBh0xP-iN8lTGeiw1RAHM";

		public async Task ArrangeWarehouseAsync(Action<WarehouseContext> arrangements)
		{
			using var scope = this.Factory.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<WarehouseContext>();
			arrangements(context);
			await context.SaveChangesAsync();
		}

		public async Task ArrangLegacyWarehouseContextAsync(Action<LegacyWarehouseContext> arrangements)
		{
			using var scope = this.Factory.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<LegacyWarehouseContext>();
			arrangements(context);
			await context.SaveChangesAsync();
		}
	}
}