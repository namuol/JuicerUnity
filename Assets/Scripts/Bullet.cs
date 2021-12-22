using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField]
  private float speed = 20;

  private float activeTime = 0;
  private Rigidbody body;

  void Awake()
  {
    activeTime = 0;
    body = GetComponent<Rigidbody>();
    Debug.Log(body);
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
    body.position = from;
    body.velocity = (to - from).normalized * speed;
  }
}
