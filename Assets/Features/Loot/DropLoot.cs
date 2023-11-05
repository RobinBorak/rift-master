using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
  [SerializeField] private LootTable lootTable;
  [SerializeField] private GameObject lootPrefab;
  private Transform lootSpawnPoint;
  private float lootSpawnRadius = 4f;

  private void Start()
  {
    //lootPrefab = Resources.Load<GameObject>("Features/Loot/LootPrefab");
    lootSpawnPoint = gameObject.transform;
    if (gameObject.CompareTag("Enemy"))
    {
      Enemy enemy = gameObject.GetComponent<Enemy>();
      enemy.onDeathDelegate += DropLootOnDeath;
    }
    else if (gameObject.CompareTag("Chest"))
    {
      Chest chest = gameObject.GetComponent<Chest>();
      chest.onChestOpenDelegate += DropLootOnDeath;
    }
  }

  private void DropLootOnDeath()
  {
    List<PlayerInventoryItem> loot = lootTable.GetLoot();
    foreach (PlayerInventoryItem item in loot)
    {
      Vector3 position = GetRandomPosition();

      GameObject lootObject = Instantiate(lootPrefab, position, Quaternion.identity);
      lootObject.GetComponentInChildren<SpriteRenderer>().sprite = item.item.icon;
      lootObject.GetComponent<Loot>().Item = item;
    }
  }

  private Vector3 GetRandomPosition()
  {
    Vector3 randomPosition = Random.insideUnitSphere * lootSpawnRadius;
    randomPosition += lootSpawnPoint.position;
    randomPosition.y = lootSpawnPoint.position.y;

    //Make sure randomPosition is on the grid Floor

    return randomPosition;
  }

}
