using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExpBar : MonoBehaviour
{
  [SerializeField] private Image expBar;
  [SerializeField] private TextMeshProUGUI levelText;
  [SerializeField] private PlayerStats playerStats;
  private ExpStages expStages;
  private int currentLevel;
  private int currentExpStage;

  void Start()
  {
    currentLevel = playerStats.level;
    currentExpStage = new ExpStages().GetExpStage(currentLevel + 1);
    levelText.text = currentLevel.ToString();

    Player.playerExpUpdateDelegate += UpdateExpBar;
    Player.playerLevelUpDelegate += LevelUp;
    UpdateExpBar(0);
  }

  private void UpdateExpBar(int exp)
  {
    float fillAmount = (float)playerStats.currentExp / (float)currentExpStage;
    expBar.fillAmount = fillAmount;
  }

  private void LevelUp()
  {
    currentLevel = playerStats.level;
    levelText.text = currentLevel.ToString();
    expBar.fillAmount = 0;
  }

}
