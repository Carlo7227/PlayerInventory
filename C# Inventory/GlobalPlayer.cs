using CInventory.Models;
using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
public class GlobalPlayer
{
    public const string URLPlayerData = "user://PlayerData.json";
    public const int InventoryMaxSlots = 45;
    public static Dictionary<string, Item> Inventory { get; set; }
    public PlayerData PlayerData { get; set; }
    public GlobalPlayer()
    {
        var parser = new GlobalDataParser();
        PlayerData = parser.LoadData<PlayerData>(URLPlayerData)
            ?? new PlayerData();
        LoadData();
    }
    public void LoadData()
    {
        if (PlayerData?.Inventory == null)
        {
            var inventory = new Dictionary<string, Item>();
            for (var i = 0; i < InventoryMaxSlots; i++)
            {
                inventory.Add(i.ToString(), new Item { Id = 0, Amount = 0 });
            }
            GlobalPlayer.Inventory = inventory;
            var parser = new GlobalDataParser();
            parser.WriteData(URLPlayerData, inventory);
        }
        else
        {
            GlobalPlayer.Inventory = this.PlayerData.Inventory;
        }
    }
}