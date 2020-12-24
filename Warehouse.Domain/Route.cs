using System;

namespace Warehouse.Domain
{
	public class Route
	{
		public Route(string id, Warehouse warehouse, string name)
		{
			this.Id = id;
			this.Warehouse = warehouse ?? throw new ArgumentNullException("Routes's warehouse must exist!");
			this.Name = name;
		}

		public string Id { get; protected set; }
		public Warehouse Warehouse { get; protected set; }
		public string Name { get; protected set; }
	}
}
