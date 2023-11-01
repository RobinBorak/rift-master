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


  [Header("Data")]
  [SerializeField] private int selectedItemId;
  [SerializeField] private int cost;

  private PlayerInventory playerInventory;
  private RiftItem selectedItem;


  // Start is called before the first frame update
  void OnEnable()
  {
    playerInventory = FindObjectOfType<PlayerInventory>();

    selectedItem = playerInventory.GetItem(selectedItemId);
    icon.sprite = selectedItem.icon;
    priceText.text = cost.ToString();

    ToggleButtonIsPurchasable();
  }

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

}
