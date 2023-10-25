using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayerStats
{
  public float maxHealth = 5f;
  public float movementSpeed = 5f;

  public float attackSpeed = 1f;
  public float attackRange = 1f;

  public int level = 1;
  public int currentExp = 0;

  public SerializedPlayerStats(PlayerStats playerStats)
  {
    maxHealth = playerStats.maxHealth;
    movementSpeed = playerStats.movementSpeed;
    attackSpeed = playerStats.attackSpeed;
    attackRange = playerStats.attackRange;
    level = playerStats.level;
    currentExp = playerStats.currentExp;
  }

}
