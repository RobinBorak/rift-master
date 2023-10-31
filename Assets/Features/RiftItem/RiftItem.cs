using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RiftItem", menuName = "RiftItems/RiftItem")]
public class RiftItem : ScriptableObject
{
  public int id;
  public new string name;
  public Sprite icon;
  public bool stackable;
  public int quantity;

  public RiftItem(int id, string name, Sprite icon, bool stackable, int quantity)
  {
    this.id = id;
    this.name = name;
    this.icon = icon;
    this.stackable = stackable;
    this.quantity = quantity;
  }

  public void Init(RiftItem item)
  {
    this.id = item.id;
    this.name = item.name;
    this.icon = item.icon;
    this.stackable = item.stackable;
    this.quantity = item.quantity;
  }

  public RiftItem()
  {
    this.id = -1;
    this.name = "";
    this.icon = null;
    this.stackable = false;
    this.quantity = 0;
  }
}
