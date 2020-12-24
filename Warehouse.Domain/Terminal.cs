using System;

namespace Warehouse.Domain
{
	public class Terminal
	{
		protected Terminal()
		{
		}
		public Terminal(string id, Warehouse warehouse, bool isActive) 
		{
			this.Id = id;
			this.Warehouse = warehouse ?? throw new ArgumentNullException("Terminal's warehouse must exist!");
			this.IsActive = isActive;
			this.CreatedOn = DateTime.Now;
		}

		public string Id { get; protected set; }
		public Warehouse Warehouse { get; protected set; }
		public bool IsActive { get; protected set; }
		public DateTime CreatedOn { get; protected set; }

		public void SetActiveState(bool isActive)
		{
			this.IsActive = isActive;
		}

		public void ChangeWarehouse(Warehouse warehouse)
		{
			this.Warehouse = warehouse;
		}
	}
}
