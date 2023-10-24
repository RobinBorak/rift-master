using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRiftsStats
{
  private static string key = "PlayerRiftsStats";
  private static List<RiftStats> riftsStats = new List<RiftStats>();
  public static bool isLoaded = false;

  private void Awake()
  {
    riftsStats = LoadRiftsStats();
  }

  public static void SaveRiftsStats()
  {
    Store.Save(key, riftsStats);
  }

  private static List<RiftStats> LoadRiftsStats()
  {
    List<RiftStats> data = (List<RiftStats>)Store.Load(key);
    if (data == null)
    {
      data = new List<RiftStats>();
    }
    isLoaded = true;
    return data;
  }

  public static void CreateOrUpdate(RiftStats riftStats)
  {
    if (riftsStats == null)
    {
      riftsStats = new List<RiftStats>();
    }

    if (riftsStats.Count == 0)
    {
      riftsStats.Add(riftStats);
    }
    else
    {
      bool found = false;
      for (int i = 0; i < riftsStats.Count; i++)
      {
        if (riftsStats[i].rift == riftStats.rift)
        {
          riftsStats[i] = riftStats;
          found = true;
          break;
        }
      }

      if (!found)
      {
        riftsStats.Add(riftStats);
      }
    }
  }


  public static RiftStats Get(int rift)
  {
    if (!isLoaded)
    {
      riftsStats = LoadRiftsStats();
    }
    return riftsStats[rift];
  }

  public static List<RiftStats> Get()
  {
    if (!isLoaded)
    {
      riftsStats = LoadRiftsStats();
    }
    return riftsStats;
  }
}
