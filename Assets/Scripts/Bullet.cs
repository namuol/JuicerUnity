using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField]
  private float speed = 40;

  private float activeTime = 0;
  private Rigidbody body;

  public void Awake()
  {
    body = GetComponent<Rigidbody>();
  }

  public void Start()
  {
    activeTime = 0;
  }

  public void FixedUpdate()
  {
    activeTime += 1;

    // Automatically deactivate after 200 frames (~2 seconds)
    if (activeTime > 200)
    {
      activeTime = 0;
      gameObject.SetActive(false);
    }
  }

  public void Shoot(Vector3 from, Vector3 to)
  {
    gameObject.SetActive(true);
    activeTime = 0;
    body.angularVelocity = Vector3.zero;
    body.position = from;
    body.velocity = (to - from).normalized * speed;
    gameObject.transform.LookAt(gameObject.transform.position + body.velocity);
  }

  public void OnCollisionEnter(Collision collision)
  {
    Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
    Hittable hittable = collision.gameObject.GetComponent<Hittable>();
    if (hittable)
    {
      hittable.Hit();
    }

    gameObject.SetActive(false);
  }
}
