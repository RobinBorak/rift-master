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

  private RiftItem item;
  private PlayerInventory playerInventory;
  private Player player;
  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<Player>();
    playerInventory = FindObjectOfType<PlayerInventory>();
    item = playerInventory.GetInventoryItem(itemId);
    icon.GetComponent<Image>().sprite = item.icon;
    count.text = item.quantity.ToString();
    InitButton();

    PlayerInventory.onItemChangeDelegate += UpdateItem;
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
    Debug.Log("UseItem :" + item.name);
    if(item.quantity <= 0) return;
    player.Heal(3f);
    playerInventory.RemoveItem(item.id, 1);
  }

  private void UpdateItem()
  {
    item = playerInventory.GetInventoryItem(itemId);
    count.text = item.quantity.ToString();
  }

}
