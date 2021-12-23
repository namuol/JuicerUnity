using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilisticSpawner : MonoBehaviour
{
  [SerializeField]
  private Spawner spawner;

  [SerializeField]
  private float probabilityOfSpawnPerFrame;

  void FixedUpdate()
  {
    if (Random.Range(0.0f, 1.0f) < probabilityOfSpawnPerFrame)
    {
      spawner.Spawn();
    }
  }
}
