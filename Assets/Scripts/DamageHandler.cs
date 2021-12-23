using System;
using UnityEngine;

public abstract class DamageHandler : MonoBehaviour
{
  /// Fires whenever damage is dealt to this object
  abstract public void OnDamage(DealsDamage damager);

  /// Fires whenever this object runs out of hit points
  abstract public void OnFullyDamaged(DealsDamage damager);
}
