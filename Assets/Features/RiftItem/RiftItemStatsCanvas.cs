using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RiftItemStatsCanvas : MonoBehaviour
{
  private RiftItem item;
  [SerializeField] private TextMeshProUGUI row1;
  [SerializeField] private TextMeshProUGUI row2;
  [SerializeField] private TextMeshProUGUI description;
  [SerializeField] private Button equipButton;


  public void SetItem(RiftItem item)
  {
    this.item = item;
    emptyTexts();

    if (item.armor > 0)
      row1.text = "Armor: " + item.armor.ToString();

    if (item.description != "")
      description.text = item.description;

    if (equipButton != null)
    {
      equipButton.gameObject.SetActive(item.isEquippable);
      EquipButton.onEquipItemDelegate += ClearItem;
    }


  }

  private void emptyTexts()
  {
    row1.text = "";
    row2.text = "";
    description.text = "";
  }

  private void ClearItem()
  {
    item = null;
    emptyTexts();
    if (equipButton != null)
    {
      equipButton.gameObject.SetActive(false);
      EquipButton.onEquipItemDelegate -= ClearItem;
    }
  }

  public void EquipItem()
  {
    PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();
    playerEquipment.Equip(item);
  }
}
