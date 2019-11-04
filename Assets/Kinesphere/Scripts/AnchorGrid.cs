using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorGrid : BodyBase
{
  public GameObject line;

  public float GridRes = 1;

  public int Size = 4;

  List<GameObject> SelectedParts = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {
    base.loadPlayers();
    int[] indices = { 1, 2, 4, 5 };
    foreach (var index in indices)
    {
      SelectedParts.Add(parts[index]);
    }

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
        }
      }
    }
  }

  public List<GameObject> GetParts()
  {
    return SelectedParts;
  }

  // Update is called once per frame
  void Update()
  {
  }
}