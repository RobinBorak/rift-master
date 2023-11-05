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
  [SerializeField] private Button dropItemButton;



  public void SetItem(RiftItem item, bool isEquipped = false)
  {
    ClearItem();
    this.item = item;

    header.text = item.name;

    if (item.armor > 0)
    {
      row1.text = "Armor: " + item.armor.ToString();
    }
    else if (item.damage > 0)
    {
      row1.text = "Damage: " + item.damage.ToString();
    }

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

    if (dropItemButton != null)
    {
      dropItemButton.gameObject.SetActive(!isEquipped);
      if (!isEquipped)
        DropItemButton.onDropItemDelegate += ClearItem;
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

    if (dropItemButton != null)
      dropItemButton.gameObject.SetActive(false);

    UnEquipButton.onUnEquipItemDelegate -= ClearItem;
    EquipButton.onEquipItemDelegate -= ClearItem;
    DropItemButton.onDropItemDelegate -= ClearItem;
  }

  public void EquipItem()
  {
    PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();
    playerEquipment.Equip(item);
  }
}
