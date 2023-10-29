using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
  }

}
