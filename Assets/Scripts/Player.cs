using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
  static float SPEED = 5;

  private float horizAxis;
  private float vertAxis;
  private Rigidbody body;

  // Start is called before the first frame update
  void Start()
  {
    body = GetComponent<Rigidbody>();
  }

  private void input()
  {
    horizAxis = Input.GetAxis("Horizontal");
    vertAxis = Input.GetAxis("Vertical");
  }

  // Update is called once per frame
  void Update()
  {
    input();
  }

  void FixedUpdate()
  {
    body.velocity = ((Vector3.right * horizAxis) + (Vector3.forward * vertAxis)) * SPEED;
  }
}
