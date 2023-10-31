using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerInventoryItem : MonoBehaviour
{
  [SerializeField] private Image icon;
  [SerializeField] private Text quantity;
  public RiftItem item;
  // Start is called before the first frame update
  void Start()
  {
    icon.sprite = item.icon;
    quantity.text = item.quantity.ToString();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
