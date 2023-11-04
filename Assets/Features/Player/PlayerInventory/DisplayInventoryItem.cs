using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayInventoryItem : MonoBehaviour
{
  [SerializeField] private Image icon;
  [SerializeField] private Text quantity;
  [SerializeField] public bool showCount = true;
  public RiftItem item;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("DisplayInventoryItem.item: " + item.name);
    icon.sprite = item.icon;
    if (item.stackable && showCount)
    {
      quantity.text = item.quantity.ToString();
    }
    else
    {
      quantity.enabled = false;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
