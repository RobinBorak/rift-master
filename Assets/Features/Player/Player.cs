using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  private static Player instance;

  private PlayerStats playerStats;

  public float currentHealth;
  public delegate void PlayerHealthLossDelegate(float damage);
  public static event PlayerHealthLossDelegate playerHealthLossDelegate;

  public delegate void PlayerResetDelegate();
  public static event PlayerResetDelegate playerResetDelegate;

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
    currentHealth = playerStats.maxHealth;
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
      playerHealthLossDelegate?.Invoke(damage);
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

}
