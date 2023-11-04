using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayerEquipment
{
  public int helmetId;

  public SerializedPlayerEquipment(int helmetId)
  {
    this.helmetId = helmetId;
  }

  public SerializedPlayerEquipment()
  {
  }
}
