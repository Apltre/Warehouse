using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warehouse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Warehouse.Tests
{
    [TestClass]
    public class Terminal_Get_Should : Test
    {
        [TestMethod]
        public async Task FindExistingTerminal()
        {
			var warehouse = new Domain.Warehouse(
					"5186458-923",
					"Moscow.One",
					DateTime.Now
				);
			await this.ArrangeWarehouseAsync(context =>
			{
				context.Add(new Terminal(
					"xxxxx",
					new Domain.Warehouse(
						"5186458-923",
						"Moscow.One",
						DateTime.Now
					),
					true));
			});
			var client = this.Factory.Create();

            var terminal = await client.GetTerminalByIdAsync("xxxxx");

            Assert.AreEqual("xxxxx", terminal.Id);
			Assert.AreEqual("5186458-923", terminal.Warehouse.Id);
			Assert.AreEqual("Moscow.One", terminal.Warehouse.Name);
			Assert.IsTrue(terminal.IsActive);
		}


		[TestMethod]
		[ExpectedException(typeof(HttpRequestException))]
		public async Task Throw404IfTerminalNotFound()
		{
			var client = this.Factory.Create();
			try
			{
				await client.GetTerminalByIdAsync("xxxxx");
			}
			catch (HttpRequestException ex)
			{
				Assert.AreEqual(ex.Message, "Response status code does not indicate success: 404 (Not Found).");
				throw;
			}
		}

		[TestMethod]
		public async Task GetAllExistingTerminals()
		{
			var warehouse = new Domain.Warehouse(
					"5186458-923",
					"Moscow.One",
					DateTime.Now
				);

			var terminalEntites = new List<Terminal>() {
				new Terminal(
					"1-1",
					warehouse,
					false
				),
				new Terminal(
					"2-2",
					warehouse,
					true
				),
				new Terminal(
					"3-3",
					warehouse,
					true
				)
			};
			await this.ArrangeWarehouseAsync(context =>
			{
				context.AddRange(terminalEntites);
			});
			var client = this.Factory.Create();

			var terminals = await client.GetTerminalsAsync();

			Assert.AreEqual(terminalEntites.Count, terminals.Count());

			for (var i = 0; i < terminalEntites.Count; i++)
			{
				var activeTerminalEntity = terminalEntites[i];
				var terminal = terminals.ElementAt(i);
				Assert.AreEqual(activeTerminalEntity.Id, terminal.Id);
				Assert.AreEqual(activeTerminalEntity.IsActive, terminal.IsActive);
			}
		}
	}
}
