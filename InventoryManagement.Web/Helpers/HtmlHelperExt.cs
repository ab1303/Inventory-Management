using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagement.Web.Helpers
{
    public static class HtmlHelperExt
    {

        public static SelectListItem[] ToListItems<TEntity>(this IEnumerable<TEntity> collection, Func<TEntity, string> name, Func<TEntity, string> value, bool prependBlank = false)
        {
            var items = new List<SelectListItem>();

            if (prependBlank)
                items.Add(new SelectListItem { Text = "", Value = "" });

            items.AddRange(collection.Select(x => new SelectListItem { Text = name(x), Value = value(x) }));

            return items.ToArray();
        }

        public static SelectListItem[] ToListItems<TEntity>(this IEnumerable<TEntity> collection, Func<TEntity, string> name, Func<TEntity, string> value, string firstItemText)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{Text = firstItemText,Value = ""}
            };

            items.AddRange(collection.Select(x => new SelectListItem { Text = name(x), Value = value(x) }).OrderBy(x => x.Text));

            return items.ToArray();
        }
        public static SelectListItem[] ToListItems<TEntity>(this IEnumerable<TEntity> collection, Func<TEntity, string> name, Func<TEntity, string> value, Func<TEntity, bool> selected)
        {
            var items = new List<SelectListItem>();

            items.AddRange(collection.Select(x => new SelectListItem { Text = name(x), Value = value(x), Selected = selected(x) }).OrderBy(x => x.Text));

            return items.ToArray();
        }
    }
}