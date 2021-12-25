using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  public void OnEnable()
  {
    var radius = 10.0f;
    var force = 5000.0f;
    Vector3 explosionPos = transform.position;
    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    foreach (Collider hit in colliders)
    {
      Rigidbody rb = hit.GetComponent<Rigidbody>();
      Debug.Log("Explosion force!!!");
      if (rb != null)
        rb.AddExplosionForce(force, explosionPos, radius, 2.0F);
    }
  }

  public void Deactivate()
  {
    gameObject.SetActive(false);
  }
}
