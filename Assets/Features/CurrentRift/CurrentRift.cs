using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* This class holds currentRift variable when switching scenes
* Access currentRift from currentRiftLogic
**/

public class CurrentRift : MonoBehaviour
{
  public int currentRift = 0;
  private static CurrentRift instance;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void SetRiftDefault()
  {
    currentRift = 0;
  }

}
