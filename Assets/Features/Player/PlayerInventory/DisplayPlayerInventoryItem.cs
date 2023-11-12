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
  }
  private void ShowStats()
  {
    riftItemStatsCanvas.SetItem(item);
  }

}
