using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamage : MonoBehaviour
{
  [SerializeField]
  private List<DamageHandler> onDamage;

  [SerializeField]
  private float defaultHitPoints = 1.0f;
  private float hitPoints;

  public void Reset()
  {
    hitPoints = defaultHitPoints;
  }

  void Awake()
  {
    Reset();
  }

  private void OnCollisionEnter(Collision collision)
  {
    var damager = collision.gameObject.GetComponent<DealsDamage>();
    if (damager) Damage(damager);
  }

  private void OnTriggerEnter(Collider other)
  {
    var damager = other.gameObject.GetComponent<DealsDamage>();
    if (damager) Damage(damager); else Debug.Log("NO DAMAGER (trigger)");
  }

  public void Damage(DealsDamage damager)
  {
    if (damager.gameObject.GetComponent<Explosion>() != null)
    {
      Debug.Log("Explode hit!");
    }
    hitPoints -= damager.amount;
    foreach (var handler in onDamage)
    {
      handler.OnDamage(damager);
    }

    if (hitPoints <= 0)
    {
      foreach (var handler in onDamage)
      {
        handler.OnFullyDamaged(damager);
      }
    }
  }
}
