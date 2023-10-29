using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LootItem : ScriptableObject
{
  public string Name;
  public Sprite Sprite;
  public int Quantity = 1;
}
