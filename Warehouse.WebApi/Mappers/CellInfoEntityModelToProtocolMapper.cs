using Warehouse.WebApi.Helpers;
using System;
using System.Linq;
using CellInfoData = Warehouse.Queries.Models.CellInfo;
using CellInfoDto = Warehouse.Protocol.Models.CellInfo;

namespace Warehouse.WebApi.Mappers
{
    public static class CellInfoEntityModelToProtocolMapper
    {
        public static string ItineraryStatusForCell(CellInfoData.Status status)
        {
            switch (status)
            {
                case CellInfoData.Status.Partially:
                    return "PartiallyScanned";
                case CellInfoData.Status.Fully:
                    return "FullyScanned";
                default:
                    throw new Exception("Unexpected itinerary status!");
            }
        }

        public static CellInfoDto Map(this CellInfoData data)
        {
            return new CellInfoDto()
            {
                Id = data.StorageCellId,
                Status = new CellInfoDto.ItineraryStatus()
                {
                    Text = ItineraryStatusForCell(data.ItineraryStatusForCell),
                    Value = data.ItineraryStatusForCell.ToString().ToLower(),
                },
                Encloses = new CellInfoDto.UnscannedItinenaryEnclosesList()
                {
                    Count = data.UnscannedEnclosesCount,
                    Elements = data.UnscannedItinenaryEncloses.Select(e =>
                        new CellInfoDto.UnscannedItinenaryEnclose()
                        {
                            Id = e.BarCode,
                            InvoiceId = e.InvoiceNumber,
                            Priority = e.Priority,
                            Client = new CellInfoDto.EncloseClient()
                            {
                                Name = e.Client
                            },
                            Dimensions = new CellInfoDto.EncloseDimensions()
                            {
                                Depth = e.Height,
                                Length = e.Length,
                                Width = e.Width,
                                Text = $"{e.Width.ToPointString()}*{e.Length.ToPointString()}*{e.Height.ToPointString()}"
                            }
                        })
                }
            };
        }
    }
}
