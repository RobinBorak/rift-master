using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
  public List<int> lootItemIds = new List<int>();
  private List<PlayerInventoryItem> lootItems = new List<PlayerInventoryItem>();
  public List<int> lootItemChances = new List<int>();
  public List<int> lootItemMaxQuantities = new List<int>();

  public List<PlayerInventoryItem> GetLoot()
  {
    RiftItems allRiftItems = FindObjectOfType<PlayerInventory>().AllRiftItems;
    List<PlayerInventoryItem> loot = new List<PlayerInventoryItem>();
    int rift = FindObjectOfType<CurrentRiftLogic>()?.Rift ?? 1;

    List<int>[] lootWithRiftLevel = ScaleLootWithRift(rift);
    List<int> scaledLootItemIds = lootWithRiftLevel[0];
    List<int> scaledLootItemChances = lootWithRiftLevel[1];
    List<int> scaledLootItemMaxQuantities = lootWithRiftLevel[2];

    for (int i = 0; i < scaledLootItemIds.Count; i++)
    {
      int lootItemChance = scaledLootItemChances[i];
      int chance = Random.Range(0, 100);
      if (chance < lootItemChance)
      {
        PlayerInventoryItem item = new PlayerInventoryItem(allRiftItems.GetRiftItem(scaledLootItemIds[i]));

        int maxQuantity = scaledLootItemMaxQuantities[i];
        if (maxQuantity > 1)
        {
          item.item.quantity = Random.Range(1, maxQuantity);
        }
        else
        {
          item.item.quantity = 1;
        }
        loot.Add(item);
      }
    }
    return loot;
  }

  private List<int>[] ScaleLootWithRift(int rift = 1)
  {
    if (rift < 1)
    {
      Debug.LogError("Rift level cannot be less than 1");
      rift = 1;
    }

    float scale = 1 + (rift / 10f);
    List<int> scaledLootItemChances = new List<int>();
    List<int> scaledLootItemMaxQuantities = new List<int>();
    for (int i = 0; i < lootItemChances.Count; i++)
    {
      int lootItemChance = lootItemChances[i];
      lootItemChance = Mathf.RoundToInt(lootItemChance * scale);
      scaledLootItemChances.Add(lootItemChance);
    }

    for (int i = 0; i < lootItemMaxQuantities.Count; i++)
    {
      int lootItemMaxQuantity = lootItemMaxQuantities[i];
      if (lootItemMaxQuantity > 1)
      {
        lootItemMaxQuantity = Mathf.RoundToInt(lootItemMaxQuantity * scale);
        scaledLootItemMaxQuantities.Add(lootItemMaxQuantity);
      }
      else
      {
        scaledLootItemMaxQuantities.Add(1);
      }
    }

    return new List<int>[] { lootItemIds, scaledLootItemChances, scaledLootItemMaxQuantities };

  }


}