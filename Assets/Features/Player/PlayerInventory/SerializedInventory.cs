using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedInventory
{
  public List<SerializedPlayerInventoryItem> items = new List<SerializedPlayerInventoryItem>();
  public int gold = 0;

  public SerializedInventory(
    int gold,
    List<PlayerInventoryItem> playerInventoryItems
  )
  {
    this.gold = gold;
    foreach (PlayerInventoryItem item in playerInventoryItems)
    {
      this.items.Add(new SerializedPlayerInventoryItem(item.item.id, item.item.quantity));
    }
  }

  public SerializedInventory()
  {
  }
}
