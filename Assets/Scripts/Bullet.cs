using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private float activeTime = 0;

  void Start()
  {
    activeTime = 0;
  }

  void FixedUpdate()
  {
    activeTime += 1;

    // Automatically deactivate after 200 frames (~2 seconds)
    if (activeTime > 200) {
      activeTime = 0;
      gameObject.SetActive(false);
    }
  }  
}
