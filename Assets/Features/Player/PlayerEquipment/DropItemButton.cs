using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemButton : MonoBehaviour
{
  public RiftItem item;
  public GameObject lootPrefab;

  public delegate void DropItemDelegate();
  public static event DropItemDelegate onDropItemDelegate;

  public void DropItem()
  {
    if (item != null)
    {
      PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
      PlayerInventoryItem playerInventoryItem = new PlayerInventoryItem(item);
      Transform playerTransform = FindObjectOfType<PlayerMovement>().transform;
      Vector3 dropPosition = playerTransform.position;
      dropPosition.y -= 1.3f;
      GameObject lootObject = Instantiate(lootPrefab, dropPosition, Quaternion.identity);
      lootObject.GetComponentInChildren<SpriteRenderer>().sprite = item.icon;
      lootObject.GetComponent<Loot>().Item = playerInventoryItem;
      playerInventory.RemoveItem(playerInventoryItem);
      onDropItemDelegate?.Invoke();
    }
  }
}
