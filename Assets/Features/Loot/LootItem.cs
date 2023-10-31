using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Deprecated
[CreateAssetMenu]
public class LootItem : ScriptableObject
{
  public RiftItem Item;
  public int Quantity = 1;
}
