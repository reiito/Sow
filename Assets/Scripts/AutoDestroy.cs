using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
  [HideInInspector]
  public bool done = false;

  float diff = .1f;

  private void Awake()
  {
    if (!done)
      Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - diff);
  }

  private void Update()
  {
    if (done)
    {
      GetComponent<Animator>().StopPlayback();
      Destroy(gameObject);
    }
  }
}
