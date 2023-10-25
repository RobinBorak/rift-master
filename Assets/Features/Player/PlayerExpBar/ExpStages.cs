using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Exp stages

public class ExpStages
{
  public int[] expStages = new int[7];

  public ExpStages()
  {
    expStages[0] = 0;
    expStages[1] = 100;
    expStages[2] = 200;
    expStages[3] = 300;
    expStages[4] = 400;
    expStages[5] = 500;
    expStages[6] = 600;
  }

  public int GetExpStage(int level)
  {
    return expStages[level];
  }
}