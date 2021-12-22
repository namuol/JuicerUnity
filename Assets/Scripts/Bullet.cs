using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField]
  private float speed = 40;

  private float activeTime = 0;
  private Rigidbody body;

  void Awake()
  {
    body = GetComponent<Rigidbody>();
    Debug.Log(body);
  }

  void Start()
  {
    activeTime = 0;
  }

  void FixedUpdate()
  {
    activeTime += 1;

    // Automatically deactivate after 200 frames (~2 seconds)
    if (activeTime > 200)
    {
      activeTime = 0;
      gameObject.SetActive(false);
    }
  }

  public void shoot(Vector3 from, Vector3 to)
  {
    gameObject.SetActive(true);
    activeTime = 0;
    body.angularVelocity = Vector3.zero;
    body.position = from;
    body.velocity = (to - from).normalized * speed;
    gameObject.transform.LookAt(gameObject.transform.position + body.velocity);
  }

  public void OnCollisionEnter()
  {
    gameObject.SetActive(false);
  }
}
