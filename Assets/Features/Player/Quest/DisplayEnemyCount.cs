using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayEnemyCount : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI text;
  private EnemyCount enemyCount;
  // Start is called before the first frame update
  void Start()
  {
    enemyCount = FindObjectOfType<EnemyCount>();
    UpdateEnemyCount();
    EnemyCount.onEnemyDeathCountChange += UpdateEnemyCount;
  }

  private void UpdateEnemyCount()
  {
    int enemyKilled = enemyCount.enemyKilled;
    int killsToComplete = enemyCount.killsToComplete;
    text.text = enemyKilled + " / " + killsToComplete;
  }

}
