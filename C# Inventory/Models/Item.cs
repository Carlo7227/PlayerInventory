using CInventory.Models.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CInventory.Models
{
    public class Item : IFromJObject<Item>
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Icon { get; set; }
        public bool Stackable { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Weight { get; set; }
        public int StackLimit { get; set; }
        public string Description { get; set; }
        public int SellPrice { get; set; }
        public Item Copy()
        {
            return new Item
            {
                Id = Id,
                Amount = Amount,
                Icon = Icon,
                Stackable = Stackable,
                Name = Name,
                Type = Type,
                Weight = Weight,
                StackLimit = StackLimit,
                Description = Description,
                SellPrice = SellPrice
            };
        }
        public Item FromJObject(JObject jObject)
        {
            if (jObject.ContainsKey("id")) this.Id = jObject["id"].Value<int>();
            if (jObject.ContainsKey("amount")) this.Amount = jObject["amount"].Value<int>();
            if (jObject.ContainsKey("icon")) this.Icon = jObject["icon"].Value<string>();
            if (jObject.ContainsKey("stackable")) this.Stackable = jObject["stackable"].Value<bool>();
            if (jObject.ContainsKey("type")) this.Type = jObject["type"].Value<string>();
            if (jObject.ContainsKey("weight")) this.Weight = jObject["weight"].Value<decimal>();
            if (jObject.ContainsKey("stacklimit")) this.StackLimit = jObject["stacklimit"].Value<int>();
            if (jObject.ContainsKey("description")) this.Description = jObject["description"].Value<string>();
            if (jObject.ContainsKey("sellprice")) this.SellPrice = jObject["sellprice"].Value<int>();
            return this;
        }
    }
}