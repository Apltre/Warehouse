using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warehouse.Domain;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Warehouse.Tests
{
	[TestClass]
	public class Terminal_Patch_Should : Test
	{
		[TestMethod]
		public async Task UpdateTerminal()
		{
			await this.ArrangeWarehouseAsync(context =>
			{
				context.Add(new Terminal(
									"t1-t1",
									new Domain.Warehouse(
												"w1-w1",
												"Moscow.One",
												DateTime.Now
									)
									, true));
				context.Add(new Domain.Warehouse(
												"w2-w2",
												"Spb.One",
												DateTime.Now
									));
			});

			var client = this.Factory.Create();

			var patchResult = await client.PatchTerminalAsync("t1-t1", new Protocol.Models.TerminalPatch()
			{
				IsActive = false,
				WarehouseId = "w2-w2"
			});

			Assert.AreEqual(patchResult.Id, "t1-t1");
			Assert.AreEqual(patchResult.Warehouse.Id, "w2-w2");
			Assert.AreEqual(patchResult.IsActive, false);

			var updatedTerminal = await client.GetTerminalByIdAsync("t1-t1");

			Assert.AreEqual(updatedTerminal.Id, "t1-t1");
			Assert.AreEqual(updatedTerminal.Warehouse.Id, "w2-w2");
			Assert.AreEqual(patchResult.IsActive, false);
		}

		[TestMethod]
		[ExpectedException(typeof(HttpRequestException))]
		public async Task Throw400IfTargetWarehouseDoesntExist()
		{
			await this.ArrangeWarehouseAsync(context =>
			{
				context.Add(new Terminal(
									"t1-t1",
									new Domain.Warehouse(
												"w1-w1",
												"Moscow.One",
												DateTime.Now
									)
									, true));
			});

			var client = this.Factory.Create();

			try
			{
				await client.PatchTerminalAsync("t1-t1", new Protocol.Models.TerminalPatch()
				{
					IsActive = false,
					WarehouseId = "w2-w2"
				});
			}
			catch (HttpRequestException ex)
			{
				Assert.AreEqual(ex.Message, "Response status code does not indicate success: 400 (Bad Request).");
				throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(HttpRequestException))]
		public async Task Throw404IfTerminalDoesntExist()
		{
			await this.ArrangeWarehouseAsync(context =>
			{
			});

			var client = this.Factory.Create();

			try
			{
				await client.PatchTerminalAsync("t1-t1", new Protocol.Models.TerminalPatch()
				{
					IsActive = false,
					WarehouseId = "w2-w2"
				});
			}
			catch (HttpRequestException ex)
			{
				Assert.AreEqual(ex.Message, "Response status code does not indicate success: 404 (Not Found).");
				throw;
			}
		}
	}
}
