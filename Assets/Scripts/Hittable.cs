using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
  private AudioSource hitSound;

  // Ideally this could be configured, right?
  void Awake()
  {
    hitSound = gameObject.GetComponent<AudioSource>();
  }

  public void hit()
  {
    Debug.Log("Hit!");
    hitSound.Play();
  }
}
