using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public GameObject target;
  public List<GameObject> eyes;
  public GameObject body;
  private Rigidbody rigidBody;

  public AudioSource explodeSound;

  public ObjectPool explosionPool;

  void Awake()
  {
    rigidBody = gameObject.GetComponent<Rigidbody>();
  }

  void OnEnable()
  {
    gameObject.GetComponent<TakesDamage>().Reset();
  }

  void FixedUpdate()
  {
    if (Random.Range(0.0f, 1.0f) < 0.001)
    {
      Blink();
    }
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
    Wince();
    GetComponent<Animator>().SetTrigger("Explode");
  }

  public void Explode()
  {
    explodeSound.Play();
    var explosion = explosionPool.GetObject();
    explosion.transform.position = gameObject.transform.position;
    explosion.SetActive(true);
  }

  public void Deactivate()
  {
    gameObject.SetActive(false);
  }

  public void Hit()
  {
    GetComponent<Animator>().SetTrigger("Hit");
    Wince();
  }

  public void Wince()
  {
    foreach (var eye in eyes)
    {
      eye.GetComponent<Animator>().SetTrigger("Wince");
    }
  }

  public void Blink()
  {
    foreach (var eye in eyes)
    {
      eye.GetComponent<Animator>().SetTrigger("Blink");
    }
  }
}
