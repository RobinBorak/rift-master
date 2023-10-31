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
      int gold = collision.gameObject.GetComponent<Loot>().Item.item.quantity;
      FindObjectOfType<PlayerInventory>().AddGold(gold);
      Destroy(collision.gameObject);
    }
    else if (collision.gameObject.CompareTag("Loot"))
    {
      PlayerInventoryItem item = collision.gameObject.GetComponent<Loot>().Item;
      PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
      playerInventory.AddItem(item);
      Destroy(collision.gameObject);
    }
  }

}
