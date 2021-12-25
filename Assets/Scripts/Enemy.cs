using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public GameObject target;
  public List<GameObject> eyes;
  public GameObject body;
  public AudioSource explodeSound;
  public ObjectPool explosionPool;
  public GameObject feet;
  public LayerMask groundMask;

  private Rigidbody rigidBody;
  private Animator animator;
  private Follower follower;

  void Awake()
  {
    rigidBody = gameObject.GetComponent<Rigidbody>();
    animator = gameObject.GetComponent<Animator>();
    follower = gameObject.GetComponent<Follower>();
  }

  void OnEnable()
  {
    gameObject.GetComponent<TakesDamage>().Reset();
    rigidBody.isKinematic = false;
  }

  bool HasFooting()
  {
    var pos = gameObject.transform.position;
    return Physics.Raycast(pos, gameObject.transform.TransformDirection(Vector3.down), 0.6f, groundMask);
  }

  bool TouchingGround()
  {
    return Physics.Raycast(gameObject.transform.position, Vector3.down, 0.8f, groundMask);
  }

  void FixedUpdate()
  {

    if (Random.Range(0.0f, 1.0f) < 0.002)
    {
      Blink();
      // rigidBody.AddForce(Vector3.up * 20.0f, ForceMode.Impulse);
    }

    var hasFooting = HasFooting();
    if (!animator.GetBool("Walking") && hasFooting)
    {
      animator.SetBool("Walking", true);
    }
    if (animator.GetBool("Walking") && !hasFooting)
    {
      animator.SetBool("Walking", false);
    }

    // Helps enemy self-right itself:
    var touchingGround = hasFooting || TouchingGround();
    if (touchingGround)
    {
      Follow(target);
    }
    else
    {
      Follow(null);
    }

  }

  public void SetTarget(GameObject target)
  {
    this.target = target;
  }

  private void Follow(GameObject target)
  {
    follower.toFollow = target;

    foreach (var eye in eyes)
    {
      eye.GetComponent<Follower>().toFollow = target;
      if (target == null)
      {
        eye.transform.rotation.Set(0, 0, 0, 0);
      }
    }
  }

  public void Die()
  {
    Wince();
    animator.SetTrigger("Explode");
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
    Debug.Log("Deactivate!");
    rigidBody.isKinematic = true;
    gameObject.transform.forward = Vector3.forward;
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

  public void Step()
  {
    var direction = (target.transform.position - gameObject.transform.position).normalized;
    rigidBody.AddForce(direction * 4.0f, ForceMode.VelocityChange);
  }
}
