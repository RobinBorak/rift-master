using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RiftStats
{
  public int rift = 0;
  public bool completed = false;
  public int completionTime = 0;

  public RiftStats(int rift, bool completed, int completionTime)
  {
    this.rift = rift;
    this.completed = completed;
    this.completionTime = completionTime;
  }

  public RiftStats(int rift)
  {
    this.rift = rift;
    this.completed = false;
  }

}
