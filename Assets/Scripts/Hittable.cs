using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
  [SerializeField]
  private AudioSource hitSound;

  public void Hit()
  {
    if (hitSound != null)
    {
      hitSound.Play();
    }
  }
}
