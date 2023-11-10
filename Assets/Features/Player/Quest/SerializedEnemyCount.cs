using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedEnemyCount
{
  public int enemyKilled = 0;
  public int completed = 0;

  public SerializedEnemyCount(
    int enemyKilled,
    int completed
  )
  {
    this.enemyKilled = enemyKilled;
    this.completed = completed;
  }

  public SerializedEnemyCount()
  {
  }
}
