using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedInventory
{
  public int gold = 0;

  public SerializedInventory(
    int gold
  )
  {
    this.gold = gold;
  }

  public SerializedInventory()
  {
  }
}
