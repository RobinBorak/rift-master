using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectTalentPoints : MonoBehaviour
{
  public int talentId;
  public TextMeshProUGUI talentName;
  public TextMeshProUGUI talentPoints;
  public Button addPointButton;

  private Talents talents;
  private Talent talent;
  private PlayerTalents playerTalents;

  // Start is called before the first frame update
  void OnEnable()
  {
    talents = new Talents();
    playerTalents = FindObjectOfType<PlayerTalents>();

    Talent talent = talents.GetTalent(talentId);
    talentName.text = talent.name;
    talentPoints.text = "+ " + playerTalents.GetTalentPoints(talentId).ToString();

    toggleAddPointButton();

  }

  void Start()
  {
    addPointButton.onClick.AddListener(AddTalentPoint);
    PlayerTalents.onTalentsChangedCallback += toggleAddPointButton;
  }

  private void AddTalentPoint()
  {
    Debug.Log("Add point to talent " + talentId);
    playerTalents.AddTalent(talentId);
    talentPoints.text = "+ " + playerTalents.GetTalentPoints(talentId).ToString();
  }


  private void toggleAddPointButton()
  {
    addPointButton.gameObject.SetActive(playerTalents.availableTalentPoints > 0);
  }
}
