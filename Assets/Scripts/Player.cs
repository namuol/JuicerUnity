using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
  static float SPEED = 5;

  private float horizMoveAxis;
  private float vertMoveAxis;
  private bool fired;
  private Rigidbody body;

  [SerializeField]
  private GameObject bulletPrefab;

  // Start is called before the first frame update
  void Start()
  {
    body = GetComponent<Rigidbody>();
  }

  private void input()
  {
    horizMoveAxis = Input.GetAxis("Horizontal");
    vertMoveAxis = Input.GetAxis("Vertical");
    fired = Input.GetButtonDown("Fire1");
  }

  // Update is called once per frame
  void Update()
  {
    input();

  }

  void FixedUpdate()
  {
    // Movement
    body.velocity = ((Vector3.right * horizMoveAxis) + (Vector3.forward * vertMoveAxis)) * SPEED;

    // Weapons
    if (fired)
    {
      fire();
    }
  }

  private void fire()
  {
    fired = false;

    // Spawn a bullet
    Instantiate(bulletPrefab, body.position, Quaternion.identity);
  }
}
