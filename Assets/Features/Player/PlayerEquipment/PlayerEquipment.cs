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

  public delegate void OnPlayerEquipmentChangeDelegate();
  public static event OnPlayerEquipmentChangeDelegate playerEquipmentChangeDelegate;

  // Start is called before the first frame update
  void Start()
  {

    playerInventory = FindObjectOfType<PlayerInventory>();
    serializedPlayerEquipment = (SerializedPlayerEquipment)Store.Load(key);
    if (serializedPlayerEquipment == null)
    {
      serializedPlayerEquipment = new SerializedPlayerEquipment();
    }

    Invoke("EquipItemsAsync", 0);
  }

  private void EquipItemsAsync()
  {
    if (serializedPlayerEquipment.helmetId != -1)
    {
      helmet = playerInventory.GetItem(serializedPlayerEquipment.helmetId);
      Equip(helmet);
    }
  }

  public void Equip(RiftItem item)
  {
    switch (item.equipmentPart)
    {
      case EquipmentPart.Helmet:
        EquipHelmet(item);
        break;
    }

    PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
    playerInventory.RemoveItem(new PlayerInventoryItem(item));

    playerEquipmentChangeDelegate?.Invoke();
    Save();
  }

  public void UnEquip(RiftItem item)
  {
    switch (item.equipmentPart)
    {
      case EquipmentPart.Helmet:
        UnEquipHelmet(item);
        break;
    }

    PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
    playerInventory.AddItem(new PlayerInventoryItem(item));
    playerEquipmentChangeDelegate?.Invoke();
    Save();
  }

  private void EquipHelmet(RiftItem item)
  {
    Character4D character = gameObject.GetComponent<Player>().TestGetCharacter4D();
    var character4dItem = character.SpriteCollection.Armor.Find(i => i.Id == item.character4dId);
    character.Equip(character4dItem, EquipmentPart.Helmet);
    helmet = item;
  }

  private void UnEquipHelmet(RiftItem item)
  {
    Character4D character = gameObject.GetComponent<Player>().TestGetCharacter4D();
    character.UnEquip(EquipmentPart.Helmet);
    helmet = null;
  }

  public int GetArmor()
  {
    int armor = 0;
    if (helmet != null)
    {
      armor += helmet.armor;
    }
    return armor;
  }

  private void Save()
  {
    Store.Save(key, new SerializedPlayerEquipment(
      helmet != null ? helmet.id : -1
    ));
  }
}
