using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHand : MonoBehaviour
{
  public Transform phone;
  public Transform stopTransform;
  public GameObject blackScreen;
  public CameraShake shaker;
  public AudioController sound;
  public Animator endAnimator;

  public GameObject startScreen;

  public float moveSpeed;
  public float backSpeed;
  public float shakeRate;
  public float distance;

  float verticalMovement = 0f;
  float distanceTraveled = 0f;

  public float startDistance;

  public bool done;
  public bool lose;

  public bool start = true;

  bool audioStarted = false;
  float startSpeed = 100f;

  float startInitPos;

  private void Awake()
  {
    distance = Vector3.Distance(stopTransform.position, phone.position);
    startDistance = distance;

    startInitPos = startScreen.GetComponent<RectTransform>().transform.position.y;
  }

  private void Update()
  {
    if (Input.GetKeyDown("r") && lose)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    if (Input.GetKeyDown("escape"))
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    if (done)
      return;


    if (start)
    {
      verticalMovement = Input.GetAxis("Mouse Y") * startSpeed;
    }
    else
    {
      distance = Vector3.Distance(stopTransform.position, phone.position);

      if (distance < 0.2)
      {
        done = true;
        sound.PlayChime();
        blackScreen.SetActive(true);
      }

      if (distance > 10.5f)
      {
        endAnimator.SetTrigger("End");
        done = true;
        lose = true;
      }

      verticalMovement = Input.GetAxis("Mouse Y") * moveSpeed;

      if (verticalMovement > 0)
      {
        StartCoroutine(shaker.Shake(.01f, (startDistance - distance) * shakeRate));
      }

      transform.position -= new Vector3(0, backSpeed, 0) * Time.deltaTime;

      StartCoroutine(shaker.Shake(.05f, (startDistance - distance) * shakeRate));
    }

    if (!audioStarted && !start)
    {
      sound.PlayAmbient();
      audioStarted = true;
    }
  }

  private void FixedUpdate()
  {
    if (done)
      return;

    if (start)
    {
      startScreen.GetComponent<RectTransform>().transform.position += new Vector3(0, verticalMovement * Time.fixedDeltaTime, 0);
      startScreen.GetComponent<RectTransform>().transform.position = new Vector3
      (
        startScreen.GetComponent<RectTransform>().transform.position.x,
        Mathf.Clamp(startScreen.GetComponent<RectTransform>().transform.position.y, startInitPos, 1551f),
        startScreen.GetComponent<RectTransform>().transform.position.z
      );
      Debug.Log(startScreen.GetComponent<RectTransform>().transform.position.y);
      if (startScreen.GetComponent<RectTransform>().transform.position.y > 1550f)
      {
        start = false;
      }
    }
    else
    {
      if (verticalMovement > 0)
      {
        transform.position += new Vector3(0, verticalMovement * Time.fixedDeltaTime, 0);
        distanceTraveled += verticalMovement * Time.fixedDeltaTime; //change to use distance
      }
    }
  }
}
