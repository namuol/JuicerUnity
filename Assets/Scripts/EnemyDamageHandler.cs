using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : DamageHandler
{
  /// The enemy we're controlling.
  public Enemy enemy;

  public override void OnDamage(DealsDamage damager)
  {
    // Play a damage animation
    enemy.body.GetComponent<Animator>().SetTrigger("Hit");
  }

  public override void OnFullyDamaged(DealsDamage damager)
  {
    // Play a destruction animation and set game object inactive (we assume this
    // is being used by a pool)
    enemy.Die();
  }
}
