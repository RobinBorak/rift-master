using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
  public RiftItem item;

  public delegate void OnEquipItemDelegate();
  public static event OnEquipItemDelegate onEquipItemDelegate;

  public void EquipSelectedItem()
  {
    PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();
    playerEquipment.Equip(item);
    onEquipItemDelegate?.Invoke();
  }
}
