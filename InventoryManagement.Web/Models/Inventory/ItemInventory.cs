﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagement.Web.Models.Inventory
{
    public class ItemInventory
    {
        public int NumberOfPieces { get; set; }
        public int ItemId { get; set; }

        public IList<SelectListItem> ItemList { get; set; }
    }
}