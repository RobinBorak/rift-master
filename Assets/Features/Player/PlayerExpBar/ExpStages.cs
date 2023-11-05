using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Exp stages

public class ExpStages
{
  public int[] expStages = new int[21];

  public ExpStages()
  {
    expStages[0] = 0;
    expStages[1] = 100;
    expStages[2] = 200;
    expStages[3] = 400;
    expStages[4] = 600;
    expStages[5] = 800;
    expStages[6] = 1000;
    expStages[7] = 1400;
    expStages[8] = 1800;
    expStages[9] = 2200;
    expStages[10] = 2600;
    expStages[11] = 3400;
    expStages[12] = 4200;
    expStages[13] = 5000;
    expStages[14] = 5800;
    expStages[15] = 6600;
    expStages[16] = 7800;
    expStages[17] = 9000;
    expStages[18] = 10200;
    expStages[19] = 11400;
    expStages[20] = 12600;

  }

  public int GetExpStage(int level)
  {
    int _level = level;
    if(level > 20)
    {
      _level = 20;
    }
    return expStages[_level];
  }
}