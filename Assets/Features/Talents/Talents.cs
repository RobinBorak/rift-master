using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talents
{
  public List<Talent> talents = new List<Talent>();

  public Talents()
  {
    talents.Add(new Talent { id = 1, name = "Health" });
    talents.Add(new Talent { id = 2, name = "Speed" });
  }


  public Talent GetTalent(int id)
  {
    return talents.Find(talent => talent.id == id);
  }

}
