using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRift : MonoBehaviour
{
  [SerializeField] private int rift = 0;
  List<string> mapNames = new List<string>();
  private CurrentRift currentRift;

  private void Start()
  {
    mapNames = LoadMapNames();
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
    // Load scene
    UnityEngine.SceneManagement.SceneManager.LoadScene(GetRandomMap());
  }

  private int GetRandomMap()
  {
    return Random.Range(1, mapNames.Count);
  }

  private List<string> LoadMapNames()
  {
    List<string> names = new List<string>();
    names.Add("level 1 (Sandstone)");
    return names;
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
