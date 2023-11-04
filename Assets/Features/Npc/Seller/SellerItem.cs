using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SellerItem : MonoBehaviour
{
  [SerializeField] private Image icon;
  [SerializeField] private Text quantity;
  [SerializeField] public bool showCount = false;
  [SerializeField] private bool displayItemInfo = false;
  [SerializeField] private int cost = 0;
  public RiftItem item;

  // Start is called before the first frame update
  void OnEnable()
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

    if (displayItemInfo)
    {
      SellerItemInfo sellerItemInfo = FindObjectOfType<SellerItemInfo>();
      sellerItemInfo.SetItem(item, cost);
    }

    InitSelectItemEvent();

  }

  private void InitSelectItemEvent()
  {
    EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    pointerDownEntry.callback.AddListener(selectItem);
    trigger.triggers.Add(pointerDownEntry);
  }

  private void selectItem(BaseEventData data)
  {
    SellerItemInfo sellerItemInfo = FindObjectOfType<SellerItemInfo>();
    sellerItemInfo.SetItem(item, cost);
  }
}
