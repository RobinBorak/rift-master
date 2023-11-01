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
    Init();
    PlayerInventory.onItemChangeDelegate += Init;
  }

  void OnDisable()
  {
    PlayerInventory.onItemChangeDelegate -= Init;
  }

  void Init()
  {
    Reset();
    playerInventory = FindObjectOfType<PlayerInventory>();

    foreach (PlayerInventoryItem playerInventoryItem in playerInventory.PlayerInventoryItems)
    {
      GameObject itemObject = Instantiate(inventoryItemPrefab, transform);
      itemObject.GetComponent<DisplayPlayerInventoryItem>().item = playerInventoryItem.item;
      itemObject.transform.SetParent(grid.transform);
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
