using CInventory;
using Godot;
using System;
public class Control : Godot.Control
{
    // Declare member variables here. Examples:
    private int activeItemSlot = -1;
    private int dropItemSlot = -1;
    private bool isDraggingItem = false;
    private bool mouseButtonReleased = true;
    private int draggedItemSlot = -1;
    private bool isAwaitingSplit = false;
    private int splitItemSlot = -1;
    private Vector2 initial_mousePos = Vector2.Zero;
    private bool cursor_insideItemList = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var ItemList = this.GetNode<ItemList>("Panel/ItemList");
        ItemList.SetMaxColumns(10);
        ItemList.SetFixedIconSize( new Vector2(48, 48));
        ItemList.SetIconMode(ItemList.IconModeEnum.Top);
        ItemList.SetSelectMode(ItemList.SelectModeEnum.Single);
        ItemList.SetSameColumnWidth(true);
        ItemList.SetAllowRmbSelect(true);
        LoadItems();
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
    public void LoadItems()
    {
        var ItemList = this.GetNode<ItemList>("Panel/ItemList");
        ItemList.Clear();
        for (int i = 0; i < GlobalPlayer.InventoryMaxSlots; i++)
        {
            ItemList.AddItem("", null, false);
            UpdateSlot(i);
        }
    }
    public void UpdateSlot(int slot)
    {
        if (slot < 0) return;
        var inventoryController = InventoryController.Default();
        var itemMetaData = inventoryController.GetInventoryItem(slot.ToString());
        var icon = ResourceLoader.Load(itemMetaData.Icon);
        var amount = itemMetaData.Amount;
        var itemList = this.GetNode<ItemList>("Panel/ItemList");
        itemMetaData.Amount = amount;
        if (itemMetaData.Stackable)
        {
            itemList.SetItemText(slot, amount.ToString());
        }
        itemList.SetItemIcon(slot, (Texture)icon);
        itemList.SetItemSelectable(slot, itemMetaData.Id > 0);
        itemList.SetItemMetadata(slot, itemMetaData);
        itemList.SetItemTooltip(slot, itemMetaData.Name);
        itemList.SetItemTooltipEnabled(slot, itemMetaData.Id > 0);
    }
}