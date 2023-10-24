using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
  public float maxHealth = 5f;
  public float movementSpeed = 3f;
  public float maxDistanceToTargetToMove = 10f;

  [Header("Combat")]
  public float attackSpeed = 1.5f;
  public float attackRange = 1.5f;

  // Scale stats before Start() is called in other scripts
  void Awake()
  {
    CurrentRiftLogic currentRiftLogic = FindObjectOfType<CurrentRiftLogic>();
    ScaleWithRift(currentRiftLogic.rift);
  }

  private void ScaleWithRift(int riftLevel = 1)
  {
    maxHealth *= riftLevel * 0.6f;
  }

}
