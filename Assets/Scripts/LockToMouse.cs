using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToMouse : MonoBehaviour
{

  [SerializeField]
  private LayerMask aimMask;

  void FixedUpdate()
  {
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out RaycastHit hit, 999999, aimMask))
    {
      gameObject.transform.position = hit.point;
    }
  }
}
