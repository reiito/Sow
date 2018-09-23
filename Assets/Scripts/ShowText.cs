using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct LevelBoundries
{
  public float xBoundry;
  public float yBoundry;

  public LevelBoundries(float xB, float yB)
  {
    xBoundry = xB;
    yBoundry = yB;
  }
}

public class ShowText : MonoBehaviour
{
  public PlayerHand player;

  public Transform parent;

  public GameObject text;

  public LevelBoundries boundries;

  public float spawnSpeed = 6;
  float spawnRate;
  float spawnCountDown = 0;

  int lastLine;

  string[] dialogue =
{
    " Are they ok? ",
    " Would they be busy? ",
    " It's been so long. ",
    " We never got along. ",
    " How are they? ",
    " What are we going to talk about? ",
    " If only I... ",
    " Wait... ",
    " Then again... ",
    " Would they even respond? ",
    " Do they even care? ",
    " They're a good person. ",
    " It's hard to talk to them. ",
    " They speak another language. ",
    " We have nothing in common. ",
    " I don't want to! ",
    " NO! ",
    " Please! ",
    " They weren't that bad. ",
    " Can we get along? ",
    " What if...? ",
    " What about...? ",
    " Have they changed? ",
    " That was fun. ",
    " I remember that night. ",
    " Yes! ",
    " This time will be different. ",
    " What's the worst that could happen? ",
    " Make it count. ",
    " It wasn't that bad. ",
    " Am I remembering it wrong? ",
    " Why is this so hard? ",
    " It's too awkward. ",
    " It's weird. ",
    " This should be easy. ",
    " They're family! ",
    " Just do it! ",
    " Do they love me? ",
    " Do I love them?"
  };

  private void Start()
  {
    spawnRate = player.distance / spawnSpeed;
    spawnCountDown = spawnRate;
  }

  private void Update()
  {
    if (player.done || player.start)
      return;

    spawnCountDown -= Time.deltaTime;
    if (spawnCountDown < 0.1)
    {
      SpawnText();
      spawnRate = player.distance / spawnSpeed;
      spawnCountDown = spawnRate;
    }
  }

  void SpawnText()
  {
    GameObject newText = Instantiate(text, parent);
    newText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(Random.Range(-boundries.xBoundry, boundries.xBoundry), Random.Range(-boundries.yBoundry, boundries.yBoundry), 0));
    int line = Random.Range(0, dialogue.Length);
    while (line == lastLine)
      line = Random.Range(0, dialogue.Length);
    lastLine = line;
    newText.GetComponentInChildren<Text>().text = dialogue[line];
    newText.GetComponent<AutoDestroy>().done = player.done;
  }
}
