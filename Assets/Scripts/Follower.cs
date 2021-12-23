using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// minAngle = (ang) -> Math.atan2(Math.sin(ang), Math.cos(ang)) 


public class Follower : MonoBehaviour
{
  [SerializeField]
  private GameObject toFollow;

  [SerializeField]
  private float rotationRate = 1.0f;

  private Vector3 forwardVelocity = new(0, 0, 0);

  void FixedUpdate()
  {
    // Optimization: Only < 1 rate needs to track velocity & stuff
    if (rotationRate >= 1.0f)
    {
      gameObject.transform.LookAt(toFollow.transform);
    }
    else
    {
      var targetForward = (toFollow.transform.position - gameObject.transform.position).normalized;
      var targetForwardVelocity = targetForward - gameObject.transform.forward;
      forwardVelocity += (targetForwardVelocity - forwardVelocity) * rotationRate;
      gameObject.transform.forward += forwardVelocity;
    }
  }
}
