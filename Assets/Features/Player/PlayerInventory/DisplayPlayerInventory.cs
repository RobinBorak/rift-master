using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerInventory : MonoBehaviour
{

  [SerializeField] private GameObject grid;
  [SerializeField] private GameObject inventoryItemPrefab;
  private PlayerInventory playerInventory;

  void OnEnable()
  {
    Reset();
    playerInventory = FindObjectOfType<PlayerInventory>();

    foreach (PlayerInventoryItem playerInventoryItem in playerInventory.PlayerInventoryItems)
    {
      GameObject itemObject = Instantiate(inventoryItemPrefab, transform);
      itemObject.GetComponent<DisplayPlayerInventoryItem>().item = playerInventoryItem.item;
      itemObject.transform.SetParent(grid.transform);
      Debug.Log("DisplayPlayerInventory: " + playerInventoryItem.item.name + " x" + playerInventoryItem.item.quantity);
    }
  }

  void Reset()
  {
    // Remove all children in grid
    foreach (Transform child in grid.transform)
    {
      Destroy(child.gameObject);
    }

  }
}
