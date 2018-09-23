using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToCamera : MonoBehaviour
{
  public Transform text;

  private void Update()
  {
    transform.position = Camera.main.WorldToScreenPoint(text.position);
  }
}