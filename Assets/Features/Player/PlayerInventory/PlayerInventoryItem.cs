using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryItem
{
  public RiftItem item;

  public PlayerInventoryItem(RiftItem item)
  {
    this.item = item;
  }
  public PlayerInventoryItem(RiftItem item, int quantity)
  {
    this.item = item;
    this.item.quantity = quantity;
  }
}