using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using Dan.Models;

public class PopulateLeaderboard : MonoBehaviour
{
  [SerializeField] private GameObject rowPrefab;
  private Entry[] entries;

  public void NewTest(){
    string username = FindObjectOfType<PlayerCharacter>().Username;
    Highscore.UploadNewEntry(username, 15);
  }

  void OnEnable()
  {
    Highscore.GetEntries(Callback);
  }

  private void Clear()
  {
    foreach (Transform child in transform)
    {
      Destroy(child.gameObject);
    }
  }

  private void Callback(Entry[] entries)
  {
    Clear();
    foreach (Entry entry in entries)
    {
      GameObject row = Instantiate(rowPrefab) as GameObject;
      row.transform.SetParent(transform, false);
      row.GetComponent<DisplayHighscoreRow>().SetEntry(entry);
    }
  }
}
