using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionItemButton : MonoBehaviour
{
  [SerializeField] private GameObject icon;
  [SerializeField] private Text count;
  [SerializeField] private int itemId;

  private RiftItem inventoryItem;
  private PlayerInventory playerInventory;
  private PlayerHealth playerHealth;
  private PlayerStats playerStats;
  // Start is called before the first frame update
  void Start()
  {
    playerHealth = FindObjectOfType<PlayerHealth>();
    playerInventory = FindObjectOfType<PlayerInventory>();
    playerStats = FindObjectOfType<PlayerStats>();
    inventoryItem = playerInventory.GetInventoryItem(itemId);
    RiftItem item = playerInventory.GetItem(itemId);

    icon.GetComponent<Image>().sprite = item.icon;
    UpdateItemText();
    InitButton();
    PlayerInventory.onItemChangeDelegate += UpdateItemText;
  }

  private void InitButton()
  {
    //On attackButton pointer down
    EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    pointerDownEntry.callback.AddListener(UseItem);
    trigger.triggers.Add(pointerDownEntry);
  }

  private void UseItem(BaseEventData data)
  {
    if (inventoryItem == null || inventoryItem.quantity <= 0) return;
    if (playerHealth.currentHealth >= playerStats.MaxHealth) return;
    playerHealth.Heal(10);
    playerInventory.RemoveItem(inventoryItem.id, 1);
  }

  private void UpdateItemText()
  {
    inventoryItem = playerInventory.GetInventoryItem(itemId);
    if (inventoryItem != null)
    {
      count.text = inventoryItem.quantity.ToString();
    }
    else
    {
      count.text = "0";
    }
  }

}
