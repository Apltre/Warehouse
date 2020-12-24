using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warehouse.LegacyData.Entity;
using Warehouse.LegacyData.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Warehouse.Tests
{
    [TestClass]
    public class Cells_Get_Should : Test
    {
        [TestMethod]
        public async Task GetItineraryCellInfo()
        {
			var unscannedItineraryInvoice = new ItineraryInvoice()
			{
				Key1 = 1,
				Key2 = 1,
				Priority = 1,
				ScanState = ScanState.NotScanned,
				Enclose = new Enclose()
				{
					Key1 = 1,
					Key2 = 2,
					Barcode = "22222222222",
					Length = 1.5,
					Height = 2.0,
					Width = 3.5,
					StorageKey1 = 1,
					StorageKey2 = 10,
					Invoice = new Invoice()
					{
						Key1 = 1,
						Key2 = 3,
						Number = "33333333333",
						Contract = new Contract()
						{
							Key1 = 1,
							Key2 = 5,
							Client = new Client()
							{
								Key1 = 1,
								Key2 = 6,
								Name = "OOO Roga i kopita"
							}
						}
					},
					DeliveryPoint = new DeliveryPoint()
					{
						Key1 = 1,
						Key2 = 4,
						Type = DeliveryPointType.AutomatedDeliveryPoint
					}
				},
				StorageCellEnclose = new StorageCellEnclose()
				{
					Key1 = 1,
					Key2 = 7,
					StorageCell = new StorageCell() 
					{
						Key1 = 1,
						Key2 = 8
					}
				}
			};
			var scannedItineraryInvoice = new ItineraryInvoice()
			{
				Key1 = 2,
				Key2 = 1,
				Priority = 1,
				ScanState = ScanState.Scanned,
				Enclose = new Enclose()
				{
					Key1 = 2,
					Key2 = 2,
					Barcode = "22222222223",
					Length = 1.5,
					Height = 2.0,
					Width = 3.5,
					InvoiceKey1 = 1,
					InvoiceKey2 = 3,
					StorageKey1 = 1,
					StorageKey2 = 10
				},
				StorageCellEnclose = new StorageCellEnclose()
				{
					Key1 = 2,
					Key2 = 7,
					StorageCellKey1 = 1,
					StorageCellKey2 = 8
				}
			};
			var differentCellItineraryInvoice = new ItineraryInvoice()
			{
				Key1 = 3,
				Key2 = 1,
				Priority = 1,
				ScanState = ScanState.NotScanned,
				Enclose = new Enclose()
				{
					Key1 = 3,
					Key2 = 2,
					Barcode = "22222222224",
					Length = 1.5,
					Height = 2.0,
					Width = 3.5,
					StorageKey1 = 1,
					StorageKey2 = 10,
					Invoice = new Invoice()
					{
						Key1 = 2,
						Key2 = 3,
						Number = "33333333334",
						Contract = new Contract()
						{
							Key1 = 2,
							Key2 = 5,
							Client = new Client()
							{
								Key1 = 2,
								Key2 = 6,
								Name = "OAO Zimnie vechera"
							}
						}
					}
				},
				StorageCellEnclose = new StorageCellEnclose()
				{
					Key1 = 3,
					Key2 = 7,
					StorageCell = new StorageCell()
					{
						Key1 = 2,
						Key2 = 8
					}
				}
			};

			await this.ArrangLegacyWarehouseContextAsync(context =>
			{
				context.AddRange(new List<ItineraryInvoice> { scannedItineraryInvoice, unscannedItineraryInvoice, differentCellItineraryInvoice });
			});

			var client = this.Factory.Create();

            var cellInfo = await client.GetItineraryCellInfoAsync("1-8");

			Assert.AreEqual(cellInfo.Encloses.Count, 1, "Wrong encloses count");
			var enclose = cellInfo.Encloses.Elements.Single();
			Assert.AreEqual(enclose.Id, "22222222222", "Wrong enclose barcode");
			Assert.AreEqual(enclose.Priority, true, "Wrong priority");
			Assert.AreEqual(cellInfo.Status.Value, "partially", "Wrong cell itinerary invoice status");
			Assert.AreEqual(enclose.Dimensions.Text, "3.50*1.50*2.00", "Wrong enclose dimensions text");
			Assert.AreEqual(enclose.Client.Name, "OOO Roga i kopita", "Wrong enclose client name");
		}


		[TestMethod]
		[ExpectedException(typeof(HttpRequestException))]
		public async Task Throw404IfCellNotFound()
		{
			var client = this.Factory.Create();
			try
			{
				await client.GetItineraryCellInfoAsync("1234-926");
			}
			catch (HttpRequestException ex)
			{
				Assert.AreEqual(ex.Message, "Response status code does not indicate success: 404 (Not Found).");
				throw;
			}
		}
	}
}
