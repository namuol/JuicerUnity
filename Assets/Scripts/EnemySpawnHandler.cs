using System;
using UnityEngine;

public class EnemySpawnHandler : SpawnHandler
{
  /// We mainly need this to pass the target object (usually the Player) to the
  /// enemy, which then gets passed through to its various children.
  [SerializeField]
  private GameObject target;

  public override GameObject OnSpawn(GameObject obj)
  {
    var enemy = obj.GetComponent<Enemy>();
    enemy.SetTarget(target);
    return obj;
  }
}
