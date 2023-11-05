using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RiftItemStatsCanvas : MonoBehaviour
{
  private RiftItem item;
  [SerializeField] private TextMeshProUGUI header;
  [SerializeField] private TextMeshProUGUI row1;
  [SerializeField] private TextMeshProUGUI row2;
  [SerializeField] private TextMeshProUGUI description;
  [SerializeField] private Button equipButton;
  [SerializeField] private Button unEquipButton;



  public void SetItem(RiftItem item, bool isEquipped = false)
  {
    this.item = item;
    emptyTexts();

    header.text = item.name;

    if (item.armor > 0)
      row1.text = "Armor: " + item.armor.ToString();

    if (item.description != "")
      description.text = item.description;

    if (equipButton != null)
    {
      equipButton.gameObject.SetActive(item.isEquippable);
      if (item.isEquippable)
        EquipButton.onEquipItemDelegate += ClearItem;
    }

    if (unEquipButton != null)
    {
      unEquipButton.gameObject.SetActive(isEquipped);
      if (isEquipped)
        UnEquipButton.onUnEquipItemDelegate += ClearItem;
    }

  }

  private void emptyTexts()
  {
    header.text = "";
    row1.text = "";
    row2.text = "";
    description.text = "";
  }

  private void ClearItem()
  {
    item = null;
    emptyTexts();
    if (equipButton != null)
      equipButton.gameObject.SetActive(false);

    if (unEquipButton != null)
      unEquipButton.gameObject.SetActive(false);

    UnEquipButton.onUnEquipItemDelegate -= ClearItem;
    EquipButton.onEquipItemDelegate -= ClearItem;
  }

  public void EquipItem()
  {
    PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();
    playerEquipment.Equip(item);
  }
}
