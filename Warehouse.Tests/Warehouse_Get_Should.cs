using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warehouse.LegacyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Common;

namespace Warehouse.Tests
{
	[TestClass]
	public class Warehouse_Get_Should : Test
	{
		[TestMethod]
		public async Task GetAllExistingWarehouses()
		{
			var warehousesEntites = new List<Domain.Warehouse>() {
				new Domain.Warehouse(
					"1-1",
					"First",
					DateTime.Now
				),
				new Domain.Warehouse(
					"2-2",
					"Second",
					DateTime.Now
				)
			};
			await this.ArrangeWarehouseAsync(context =>
			{
				context.AddRange(warehousesEntites);
			});
			var client = this.Factory.Create();

			var warehouses = await client.GetWarehousesAsync();

			Assert.AreEqual(warehousesEntites.Count, warehouses.Count());

			for (var i = 0; i < warehousesEntites.Count; i++)
			{
				var entityWarehouse = warehousesEntites[i];
				var warehouse = warehouses.ElementAt(i);
				Assert.AreEqual(entityWarehouse.Id, warehouse.Id);
				Assert.AreEqual(entityWarehouse.Name, warehouse.Name);
			}
		}

		[TestMethod]
		public async Task GetAllWarehouseRoutes()
		{
			var warehouseKey = new LegacyKey(1, 1);
			var warehouse = new Domain.Warehouse(
					warehouseKey.ToString(),
					"First",
					DateTime.Now
				);

			var warehouseKey1 = new LegacyKey(2, 2);

			await this.ArrangeWarehouseAsync(context =>
			{
				context.Add(warehouse);
			});

			await this.ArrangLegacyWarehouseContextAsync(context =>
			{
				context.AddRange(
					new List<RouteCell>()
					{
						new RouteCell()
						{
							Key = new LegacyKey(1, 3),
							Route = new Route()
							{
								Key = new LegacyKey(1, 2),
								RouteNumber = "A1"
							},
							StorageCell = new StorageCell()
							{
								Key = new LegacyKey(1, 4),
								StorageKey1 = warehouseKey.Key1,
								StorageKey2 = warehouseKey.Key2
							}
						},
						new RouteCell()
						{
							Key = new LegacyKey(2, 3),
							RouteKey1 = 1,
							RouteKey2 = 2,
							StorageCellKey1 = 1,
							StorageCellKey2 = 4
						},
						new RouteCell()
						{
							Key = new LegacyKey(3, 3),
							RouteKey1 = 1,
							RouteKey2 = 2,
							StorageCellKey1 = 1,
							StorageCellKey2 = 4
						},
						new RouteCell()
						{
							Key = new LegacyKey(4, 3),
							Route = new Route()
							{
								Key = new LegacyKey(2, 2),
								RouteNumber = "B1"
							},
							StorageCell = new StorageCell()
							{
								Key = new LegacyKey(2, 4),
								StorageKey1 = warehouseKey.Key1,
								StorageKey2 = warehouseKey.Key2
							}
						},
						new RouteCell()
						{
							Key = new LegacyKey(5, 3),
							Route = new Route()
							{
								Key = new LegacyKey(3, 2),
								RouteNumber = "C1"
							},
							StorageCell = new StorageCell()
							{
								Key = new LegacyKey(3, 4),
								StorageKey1 = warehouseKey1.Key1,
								StorageKey2 = warehouseKey1.Key2
							}
						}
					});
			});

			var client = this.Factory.Create(this.TestJwtToken);

			var resultWarehouseRoutes = await client.GetWarehouseRoutesAsync(warehouseKey.ToString());

			Assert.AreEqual(resultWarehouseRoutes.Count, 2, "WrongResultsNumber");
			Assert.IsTrue(resultWarehouseRoutes.Routes.Any(r => r.Id == new LegacyKey(1, 2).ToString()));
			Assert.IsTrue(resultWarehouseRoutes.Routes.Any(r => r.Id == new LegacyKey(2, 2).ToString()));
		}
	}
}
