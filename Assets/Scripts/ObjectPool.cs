using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  [SerializeField]
  private GameObject prefab;

  [SerializeField]
  private int poolSize = 10;

  private List<GameObject> pool;


  void Awake()
  {
    pool = new List<GameObject>();
    for (int i = 0; i < poolSize; ++i)
    {
      var obj = Instantiate(prefab);
      obj.SetActive(false);
      pool.Add(obj);
    }
  }

  public GameObject GetObject()
  {
    for (int i = 0; i < poolSize; ++i)
    {
      if (!pool[i].activeInHierarchy)
      {
        return pool[i];
      }
    }

    return null;
  }
}
