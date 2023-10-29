using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
  public List<LootItem> lootItems = new List<LootItem>();
  public List<int> lootItemChances = new List<int>();
  public List<int> lootItemMaxQuantity = new List<int>();

  public List<LootItem> GetLoot()
  {
    List<LootItem> loot = new List<LootItem>();
    for (int i = 0; i < lootItems.Count; i++)
    {
      int chance = Random.Range(0, 100);
      if (chance < lootItemChances[i])
      {
        LootItem item = lootItems[i];
        if (lootItemMaxQuantity[i] > 1)
          item.Quantity = Random.Range(1, lootItemMaxQuantity[i]);
        loot.Add(item);
      }
    }
    return loot;
  }
}