using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerInventory : MonoBehaviour
{

  [SerializeField] private GameObject grid;
  [SerializeField] private GameObject inventoryItemPrefab;
  [SerializeField] private RiftItemStatsCanvas riftItemStatsCanvas;
  [SerializeField] private ToggleGroup toggleGroup;
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
      itemObject.GetComponent<DisplayPlayerInventoryItem>().riftItemStatsCanvas = riftItemStatsCanvas;
      itemObject.GetComponent<Toggle>().group = toggleGroup;
      itemObject.GetComponent<Toggle>().isOn = false;
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
