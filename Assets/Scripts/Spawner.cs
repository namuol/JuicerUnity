using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField]
  private ObjectPool pool;

  [SerializeField]
  private List<Collider> include;

  [SerializeField]
  private List<Collider> exclude;

  private Bounds spawnBounds;
  private bool spawnBoundsDirty;

  void FixedUpdate()
  {
    spawnBoundsDirty = true;
  }

  void UpdateSpawnBounds()
  {
    if (!spawnBoundsDirty)
    {
      return;
    }

    spawnBounds.center.Set(0, 0, 0);
    spawnBounds.min.Set(0, 0, 0);
    spawnBounds.max.Set(0, 0, 0);
    foreach (var included in include)
    {
      spawnBounds.Encapsulate(included.bounds.min);
      spawnBounds.Encapsulate(included.bounds.max);
    }

    spawnBoundsDirty = false;
  }

  Vector3 GetRandomPoint()
  {
    UpdateSpawnBounds();
    Vector3 point = new(0, 0, 0);
    int attempts = 0;
    do
    {
      ++attempts;
    } while (!PointInside(point) && attempts < 100);

    return point;
  }

  bool PointInside(Vector3 point)
  {
    return true;
  }

  public GameObject Spawn()
  {
    var obj = pool.GetObject();
    obj.transform.position = GetRandomPoint();
    return obj;
  }
}
