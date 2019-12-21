using CInventory.Models.Interfaces;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CInventory.Models
{
    public class ItemDatabase : IFromJObject<ItemDatabase>
    {
        public Dictionary<string, Item> Items { get; set; }
        public ItemDatabase FromJObject(JObject jObject)
        {
            Items = new Dictionary<string, Item>();
            foreach (var prop in jObject.Properties())
            {
                var key = prop.Name;
                var item = new Item();
                var itemJObject = jObject[prop.Name] as JObject;
                item.FromJObject(itemJObject);
                Items.Add(key, item);
            }
            return this;
        }
    }
}