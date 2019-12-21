using CInventory.Models.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CInventory.Models
{
    public class PlayerData : IFromJObject<PlayerData>
    {
        public Dictionary<string, Item> Inventory { get; set; }
        public PlayerData FromJObject(JObject jObject)
        {
            var inventoryJObject = (jObject["inventory"] as JObject);
            Inventory = new Dictionary<string, Item>();
            foreach (var prop in inventoryJObject.Properties())
            {
                var key = prop.Name;
                var item = new Item();
                var itemJObject = inventoryJObject[prop.Name] as JObject;
                item.FromJObject(itemJObject);
                Inventory.Add(key, item);
            }
            return this;
        }
    }
}