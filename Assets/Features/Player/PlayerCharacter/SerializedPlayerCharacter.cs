using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayerCharacter
{
  public bool isMale = true;
  public string username = "";

  public SerializedPlayerCharacter(bool isMale, string username)
  {
    this.isMale = isMale;
    this.username = username;
  }
}
