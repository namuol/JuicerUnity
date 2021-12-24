using System;
using UnityEngine;

public class EnemySpawnHandler : SpawnHandler
{
  /// We mainly need this to pass the target object (usually the Player) to the
  /// enemy, which then gets passed through to its various children.
  public GameObject target;
  public ObjectPool explosionPool;

  public override GameObject OnSpawn(GameObject obj)
  {
    var enemy = obj.GetComponent<Enemy>();
    enemy.SetTarget(target);
    enemy.explosionPool = explosionPool;
    return obj;
  }
}
