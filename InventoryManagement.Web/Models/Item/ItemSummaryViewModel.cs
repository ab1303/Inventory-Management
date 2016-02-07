using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagement.Web.Infrastructure.Mapping;

namespace InventoryManagement.Web.Models.Item
{
    public class ItemSummaryViewModel : IMapFrom<Domain.Item>
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}