using System;
using UnityEngine;

[Serializable]
public class EnemySpawnHandler : SpawnHandler
{
  [SerializeField]
  private GameObject target;

  public override GameObject OnSpawn(GameObject obj)
  {
    var enemy = obj.GetComponent<Enemy>();
    enemy.SetTarget(target);
    return obj;
  }
}
