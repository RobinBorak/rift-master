using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
  [SerializeField] private GameObject enemyPrefab;
  [SerializeField] private int respawnAfterSeconds = 30;
  private int getNumberOfEnemiesAliveFrequencyInSeconds = 10;
  private int numberOfEnemiesAlive = 0;
  private bool isRespawning = false;

  // Start is called before the first frame update
  void Start()
  {
    RespawnEnemies();
    InvokeRepeating("UpdateNumberOfEnemiesAlive", 0, getNumberOfEnemiesAliveFrequencyInSeconds);
    InvokeRepeating("SpawnEnemiesWhenAllDead", 0, getNumberOfEnemiesAliveFrequencyInSeconds);
  }

  private void SpawnEnemiesWhenAllDead()
  {
    if (numberOfEnemiesAlive == 0 && !isRespawning)
    {
      Debug.Log("All enemies dead, respawning");
      Invoke("RespawnEnemies", respawnAfterSeconds);
      isRespawning = true;
    }
  }

  private void RespawnEnemies()
  {
    SpawnEnemies(Random.Range(1, 5));
    isRespawning = false;
  }

  private void UpdateNumberOfEnemiesAlive()
  {
    int _numberOfEnemiesAlive = 0;
    //Find enemies in this gameObject folder
    foreach (Transform child in transform.GetComponentsInChildren<Transform>())
    {
      if (child.tag == "Enemy")
      {
        _numberOfEnemiesAlive++;
      }
    }
    numberOfEnemiesAlive = _numberOfEnemiesAlive;
  }

  private void SpawnEnemy()
  {
    Vector2 spawnPosition = RandomSpawnPosition();
    // Make sure spawnPosition is on the floor
    /*
    RaycastHit2D hit = Physics2D.Raycast(spawnPosition, Vector2.down, 10f, LayerMask.GetMask("Floor"));
    if (hit.collider == null)
    {
      Debug.Log("No floor found at spawn position, trying again");
      SpawnEnemy();
      return;
    }
    */

    //Instantiate enemy in this gameObject folder
    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    enemy.transform.parent = gameObject.transform;
  }

  private Vector2 RandomSpawnPosition()
  {
    return new Vector2(
      Random.Range(transform.position.x - 3, transform.position.x + 3),
      Random.Range(transform.position.y - 3, transform.position.y + 3)
    );
  }

  private void SpawnEnemies(int numberOfEnemies)
  {
    for (int i = 0; i < numberOfEnemies; i++)
    {
      SpawnEnemy();
    }
  }
}
