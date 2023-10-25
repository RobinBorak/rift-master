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
    expStages[4] = 800;
    expStages[5] = 1600;
    expStages[6] = 3200;
    expStages[7] = 6400;
    expStages[8] = 12800;
    expStages[9] = 25600;
    expStages[10] = 51200;
    expStages[11] = 102400;
    expStages[12] = 204800;
    expStages[13] = 409600;
    expStages[14] = 819200;
    expStages[15] = 1638400;
    expStages[16] = 3276800;
    expStages[17] = 6553600;
    expStages[18] = 13107200;
    expStages[19] = 26214400;
    expStages[20] = 52428800;
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