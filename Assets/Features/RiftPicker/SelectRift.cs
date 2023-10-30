using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRift : MonoBehaviour
{
  [SerializeField] private int rift = 0;
  // Consider changing to buildIndexes
  List<string> mapNames = new List<string>{
    "level 1 (Sandstone)",
    "level 2 (RPGW_Cave)"
  };
  private CurrentRift currentRift;

  private void Start()
  {
    currentRift = FindObjectOfType<CurrentRift>();
  }
  //onclick
  public void LoadRift()
  {
    Debug.Log("Rift " + rift + " started");
    currentRift.currentRift = rift;
    LoadMap();
  }

  private void LoadMap()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(GetRandomMapName());
  }

  private string GetRandomMapName()
  {
    return mapNames[Random.Range(0, mapNames.Count)];
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      //Based on last rift, load next map
      Debug.Log("Continuing to next map for rift " + rift);
      LoadMap();
    }
  }

  public void SetRift(int rift)
  {
    this.rift = rift;
  }
}
