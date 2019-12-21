using CInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CInventory
{
    public class InventoryController
    {
        public GlobalPlayer GlobalPlayer { get; set; }
        public GlobalItemDatabase GlobalItemDatabase { get; set; }

        public InventoryController(GlobalPlayer globalPlayer, GlobalItemDatabase globalItemDatabase)
        {
            GlobalPlayer = globalPlayer;
            GlobalItemDatabase = globalItemDatabase;
        }
        public Item GetInventoryItem(string id)
        {
            var idString = id.ToString();
            var inventory = GlobalPlayer.Inventory;
            var inventoryItem = inventory[idString];
            //var inventoryItem = GlobalPlayer.PlayerData.Inventory[id.ToString()];
            var itemMetaData = GlobalItemDatabase.GetItem(inventoryItem.Id.ToString()).Copy();
            itemMetaData.Amount = inventoryItem.Amount;
            return itemMetaData;
        }
        
        public static InventoryController Default()
        {
            return new InventoryController(new GlobalPlayer(), new GlobalItemDatabase());
        }
    }
}
