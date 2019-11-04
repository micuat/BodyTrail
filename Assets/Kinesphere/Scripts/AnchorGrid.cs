using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorGrid : BodyBase
{
  public GameObject line;

  public float GridRes = 1;

  List<GameObject> Anchors = new List<GameObject>();

  public int Size = 4;

  // Start is called before the first frame update
  void Start()
  {
    base.loadPlayers();

    int N = Size;
    int M = Size / 2;
    for (int i = -M; i <= M; i++)
    {
      for (int j = 0; j <= N; j++)
      {
        for (int k = -M; k <= M; k++)
        {
          GameObject gc = new GameObject("Anchor");
          gc.AddComponent(typeof(GridReactor));
          gc.GetComponent<GridReactor>().Init(transform, i, j, k);
          Anchors.Add(gc);
        }
      }
    }
  }

  public List<GameObject> GetParts()
  {
    return parts;
  }

  // Update is called once per frame
  void Update()
  {
    foreach (var anchor in Anchors)
    {

    }
  }
}