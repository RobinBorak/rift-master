using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnEquipButton : MonoBehaviour
{
  public RiftItem item;

  public delegate void OnUnEquipItemDelegate();
  public static event OnUnEquipItemDelegate onUnEquipItemDelegate;

  public void UnEquipSelectedItem()
  {
    PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();
    playerEquipment.UnEquip(item);
    onUnEquipItemDelegate?.Invoke();
  }
}
