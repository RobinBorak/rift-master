using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayerInventoryItem
{
  public int id;
  public int quantity;

  public SerializedPlayerInventoryItem(int id, int quantity)
  {
    this.id = id;
    this.quantity = quantity;
  }
}