using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
  public PlayerHand player;

  public AudioSource chime;
  public AudioSource heartBeat;
  public AudioSource shaking;
  public AudioSource phoneRing;

  float speed = 5;
  bool phonePlayed = false;

  public void PlayChime()
  {
    chime.Play();
  }

  public void PlayAmbient()
  {
    heartBeat.Play();
    shaking.Play();
  }

  private void Update()
  {
    shaking.volume = (player.startDistance - player.distance) / player.startDistance;

    heartBeat.pitch = Mathf.Clamp(1 + ((player.startDistance - player.distance) / (player.startDistance / 2)), 0.75f, 3f);


    if (player.done)
    {
      heartBeat.Stop();
      shaking.Stop();
      chime.volume -= Time.deltaTime / speed;
      phoneRing.volume += Time.deltaTime / speed;
    }

    if (phoneRing.volume >= 0.8f && !phonePlayed && !player.lose)
    {
      phoneRing.Play();
      phonePlayed = true;
    }

    if (phonePlayed)
    {
      if (!phoneRing.isPlaying)
      {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
      }
    }
  }
}
