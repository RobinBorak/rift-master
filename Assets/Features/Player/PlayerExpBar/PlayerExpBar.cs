using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExpBar : MonoBehaviour
{
  [SerializeField] private Image expBar;
  [SerializeField] private TextMeshProUGUI levelText;
  [SerializeField] private GameObject levelIconContainer;
  private PlayerStats playerStats;
  private PlayerTalents playerTalents;
  private ExpStages expStages;
  private int currentLevel;
  private int currentExpStage;

  IEnumerator Start()
  {
    playerTalents = FindObjectOfType<PlayerTalents>();
    playerStats = FindObjectOfType<PlayerStats>();

    while (!playerTalents.isDoneLoading){
      yield return null;
    }

    currentLevel = playerStats.level;
    currentExpStage = new ExpStages().GetExpStage(currentLevel + 1);
    levelText.text = currentLevel.ToString();

    BlinkLevelIconWhenAvailableTalentPoints();
    PlayerTalents.onTalentsChangedCallback += BlinkLevelIconWhenAvailableTalentPoints;
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
    currentExpStage = new ExpStages().GetExpStage(currentLevel + 1);
    levelText.text = currentLevel.ToString();
    expBar.fillAmount = 0;
    BlinkLevelIconWhenAvailableTalentPoints();
  }


  private void BlinkLevelIconWhenAvailableTalentPoints()
  {
    levelIconContainer.GetComponent<Animator>().SetBool(
      "Blink",
      playerTalents.availableTalentPoints > 0
    );
  }

}
