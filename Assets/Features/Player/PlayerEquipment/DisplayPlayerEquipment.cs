using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.HeroEditor4D.Common.Scripts.Enums;


public class DisplayPlayerEquipment : MonoBehaviour
{

  [SerializeField] private Image helmet;
  [SerializeField] private Image armor;
  [SerializeField] private Image meleeWeapon1H;
  [SerializeField] private Image shield;

  private PlayerEquipment playerEquipment;

  // Start is called before the first frame update
  void Start()
  {
    playerEquipment = FindObjectOfType<PlayerEquipment>();
    PlayerEquipment.playerEquipmentChangeDelegate += UpdateEquipment;
    UpdateEquipment();

    InitEquippableEvent(helmet, EquipmentPart.Helmet);
    InitEquippableEvent(armor, EquipmentPart.Armor);
    InitEquippableEvent(meleeWeapon1H, EquipmentPart.MeleeWeapon1H);
    InitEquippableEvent(shield, EquipmentPart.Shield);
  }

  private void UpdateEquipment()
  {
    UpdateHelmet();
    UpdateArmor();
    UpdateMeleeWeapon1H();
    UpdateShield();
  }

  private void UpdateHelmet()
  {
    if (playerEquipment.helmet != null)
    {
      helmet.sprite = playerEquipment.helmet.icon;
      helmet.color = new Color(1, 1, 1, 1);
    }
    else
    {
      helmet.color = new Color(1, 1, 1, 0);
      helmet.sprite = null;
    }
  }
  private void UpdateArmor()
  {
    if (playerEquipment.armor != null)
    {
      armor.sprite = playerEquipment.armor.icon;
      armor.color = new Color(1, 1, 1, 1);
    }
    else
    {
      armor.color = new Color(1, 1, 1, 0);
      armor.sprite = null;
    }
  }

  private void UpdateMeleeWeapon1H()
  {
    if (playerEquipment.meleeWeapon1H != null)
    {
      meleeWeapon1H.sprite = playerEquipment.meleeWeapon1H.icon;
      meleeWeapon1H.color = new Color(1, 1, 1, 1);
    }
    else
    {
      meleeWeapon1H.color = new Color(1, 1, 1, 0);
      meleeWeapon1H.sprite = null;
    }
  }

  private void UpdateShield()
  {
    if (playerEquipment.shield != null)
    {
      shield.sprite = playerEquipment.shield.icon;
      shield.color = new Color(1, 1, 1, 1);
    }
    else
    {
      shield.color = new Color(1, 1, 1, 0);
      shield.sprite = null;
    }
  }


  private void InitEquippableEvent(Image image, EquipmentPart equipmentPart)
  {
    EventTrigger trigger = image.GetComponent<EventTrigger>();
    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    pointerDownEntry.callback.AddListener((data) => UnEquipItem(equipmentPart));
    trigger.triggers.Add(pointerDownEntry);
  }

  private void UnEquipItem(EquipmentPart equipmentPart)
  {
    switch (equipmentPart)
    {
      case EquipmentPart.Helmet:
        playerEquipment.UnEquip(playerEquipment.helmet);
        break;
      case EquipmentPart.Armor:
        playerEquipment.UnEquip(playerEquipment.armor);
        break;
      case EquipmentPart.MeleeWeapon1H:
        playerEquipment.UnEquip(playerEquipment.meleeWeapon1H);
        break;
      case EquipmentPart.Shield:
        playerEquipment.UnEquip(playerEquipment.shield);
        break;
    }
    UpdateEquipment();
  }

}
