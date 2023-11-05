using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectedTalent
{
  public int id;
  public int points;

  public SelectedTalent(int id, int points)
  {
    this.id = id;
    this.points = points;
  }
}
