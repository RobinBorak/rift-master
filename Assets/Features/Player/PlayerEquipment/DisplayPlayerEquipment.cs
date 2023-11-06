using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.HeroEditor4D.Common.Scripts.Enums;


public class DisplayPlayerEquipment : MonoBehaviour
{

  [SerializeField] private Image helmet;
  [SerializeField] private GameObject helmetContainer;
  [SerializeField] private Image armor;
  [SerializeField] private GameObject armorContainer;
  [SerializeField] private Image meleeWeapon1H;
  [SerializeField] private GameObject meleeWeapon1HContainer;
  [SerializeField] private Image shield;
  [SerializeField] private GameObject shieldContainer;
  [SerializeField] private RiftItemStatsCanvas riftItemStatsCanvas;
  [SerializeField] private UnEquipButton unEquipButton;

  private PlayerEquipment playerEquipment;

  // Start is called before the first frame update
  void Start()
  {
    playerEquipment = FindObjectOfType<PlayerEquipment>();
    PlayerEquipment.playerEquipmentChangeDelegate += UpdateEquipment;
    UpdateEquipment();

    InitSelectItemEvent(helmetContainer, EquipmentPart.Helmet);
    InitSelectItemEvent(armorContainer, EquipmentPart.Armor);
    InitSelectItemEvent(meleeWeapon1HContainer, EquipmentPart.MeleeWeapon1H);
    InitSelectItemEvent(shieldContainer, EquipmentPart.Shield);
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


  private void InitSelectItemEvent(GameObject container, EquipmentPart equipmentPart)
  {
    EventTrigger trigger = container.GetComponent<EventTrigger>();
    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    pointerDownEntry.callback.AddListener((data) => { SelectItem(equipmentPart); });
    trigger.triggers.Add(pointerDownEntry);
  }

  private void SelectItem(EquipmentPart equipmentPart)
  {
    RiftItem item;

    switch (equipmentPart)
    {
      case EquipmentPart.Helmet:
        item = playerEquipment.helmet;
        break;
      case EquipmentPart.Armor:
        item = playerEquipment.armor;
        break;
      case EquipmentPart.MeleeWeapon1H:
        item = playerEquipment.meleeWeapon1H;
        break;
      case EquipmentPart.Shield:
        item = playerEquipment.shield;
        break;
      default:
        item = null;
        break;
    }

    ShowStats(item);
    ToggleUnEquipButton(item);
  }
  private void ShowStats(RiftItem item)
  {
    riftItemStatsCanvas.SetItem(item, true);
  }

  private void ToggleUnEquipButton(RiftItem item)
  {
    if (unEquipButton != null)
    {
      unEquipButton.item = item;
    }
  }

}
