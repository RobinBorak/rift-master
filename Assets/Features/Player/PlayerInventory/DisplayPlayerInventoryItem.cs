using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayPlayerInventoryItem : MonoBehaviour
{
  [SerializeField] private Image icon;
  [SerializeField] private Text quantity;
  public RiftItem item;
  // Start is called before the first frame update
  void Start()
  {
    icon.sprite = item.icon;
    if (item.stackable)
    {
      quantity.text = item.quantity.ToString();
    }
    else
    {
      quantity.enabled = false;
    }

    if (item.isEquippable)
      InitEquippableEvent();
  }

  private void InitEquippableEvent()
  {
    EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    pointerDownEntry.callback.AddListener(equipItem);
    trigger.triggers.Add(pointerDownEntry);
  }

  private void equipItem(BaseEventData data)
  {
    PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();
    playerEquipment.Equip(item);
  }


}
