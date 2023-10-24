using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RiftStats
{
  public int rift = 0;
  public bool completed = false;

  public RiftStats(int rift, bool completed)
  {
    this.rift = rift;
    this.completed = completed;
  }

  public RiftStats(int rift)
  {
    this.rift = rift;
    this.completed = false;
  }

}
