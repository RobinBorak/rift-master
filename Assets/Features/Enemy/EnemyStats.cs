using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStats : MonoBehaviour
{
  public float maxHealth = 2f;
  public float movementSpeed = 0.5f;
  public float maxDistanceToTargetToMove = 10f;

  [Header("Combat")]
  public float attackSpeed = 1.5f;
  public float attackRange = 1.5f;

  [NonSerialized] public int exp = 5;

  public bool isDoneLoading = false;

  // Scale stats before Start() is called in other scripts
  IEnumerator Start()
  {
    CurrentRiftLogic currentRiftLogic = FindObjectOfType<CurrentRiftLogic>();

    while (!currentRiftLogic.isDoneLoading)
    {
      yield return null;
    }

    if (currentRiftLogic)
      ScaleWithRift(currentRiftLogic.rift);

    isDoneLoading = true;
  }

  private void ScaleWithRift(int riftLevel = 1)
  {
    maxHealth *= riftLevel * 0.6f;
    exp += (int)(riftLevel * 2f);
  }

}
