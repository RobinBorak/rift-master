using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalents : MonoBehaviour
{
  private PlayerTalents instance;
  public bool isDoneLoading = false;
  public List<SelectedTalent> selectedTalents = new List<SelectedTalent>();
  private static string key = "PlayerTalents";
  public int availableTalentPoints = 0;
  public int totalAvailableTalentPoints = 0;

  private PlayerStats playerStats;


  // Delegate when talents is changed
  public delegate void OnTalentsChanged();
  public static event OnTalentsChanged onTalentsChangedCallback;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      selectedTalents = (List<SelectedTalent>)Store.Load(key);
      if (selectedTalents == null)
      {
        selectedTalents = new List<SelectedTalent>();
      }
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
    totalAvailableTalentPoints = playerStats.level;
    availableTalentPoints = totalAvailableTalentPoints - TotalTalentPointsSpent();
    Player.playerLevelUpDelegate += () =>
    {
      availableTalentPoints++;
      totalAvailableTalentPoints++;
      onTalentsChangedCallback.Invoke();
    };
    onTalentsChangedCallback += Save;
    isDoneLoading = true;
  }

  public void AddTalent(int id)
  {
    SelectedTalent talent = selectedTalents.Find(talent => talent.id == id);
    if (talent == null)
    {
      talent = new SelectedTalent(id, 1);
      selectedTalents.Add(talent);
    }
    else
    {
      talent.points++;
    }
    availableTalentPoints--;
    onTalentsChangedCallback.Invoke();
  }

  public void RemoveTalent(int id)
  {
    SelectedTalent talent = selectedTalents.Find(talent => talent.id == id);
    if (talent != null)
    {
      talent.points--;
      if (talent.points <= 0)
      {
        selectedTalents.Remove(talent);
      }
    }
    availableTalentPoints++;
    onTalentsChangedCallback.Invoke();
  }

  public int GetTalentPoints(int id)
  {
    SelectedTalent talent = selectedTalents.Find(talent => talent.id == id);
    if (talent != null)
    {
      return talent.points;
    }
    return 0;
  }

  private int TotalTalentPointsSpent()
  {
    int total = 0;
    foreach (SelectedTalent talent in selectedTalents)
    {
      total += talent.points;
    }
    return total;
  }

  public void Save()
  {
    Store.Save(key, selectedTalents);
  }
}
