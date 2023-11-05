using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.Enums;

[CreateAssetMenu(fileName = "New RiftItem", menuName = "RiftItems/RiftItem")]
public class RiftItem : ScriptableObject
{
  public int id;
  public new string name = "";
  public string description = "";
  public Sprite icon;
  public bool stackable = false;
  public int quantity = 1;
  public int armor = 0;
  public string character4dId = "";
  public bool isEquippable = false;
  public EquipmentPart equipmentPart;

  public RiftItem(
    int id, 
    string name, 
    string description,
    Sprite icon, 
    bool stackable, 
    int quantity,
    int armor,
    string character4dId,
    bool isEquippable,
    EquipmentPart equipmentPart
  )
  {
    this.id = id;
    this.name = name;
    this.description = description;
    this.icon = icon;
    this.stackable = stackable;
    this.quantity = quantity;
    this.armor = armor;
    this.character4dId = character4dId;
    this.isEquippable = isEquippable;
    this.equipmentPart = equipmentPart;
  }

  public void Init(RiftItem item)
  {
    this.id = item.id;
    this.name = item.name;
    this.description = item.description;
    this.icon = item.icon;
    this.stackable = item.stackable;
    this.quantity = item.quantity;
    this.armor = item.armor;
    this.character4dId = item.character4dId;
    this.isEquippable = item.isEquippable;
    this.equipmentPart = item.equipmentPart;
  }

  public RiftItem()
  {
    this.id = -1;
  }
}
