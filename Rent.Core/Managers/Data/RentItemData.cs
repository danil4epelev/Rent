using Rent.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Core.Managers.Data
{
	public class RentItemData
	{
		public long Id { get; set; }
		public Guid Uid { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public long ChapterId { get; set; }
		public decimal Price { get; set; }
		public int PriceType { get; set; }
		public long AuthorId { get; set; }
		public long? ModeratorId { get; set; }
	}
}
