using UnityEngine;

/// Basically, a script to run when a game object is spawned.
///
/// Normally, I would make this an abstract class, but Unity doesn't seem to
/// show abstract classes in the UI, even if they're `Serializable`.
///
/// Would this work if I had at least one concrete class that inherits from
/// `SpawnHandler`?
public abstract class SpawnHandler : MonoBehaviour
{
  abstract public GameObject OnSpawn(GameObject obj);
}
