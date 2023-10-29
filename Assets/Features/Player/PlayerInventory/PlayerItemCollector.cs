using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;
using Assets.HeroEditor4D.InventorySystem.Scripts.Data;
using System.Linq;

public class PlayerItemCollector : MonoBehaviour
{

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Gold"))
    {
      int gold = collision.gameObject.GetComponent<Loot>().Item.Quantity;
      FindObjectOfType<PlayerInventory>().AddGold(gold);
      Destroy(collision.gameObject);
    }
    else if (collision.gameObject.CompareTag("Loot"))
    {
      // Only Helmet exists for now for testing purpose
      Character4D character = gameObject.GetComponent<Player>().TestGetCharacter4D();
      /*
            foreach (var itemx in character.SpriteCollection.Armor)
            {
              Debug.Log(itemx.Id + " " + itemx.Name);
            }
      */
      var item = character.SpriteCollection.Armor[22];
      character.Equip(item, EquipmentPart.Helmet);
      Destroy(collision.gameObject);
    }
  }

}
