using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayerStats
{
  public float maxHealth = 5f;
  public float movementSpeed = 3f;

  public float attackSpeed = 1f;
  public float attackRange = 1f;

  public int level = 2;
  public int currentExp = 50;

  public SerializedPlayerStats(
    float maxHealth,
    float movementSpeed,
    float attackSpeed,
    float attackRange,
    int level,
    int currentExp
  )
  {
    this.maxHealth = maxHealth;
    this.movementSpeed = movementSpeed;
    this.attackSpeed = attackSpeed;
    this.attackRange = attackRange;
    this.level = level;
    this.currentExp = currentExp;
  }

  public SerializedPlayerStats()
  {
  }

}
