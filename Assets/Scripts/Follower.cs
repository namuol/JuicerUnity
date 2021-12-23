using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// minAngle = (ang) -> Math.atan2(Math.sin(ang), Math.cos(ang)) 


public class Follower : MonoBehaviour
{
  private float ToRad(float deg)
  {
    return deg / 90.0f * Mathf.PI;
  }
  private float ToDeg(float rad)
  {
    return rad / Mathf.PI * 90.0f;
  }
  private float MinAngle(float ang)
  {
    return ang;
    // var rad = ToRad(ang);
    // return ToDeg(Mathf.Atan2(Mathf.Sin(rad), Mathf.Cos(rad)));
  }

  [SerializeField]
  private GameObject toFollow;

  [SerializeField]
  private float rotationRate = 1.0f;

  private Vector3 forwardVelocity = new(0, 0, 0);

  void FixedUpdate()
  {
    var targetForward = (toFollow.transform.position - gameObject.transform.position).normalized;
    var targetForwardVelocity = targetForward - gameObject.transform.forward;
    forwardVelocity += (targetForwardVelocity - forwardVelocity) * rotationRate;
    gameObject.transform.forward += forwardVelocity;
  }
}
