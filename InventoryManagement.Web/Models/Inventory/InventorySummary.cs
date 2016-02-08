using System;
using System.Collections.Generic;
using AutoMapper;
using System.Web.Mvc;
using InventoryManagement.Web.Infrastructure.Mapping;

namespace InventoryManagement.Web.Models.Inventory
{
    public class InventorySummary
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public IList<SelectListItem> ItemList { get; set; }
        public int NumberOfPieces { get; set; }
        public int NumberOfCartons { get; set; }
        public int TotalPieces { get; set; }

    }
}