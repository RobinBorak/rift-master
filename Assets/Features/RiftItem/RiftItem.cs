using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.Enums;

[CreateAssetMenu(fileName = "New RiftItem", menuName = "RiftItems/RiftItem")]
public class RiftItem : ScriptableObject
{
  public delegate void UseItemDelegate(int id);
  public static event UseItemDelegate useItemDelegate;

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
  public bool isUsable = false;
  public int damage = 0;

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
    EquipmentPart equipmentPart,
    bool isUsable,
    int damage
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
    this.isUsable = isUsable;
    this.damage = damage;
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
    this.isUsable = item.isUsable;
    this.damage = item.damage;
  }

  public RiftItem()
  {
    this.id = -1;
  }

  public void Use()
  {
    bool itemUsed = false;
    switch (id)
    {
      case 1:
        itemUsed = UseMinorHealthPotion();
        break;
      case 16:
        itemUsed = UseTalentScroll();
        break;
      default:
        throw new System.NotImplementedException();
    }
    if (itemUsed)
      useItemDelegate?.Invoke(id);
  }

  private bool UseMinorHealthPotion()
  {
    PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
    PlayerStats playerStats = FindObjectOfType<PlayerStats>();

    if (playerHealth.currentHealth >= playerStats.MaxHealth) return false;
    playerHealth.Heal(10);
    return true;
  }

  //Reset all talent points
  private bool UseTalentScroll()
  {
    Player player = FindObjectOfType<Player>();
    PlayerTalents playerTalents = FindObjectOfType<PlayerTalents>();
    if (playerTalents.availableTalentPoints == playerTalents.totalAvailableTalentPoints) return false;

    playerTalents.ResetTalentPoints();
    player.Reset();
    return true;
  }
}
