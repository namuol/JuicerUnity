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

  // For keyboard controls, we want digital axes:
  private static float ClampAxis(float axis)
  {
    if (axis == 0.0f)
    {
      return axis;
    }

    return axis > 0.0f ? 1.0f : -1.0f;
  }

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
    var targetVelocity = ((Vector3.left * horizMoveAxis) + (Vector3.back * vertMoveAxis)) * SPEED;
    // Movement
    body.velocity += (targetVelocity - body.velocity) * 0.1f;

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
    horizMoveAxis = ClampAxis(Input.GetAxis("Horizontal"));
    vertMoveAxis = ClampAxis(Input.GetAxis("Vertical"));

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
    body.AddForce((body.position - aimPoint.transform.position).normalized * 5.0f, ForceMode.Impulse);
  }
}
