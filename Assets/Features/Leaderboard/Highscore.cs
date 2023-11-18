using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using Dan.Models;

public static class Highscore
{
  private static string publicKey = "e7675c72c8c01da459e42c2b159ba1b437cdcd3f871f221d0ac0849411d62ee8";
  private static Entry[] entries;
  private static float lastFetched = 0f;
  private static float cacheTime = 60f * 10f;
  private static bool bypassNextCache = false;

  public static void UploadNewEntry(string username, int rift)
  {
    Debug.Log("Uploading new entry for " + username + " with score " + rift + "...");
    LeaderboardCreator.UploadNewEntry(publicKey, username, rift);
    bypassNextCache = true;
  }

  public static void LoadEntries(Action<Entry[]> callback)
  {
    LeaderboardCreator.GetLeaderboard(publicKey, (Entry[] _entries) =>
    {
      entries = _entries;
      lastFetched = Time.time;
      callback(entries);
    });
  }

  /*
  * This is the method that should be called to get the entries.
  * It will return the cached entries if they exist, otherwise it will fetch them.
  * Use Highscore.GetEntries( (Entry[] entries) => { ... } ) to get the entries.
  **/
  public static void GetEntries(Action<Entry[]> callback)
  {
    if (entries == null || entries.Length == 0 || bypassNextCache || Time.time - lastFetched > cacheTime)
    {
      LoadEntries(callback);
      bypassNextCache = false;
    }
    else
    {
      callback(entries);
    }
  }

}
