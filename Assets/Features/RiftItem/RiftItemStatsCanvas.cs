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
  [SerializeField] private Button useButton;



  public void SetItem(RiftItem item, bool isEquipped = false)
  {
    ClearItem();
    this.item = item;

    if (item == null)
      return;

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
      equipButton.GetComponent<EquipButton>().item = item;
      if (item.isEquippable)
        EquipButton.onEquipItemDelegate += ClearItem;
    }

    if (unEquipButton != null)
    {
      unEquipButton.gameObject.SetActive(isEquipped);
      unEquipButton.GetComponent<UnEquipButton>().item = item;
      if (isEquipped)
        UnEquipButton.onUnEquipItemDelegate += ClearItem;
    }

    if (dropItemButton != null)
    {
      dropItemButton.gameObject.SetActive(!isEquipped);
      dropItemButton.GetComponent<DropItemButton>().item = item;
      if (!isEquipped)
        DropItemButton.onDropItemDelegate += ClearItem;
    }

    if (useButton != null)
    {
      useButton.gameObject.SetActive(item.isUsable);
      useButton.GetComponent<UseButton>().item = item;
      if (item.isUsable)
        UseButton.onUseItemDelegate += ClearItem;
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

    if (useButton != null)
      useButton.gameObject.SetActive(false);

    UnEquipButton.onUnEquipItemDelegate -= ClearItem;
    EquipButton.onEquipItemDelegate -= ClearItem;
    DropItemButton.onDropItemDelegate -= ClearItem;
    UseButton.onUseItemDelegate -= ClearItem;
  }

  public void EquipItem()
  {
    PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();
    playerEquipment.Equip(item);
  }
}
