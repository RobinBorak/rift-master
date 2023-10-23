using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
  private GameObject target;

  void Start()
  {
    // Set target to player
    target = GameObject.FindGameObjectWithTag("Player");
  }

  // Update is called once per frame
  void Update()
  {
    FollowTarget();
  }

  void FollowTarget()
  {
    transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
  }
}
