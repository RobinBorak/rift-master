using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayPlayerInventoryItem : MonoBehaviour
{
  [SerializeField] private Image icon;
  [SerializeField] private Text quantity;
  [SerializeField] public bool showCount = true;
  public RiftItemStatsCanvas riftItemStatsCanvas;
  public EquipButton equipButton;
  public RiftItem item;

  // Start is called before the first frame update
  // Must be Start() instead of OnEnable() because OnEnable() is called before the item is set
  void Start()
  {
    icon.sprite = item.icon;
    if (item.stackable && showCount)
    {
      quantity.text = item.quantity.ToString();
    }
    else
    {
      quantity.enabled = false;
    }

    InitSelectItemEvent();
  }

  private void InitSelectItemEvent()
  {
    EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    pointerDownEntry.callback.AddListener((data) => { SelectItem(); });
    trigger.triggers.Add(pointerDownEntry);
  }

  private void SelectItem()
  {
    ShowStats();
    ToggleEquipButton();
  }
  private void ShowStats()
  {
    riftItemStatsCanvas.SetItem(item);
  }
  private void ToggleEquipButton()
  {
    if (equipButton != null)
    {
      equipButton.gameObject.SetActive(item.isEquippable);
      equipButton.item = item;
    }
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
