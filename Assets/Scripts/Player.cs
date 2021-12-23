using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private static readonly float SPEED = 5;

  [SerializeField]
  private GameObject bulletPrefab;

  [SerializeField]
  private GameObject aimPoint;

  // Components
  private Rigidbody body;
  private AudioSource bulletAudio;

  private float horizMoveAxis;
  private float vertMoveAxis;
  private bool fired;
  private bool firePressed;

  private readonly int bulletPoolSize = 15;
  private List<GameObject> bulletPool;

  void Awake()
  {
    body = GetComponent<Rigidbody>();
    bulletAudio = GetComponent<AudioSource>();

    bulletPool = new List<GameObject>();
    GameObject bullet;
    for (int i = 0; i < bulletPoolSize; ++i)
    {
      bullet = Instantiate(bulletPrefab);
      bullet.SetActive(false);
      bulletPool.Add(bullet);
    }
  }

  void FixedUpdate()
  {
    HandleInput();

    // Movement
    body.velocity = ((Vector3.left * horizMoveAxis) + (Vector3.back * vertMoveAxis)) * SPEED;

    // Weapons
    if (fired)
    {
      Fire();
    }

  }

  Bullet GetBullet()
  {
    for (int i = 0; i < bulletPoolSize; ++i)
    {
      if (!bulletPool[i].activeInHierarchy)
      {
        return bulletPool[i].GetComponent<Bullet>();
      }
    }

    return null;
  }

  void HandleInput()
  {
    horizMoveAxis = Input.GetAxis("Horizontal");
    vertMoveAxis = Input.GetAxis("Vertical");

    // Basically reimplementing `GetButtonDown` but for `FixedUpdate`, here
    bool fireWasPressed = firePressed;
    firePressed = Input.GetButton("Fire1");
    fired = !fireWasPressed && firePressed;
  }

  void Fire()
  {
    fired = false;

    Bullet bullet = GetBullet();
    if (!bullet)
    {
      return;
    }

    bulletAudio.Play();
    bullet.Shoot(body.position, aimPoint.transform.position);
  }
}
