using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrupoJOS_MVC5.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Model_teste
    {
        public IEnumerable<int> SelectedItemIds { get; set; }
        public IEnumerable<Item> AvailableItems
        {
            get
            {
                return new[]
                {
                new Item { Id = 1, Name = "Item 1" },
                new Item { Id = 2, Name = "Item 2" },
                new Item { Id = 3, Name = "Item 3" },
            };
            }
        }
    }
}