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

  void Awake()
  {
    hitPoints = defaultHitPoints;
  }

  private void OnCollisionEnter(Collision collision)
  {
    var damager = collision.gameObject.GetComponent<DealsDamage>();
    if (damager) Damage(damager);
  }

  public void Damage(DealsDamage damager)
  {
    hitPoints -= damager.amount;
    foreach (var handler in onDamage)
    {
      handler.OnDamage(damager);
    }

    if (hitPoints <= 0)
    {
      hitPoints = defaultHitPoints;
      foreach (var handler in onDamage)
      {
        handler.OnFullyDamaged(damager);
      }
    }
  }
}
