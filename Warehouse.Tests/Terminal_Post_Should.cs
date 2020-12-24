using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warehouse.Domain;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Warehouse.Tests
{
    [TestClass]
    public class Terminal_Post_Should : Test
    {
        [TestMethod]
		public async Task CreateNewTerminal()
        {
			await this.ArrangeWarehouseAsync(context =>
			{
				context.Add(new Domain.Warehouse(
			    	"5186458-923",
					"Moscow.One",
					DateTime.Now
				));
			});

			var client = this.Factory.Create();

			var terminal = await client.CreateTerminalAsync(new Protocol.Models.TerminalCandidate()
			{
				Id = "yyyyyyy",
				IsActive = true,
				WarehouseId = "5186458-923"
			});

			Assert.AreEqual("yyyyyyy", terminal.Id);
			Assert.AreEqual("5186458-923", terminal.Warehouse.Id);
			Assert.AreEqual("Moscow.One", terminal.Warehouse.Name);
			Assert.IsTrue(terminal.IsActive);
		}


		[TestMethod]
		[ExpectedException(typeof(HttpRequestException))]
		public async Task Throw409IfTerminalExists()
		{
			var warehouse = new Domain.Warehouse(
					"5186458-923",
					"Moscow.One",
					DateTime.Now
				);

			await this.ArrangeWarehouseAsync(context =>
			{
				context.Add(new Terminal("yyyyyyy", warehouse, true));
			});

			var client = this.Factory.Create();

			try
			{
				await client.CreateTerminalAsync(new Protocol.Models.TerminalCandidate()
				{
					Id = "yyyyyyy",
					IsActive = true,
					WarehouseId = "5186458-923"
				});
			}
			catch (HttpRequestException ex)
			{
				Assert.AreEqual(ex.Message, "Response status code does not indicate success: 409 (Conflict).");
				throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(HttpRequestException))]
		public async Task Throw400IfWarehouseDoesntExist()
		{
			var client = this.Factory.Create();
			try
			{
				await client.CreateTerminalAsync(new Protocol.Models.TerminalCandidate()
				{
					Id = "yyyyyyy",
					IsActive = true,
					WarehouseId = "5186458-923"
				});
			}
			catch(HttpRequestException ex)
			{
				Assert.AreEqual(ex.Message, "Response status code does not indicate success: 400 (Bad Request).");
				throw;
			}
		}
	}
}
