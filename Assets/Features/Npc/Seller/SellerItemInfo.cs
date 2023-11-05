using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellerItemInfo : MonoBehaviour
{

  [Header("UI")]
  [SerializeField] private Image icon;
  [SerializeField] private TextMeshProUGUI priceText;
  [SerializeField] private Button buyButton;
  [SerializeField] private RiftItemStatsCanvas riftItemStatsCanvas;

  private int cost;

  private PlayerInventory playerInventory;
  private RiftItem selectedItem;

  public void BuyItem()
  {
    if (playerInventory.Gold < cost) return;

    playerInventory.AddItem(new PlayerInventoryItem(selectedItem, 1));
    playerInventory.AddGold(-cost);
    ToggleButtonIsPurchasable();
  }

  public void ToggleButtonIsPurchasable()
  {
    if (playerInventory.Gold < cost)
    {
      buyButton.interactable = false;
    }
    else
    {
      buyButton.interactable = true;
    }
  }

  public void SetItem(RiftItem item, int cost)
  {
    playerInventory = FindObjectOfType<PlayerInventory>();

    this.cost = cost;
    selectedItem = item;
    icon.sprite = selectedItem.icon;
    priceText.text = cost.ToString();
    riftItemStatsCanvas.SetItem(item);

    ToggleButtonIsPurchasable();
  }

}
