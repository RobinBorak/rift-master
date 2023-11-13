using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
  [SerializeField] private GameObject chestPrefab;
  public int enemyKilled = 0;
  public int killsToComplete = 50;
  private int completed = 0;
  private int expForCompletion = 50;
  private static string key = "EnemyCount";

  public delegate void OnEnemyDeathCountChange();
  public static OnEnemyDeathCountChange onEnemyDeathCountChange;


  // Start is called before the first frame update
  void Start()
  {
    SerializedEnemyCount serializedEnemyCount = (SerializedEnemyCount)Store.Load(key);
    if (serializedEnemyCount == null)
      serializedEnemyCount = new SerializedEnemyCount();

    enemyKilled = serializedEnemyCount.enemyKilled;
    completed = serializedEnemyCount.completed;
    ScaleDifficulty();
    Enemy.onAnyEnemyDeathDelegate += IncreaseEnemyKilled;
  }

  private void IncreaseEnemyKilled()
  {
    enemyKilled++;
    if (enemyKilled >= killsToComplete)
    {
      Success();
      Reset();
    }
    onEnemyDeathCountChange?.Invoke();
    Save();
  }

  private void Reset()
  {
    enemyKilled = 0;
  }

  private void Success()
  {
    FindObjectOfType<Player>().GainExp(expForCompletion);
    DropChest();
    completed++;
    ScaleDifficulty();
  }

  private void DropChest()
  {
    Vector3 position = FindObjectOfType<Player>().transform.position;
    position.y -= 2;
    GameObject chest = Instantiate(chestPrefab, position, Quaternion.identity) as GameObject;
  }

  private void ScaleDifficulty()
  {
    killsToComplete = 50 + completed * 10;
    expForCompletion = 50 + completed * 10;
  }

  public void Save()
  {
    Store.Save(key, new SerializedEnemyCount(
      enemyKilled,
      completed
    ));
  }

}
