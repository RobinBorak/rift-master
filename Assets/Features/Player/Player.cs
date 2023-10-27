using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  private static Player instance;

  private PlayerStats playerStats;
  private int currentExpStage;

  public float currentHealth;
  public delegate void PlayerHealthLossDelegate();
  public static event PlayerHealthLossDelegate playerHealthLossDelegate;

  public delegate void PlayerResetDelegate();
  public static event PlayerResetDelegate playerResetDelegate;

  public delegate void PlayerExpUpdateDelegate(int exp);
  public static event PlayerExpUpdateDelegate playerExpUpdateDelegate;

  public delegate void PlayerLevelUpDelegate();
  public static event PlayerLevelUpDelegate playerLevelUpDelegate;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    Reset();
    SetAnimationSpeed();
  }

  private void Reset()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
    currentExpStage = new ExpStages().GetExpStage(playerStats.level + 1);
    currentHealth = playerStats.MaxHealth;
    playerResetDelegate?.Invoke();
    SceneManager.activeSceneChanged += OnSceneChanged;
  }

  public void TakeDamage(float damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      Die();
    }
    else
    {
      playerHealthLossDelegate?.Invoke();
    }
  }

  private void Die()
  {
    Debug.Log("Player died");
    PlaceAtStartPosition();
    Reset();
    FindObjectOfType<CurrentRiftLogic>().DecreaseSmallProgress();
  }

  private void PlaceAtStartPosition()
  {
    Transform startPosition = GameObject.Find("RespawnPosition").transform;
    transform.position = startPosition.position;
  }

  private void OnSceneChanged(Scene current, Scene next)
  {
    if (next.buildIndex == 0)
    {
      PlaceAtStartPosition();
    }
    else //if (next.name == "Rift")
    {
      PlaceAtStartPosition();
    }
  }


  private void SetAnimationSpeed()
  {
    Animator anim = gameObject.GetComponent<Animator>();
    anim.speed = playerStats.attackSpeed / 2;
  }


  public void GainExp(int exp)
  {
    playerStats.currentExp += exp;
    playerExpUpdateDelegate?.Invoke(playerStats.currentExp);

    if(playerStats.currentExp >= currentExpStage)
    {
      LevelUp();
    }
  }

  private void LevelUp()
  {
    playerStats.level++;
    playerStats.currentExp = 0;
    currentExpStage = new ExpStages().GetExpStage(playerStats.level + 1);
    playerLevelUpDelegate?.Invoke();
  }
}
