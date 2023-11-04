using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedPlayerEquipment
{
  public int helmetId = -1;
  public int armorId = -1;
  public int meleeWeapon1HId = -1;

  public SerializedPlayerEquipment(
    int helmetId,
    int armorId,
    int meleeWeapon1HId
  )
  {
    this.helmetId = helmetId;
    this.armorId = armorId;
    this.meleeWeapon1HId = meleeWeapon1HId;
  }

  public SerializedPlayerEquipment()
  {
  }
}
