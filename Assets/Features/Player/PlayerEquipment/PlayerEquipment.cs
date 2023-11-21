using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class PlayerEquipment : MonoBehaviour
{

  public RiftItem helmet;
  public RiftItem armor;
  public RiftItem meleeWeapon1H;
  public RiftItem shield;

  private string key = "playerEquipment";
  private SerializedPlayerEquipment serializedPlayerEquipment;
  private PlayerInventory playerInventory;
  private Character4D character;

  public delegate void OnPlayerEquipmentChangeDelegate();
  public static event OnPlayerEquipmentChangeDelegate playerEquipmentChangeDelegate;

  // Start is called before the first frame update
  void Start()
  {
    character = gameObject.GetComponent<Player>().TestGetCharacter4D();
    playerInventory = FindObjectOfType<PlayerInventory>();
    serializedPlayerEquipment = (SerializedPlayerEquipment)Store.Load(key);
    if (serializedPlayerEquipment == null)
    {
      //Default equipment
      Debug.Log("Default equipment");
      serializedPlayerEquipment = new SerializedPlayerEquipment(
        -1,
         9,
        6
      );
    }

    Invoke("LoadItems", 0);
  }

  private void LoadItems()
  {
    if (serializedPlayerEquipment.helmetId != -1)
    {
      helmet = playerInventory.GetItem(serializedPlayerEquipment.helmetId);
      Equip(helmet, true);
    }

    if (serializedPlayerEquipment.armorId != -1)
    {
      armor = playerInventory.GetItem(serializedPlayerEquipment.armorId);
      Equip(armor, true);
    }

    if (serializedPlayerEquipment.meleeWeapon1HId != -1)
    {
      meleeWeapon1H = playerInventory.GetItem(serializedPlayerEquipment.meleeWeapon1HId);
      Equip(meleeWeapon1H, true);
    }
    UpdateEquipment();
    Save();
  }

  // manualEquip is used and set to false when loading the game
  public void Equip(RiftItem item, bool loadItems = false)
  {
    switch (item.equipmentPart)
    {
      case EquipmentPart.Helmet:
        if (!loadItems)
          UnEquip(helmet);
        EquipHelmet(item);
        break;
      case EquipmentPart.Armor:
        if (!loadItems)
          UnEquip(armor);
        EquipArmor(item);
        break;
      case EquipmentPart.MeleeWeapon1H:
        if (!loadItems)
          UnEquip(meleeWeapon1H);
        EquipMeleeWeapon1H(item);
        break;
    }

    if (!loadItems)
    {
      // Character4D seems to be caching equipment, so we need to update it manually
      UpdateEquipment();

      PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
      playerInventory.RemoveItem(new PlayerInventoryItem(item));

      playerEquipmentChangeDelegate?.Invoke();
      Save();
    }
  }

  private void UpdateEquipment()
  {
    character = gameObject.GetComponent<Player>().TestGetCharacter4D();
    if (helmet == null)
      character.UnEquip(EquipmentPart.Helmet);

    if (armor == null)
      character.UnEquip(EquipmentPart.Armor);

    if (meleeWeapon1H == null)
      character.UnEquip(EquipmentPart.MeleeWeapon1H);

  }

  public void UnEquip(RiftItem item)
  {
    if (item == null)
      return;

    character = gameObject.GetComponent<Player>().TestGetCharacter4D();

    switch (item.equipmentPart)
    {
      case EquipmentPart.Helmet:
        character.UnEquip(EquipmentPart.Helmet);
        helmet = null;
        break;
      case EquipmentPart.Armor:
        character.UnEquip(EquipmentPart.Armor);
        armor = null;
        break;
      case EquipmentPart.MeleeWeapon1H:
        character.UnEquip(EquipmentPart.MeleeWeapon1H);
        meleeWeapon1H = null;
        break;
      default:
        Debug.LogError("UnEquip: equipmentPart not found");
        break;
    }

    PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
    playerInventory.AddItem(new PlayerInventoryItem(item));
    playerEquipmentChangeDelegate?.Invoke();
    Save();
  }

  private void EquipHelmet(RiftItem item)
  {
    character = gameObject.GetComponent<Player>().TestGetCharacter4D();
    var character4dItem = character.SpriteCollection.Armor.Find(i => i.Id == item.character4dId);
    character.Equip(character4dItem, EquipmentPart.Helmet);
    helmet = item;
  }

  private void EquipArmor(RiftItem item)
  {
    character = gameObject.GetComponent<Player>().TestGetCharacter4D();
    var character4dItem = character.SpriteCollection.Armor.Find(i => i.Id == item.character4dId);
    character.Equip(character4dItem, EquipmentPart.Armor);
    armor = item;
  }

  private void EquipMeleeWeapon1H(RiftItem item)
  {
    character = gameObject.GetComponent<Player>().TestGetCharacter4D();
    var character4dItem = character.SpriteCollection.MeleeWeapon1H.Find(i => i.Id == item.character4dId);
    character.Equip(character4dItem, EquipmentPart.MeleeWeapon1H);
    meleeWeapon1H = item;
  }

  public int GetArmor()
  {
    int totalArmor = 0;
    if (helmet != null)
    {
      totalArmor += helmet.armor;
    }
    if (armor != null)
    {
      totalArmor += armor.armor;
    }

    return totalArmor;
  }

  public int GetDamage()
  {
    int totalDamage = 0;
    if (meleeWeapon1H != null)
    {
      totalDamage += meleeWeapon1H.damage;
    }

    return totalDamage;
  }

  private void Save()
  {
    Store.Save(key, new SerializedPlayerEquipment(
      helmet != null ? helmet.id : -1,
      armor != null ? armor.id : -1,
      meleeWeapon1H != null ? meleeWeapon1H.id : -1
    ));
  }
}
