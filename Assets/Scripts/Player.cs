using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  static float SPEED = 5;

  [SerializeField]
  private GameObject bulletPrefab;

  [SerializeField]
  private GameObject aimPoint;

  private float horizMoveAxis;
  private float vertMoveAxis;
  private bool fired;
  private bool firePressed;
  private Rigidbody body;
  private int bulletPoolSize = 15;
  private List<GameObject> bulletPool;

  void Awake()
  {
    body = GetComponent<Rigidbody>();
    bulletPool = new List<GameObject>();
    GameObject bullet;
    for (var i = 0; i < bulletPoolSize; ++i)
    {
      bullet = Instantiate(bulletPrefab);
      bullet.SetActive(false);
      bulletPool.Add(bullet);
    }
  }

  void FixedUpdate()
  {
    input();

    // Movement
    body.velocity = ((Vector3.left * horizMoveAxis) + (Vector3.back * vertMoveAxis)) * SPEED;

    // Weapons
    if (fired)
    {
      fire();
    }

    body.transform.Rotate(new Vector3(0, 0.5f, 0));
  }

  private Bullet getBullet()
  {
    for (var i = 0; i < bulletPoolSize; ++i)
    {
      if (!bulletPool[i].activeInHierarchy)
      {
        return bulletPool[i].GetComponent<Bullet>();
      }
    }

    return null;
  }

  private void input()
  {
    horizMoveAxis = Input.GetAxis("Horizontal");
    vertMoveAxis = Input.GetAxis("Vertical");

    // Basically reimplementing `GetButtonDown` but for `FixedUpdate`, here
    var fireWasPressed = firePressed;
    firePressed = Input.GetButton("Fire1");
    fired = !fireWasPressed && firePressed;
  }

  private void fire()
  {
    fired = false;

    // Spawn a bullet
    var bullet = getBullet();
    if (!bullet)
    {
      return;
    }

    bullet.shoot(body.position, aimPoint.transform.position);
  }
}
