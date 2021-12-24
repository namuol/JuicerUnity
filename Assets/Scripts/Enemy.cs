using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public GameObject target;
  public List<GameObject> eyes;
  public GameObject body;
  private Rigidbody rigidBody;

  void Awake()
  {
    rigidBody = gameObject.GetComponent<Rigidbody>();
  }

  public void SetTarget(GameObject target)
  {
    this.target = target;
    if (target)
    {
      var follower = gameObject.GetComponent<Follower>();
      follower.toFollow = target;

      foreach (var eye in eyes)
      {
        eye.GetComponent<Follower>().toFollow = target;
      }
    }
  }

  public void Die()
  {
    rigidBody.velocity = new(0, 0, 0);
    rigidBody.angularVelocity = new(0, 0, 0);
    gameObject.SetActive(false);
  }
}
