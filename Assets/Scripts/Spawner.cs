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

  [SerializeField]
  private List<SpawnHandler> onSpawn;

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
      point.x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
      point.y = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
      point.z = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
      ++attempts;
    } while (!PointInside(point) && attempts < 100);

    return point;
  }

  bool PointInside(Vector3 point)
  {
    foreach (var excluded in exclude)
    {
      if (excluded.bounds.Contains(point) && excluded.ClosestPoint(point) == point)
      {
        return false;
      }
    }

    var inside = false;
    foreach (var included in include)
    {
      if (included.bounds.Contains(point) && included.ClosestPoint(point) == point)
      {
        inside = true;
        break;
      }
    }

    return inside;
  }

  public GameObject Spawn()
  {
    var obj = pool.GetObject();
    if (!obj)
    {
      return null;
    }

    obj.transform.position = GetRandomPoint();

    foreach (var spawnHandler in onSpawn)
    {
      obj = spawnHandler.OnSpawn(obj);
    }

    obj.SetActive(true);

    return obj;
  }
}
