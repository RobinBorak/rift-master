using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RiftItems", menuName = "RiftItems/RiftItems")]
public class RiftItems : ScriptableObject
{
  public List<RiftItem> riftItems = new List<RiftItem>();

  public RiftItem GetRiftItem(int id)
  {
    foreach (RiftItem item in riftItems)
    {
      if (item.id == id)
      {
        return item;
      }
    }
    return null;
  }

  public RiftItem GetRiftItem(string name)
  {
    foreach (RiftItem item in riftItems)
    {
      if (item.name == name)
      {
        return item;
      }
    }
    return null;
  }
}
