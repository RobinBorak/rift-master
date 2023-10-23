using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] private float maxHealth = 5f;
  [SerializeField] private int maxDistanceToTargetToMove = 10;
  [SerializeField] private float movementSpeed = 2f;
  private float currentHealth;
  private Rigidbody2D rb;
  private CurrentRiftLogic currentRiftLogic;
  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;
    rb = gameObject.GetComponent<Rigidbody2D>();
    currentRiftLogic = FindObjectOfType<CurrentRiftLogic>();
  }

  // Update is called once per frame
  void Update()
  {

  }
  public void TakeDamage(float damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      Die();
    }
  }
  public void TakeDamage(float damage, Transform target)
  {
    TakeDamage(damage);
    gameObject.GetComponent<EnemyMovement>().Knockback(target, 50f);
  }
  private void Die()
  {
    currentRiftLogic?.IncreaseSmallProgress();
    Destroy(gameObject);
  }

  //Getters and Setters
  public int GetMaxDistanceToTargetToMove
  {
    get { return maxDistanceToTargetToMove; }
  }

  public float GetMovementSpeed
  {
    get { return movementSpeed; }
  }
}
