using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayerStats
{
  public int level = 2;
  public int currentExp = 50;

  public SerializedPlayerStats(
    int level,
    int currentExp
  )
  {
    this.level = level;
    this.currentExp = currentExp;
  }

  public SerializedPlayerStats()
  {
  }

}
