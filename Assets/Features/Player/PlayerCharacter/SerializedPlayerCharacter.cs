using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayerCharacter
{
  public bool isMale = true;

  public SerializedPlayerCharacter(bool isMale)
  {
    this.isMale = isMale;
  }
}
