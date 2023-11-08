using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class Player : MonoBehaviour
{
  private static Player instance;

  private Character4D character;

  private PlayerStats playerStats;
  private int currentExpStage;

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
    playerStats = gameObject.GetComponent<PlayerStats>();
    character = gameObject.GetComponent<Character4D>();
    Reset();
    SetAnimationSpeed();
    SceneManager.activeSceneChanged += OnSceneChanged;
  }

  private void Reset()
  {
    character.AnimationManager.SetState(CharacterState.Idle);
    currentExpStage = new ExpStages().GetExpStage(playerStats.level + 1);
    playerResetDelegate?.Invoke();
  }


  public void Die()
  {
    Debug.Log("Player died");
    character.AnimationManager.Die();
    Invoke("Respawn", 1f);
  }

  private void Respawn()
  {
    PlaceAtRespawn();
    Reset();
    FindObjectOfType<CurrentRiftLogic>().DecreaseSmallProgress();
  }

  private void PlaceAtRespawn()
  {
    // Find closest RespawnPosition game object to player
    GameObject[] respawnPositions = GameObject.FindGameObjectsWithTag("Respawn");
    Transform player = gameObject.transform;
    Transform startPosition = respawnPositions[0].transform;
    foreach (GameObject respawnPosition in respawnPositions)
    {
      if (Vector3.Distance(player.position, respawnPosition.transform.position) < Vector3.Distance(player.position, startPosition.position))
      {
        startPosition = respawnPosition.transform;
      }
    }

    transform.position = startPosition.position;
  }

  private void PlaceAtStartPosition()
  {
    Transform startPosition = GameObject.FindGameObjectWithTag("Start").transform;
    transform.position = startPosition.position;
  }

  private void OnSceneChanged(Scene current, Scene next)
  {
    PlaceAtStartPosition();
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

    if (playerStats.currentExp >= currentExpStage)
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


  public Character4D TestGetCharacter4D()
  {
    return character;
  }
}
