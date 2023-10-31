using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
  public List<int> lootItemIds = new List<int>();
  private List<PlayerInventoryItem> lootItems = new List<PlayerInventoryItem>();
  public List<int> lootItemChances = new List<int>();
  public List<int> lootItemMaxQuantity = new List<int>();


  public List<PlayerInventoryItem> GetLoot()
  {
    RiftItems allRiftItems = FindObjectOfType<PlayerInventory>().AllRiftItems;
    List<PlayerInventoryItem> loot = new List<PlayerInventoryItem>();
    for (int i = 0; i < lootItemIds.Count; i++)
    {
      int chance = Random.Range(0, 100);
      if (chance < lootItemChances[i])
      {
        PlayerInventoryItem item = new PlayerInventoryItem(allRiftItems.GetRiftItem(lootItemIds[i]));
        if (lootItemMaxQuantity[i] > 1){
          item.item.quantity = Random.Range(1, lootItemMaxQuantity[i]);
        }else{
          item.item.quantity = 1;
        }
        loot.Add(item);
      }
    }
    return loot;
  }
}