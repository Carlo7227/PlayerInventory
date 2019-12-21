using CInventory.Models;
using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
public class GlobalItemDatabase
{
    public static ItemDatabase ItemDatabase { get; set; }
    public GlobalItemDatabase()
    {
        var parser = new GlobalDataParser();
        ItemDatabase = parser.LoadData<ItemDatabase>("res://Database//Database_Items.json");
    }
    public Item GetItem(string id)
    {
        ItemDatabase.Items.TryGetValue(id, out Item item);
        return item ?? new Item();
    }
}