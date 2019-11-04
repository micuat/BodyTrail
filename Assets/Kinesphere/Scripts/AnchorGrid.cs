using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorGrid : BodyBase
{
  public GameObject line;

  public float GridRes = 1;

  List<GameObject> Anchors = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {
    base.loadPlayers();

    int N = 4;
    int M = 2;
    for (int i = -M; i <= M; i++)
    {
      for (int j = 0; j <= N; j++)
      {
        for (int k = -M; k <= M; k++)
        {
          GameObject gc = new GameObject("Anchor");
          gc.AddComponent(typeof(GridReactor));
          gc.transform.position = new Vector3(i / GridRes, j / GridRes, k / GridRes);
          gc.transform.parent = transform;
          gc.GetComponent<GridReactor>().i = i;
          gc.GetComponent<GridReactor>().j = j;
          gc.GetComponent<GridReactor>().k = k;
          Anchors.Add(gc);
          for (int ii = 0; ii < 4; ii++)
          {
            GameObject gl = Instantiate(line);
            gl.transform.parent = gc.transform;
            gl.transform.localPosition = new Vector3(0, 0, 0);
          }
        }
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    foreach (var anchor in Anchors)
    {
      int count = 0;
      int[] indices = { 1, 2, 4, 5 };
      foreach (var index in indices)
      {
        GameObject part = parts[index];
        Vector3 partPos = part.transform.position;
        Vector3 gridPos = anchor.transform.position;
        GridReactor reactor = anchor.GetComponent<GridReactor>();

        float ix = Mathf.Floor(partPos.x * GridRes + 0.5f);
        float iy = Mathf.Floor(partPos.y * GridRes + 0.5f);
        float iz = Mathf.Floor(partPos.z * GridRes + 0.5f);

        float s = Vector3.Distance(partPos, gridPos) * 0.5f;
        if ((ix - reactor.i) >= 0 && (ix - reactor.i) <= 1 &&
        (iy - reactor.j) >= 0 && (iy - reactor.j) <= 1 &&
        (iz - reactor.k) >= 0 && (iz - reactor.k) <= 1)
        {
          reactor.lengths[count] = Mathf.Lerp(reactor.lengths[count], s, 0.2f);
          anchor.transform.GetChild(count).localScale = new Vector3(0.01f, reactor.lengths[count], 0.01f);
        }
        else
        {
          reactor.lengths[count] = Mathf.Lerp(reactor.lengths[count], 0, 0.2f);
          if (reactor.lengths[count] < 0.01f)
          {
            anchor.transform.GetChild(count).localScale = new Vector3(0, 0, 0);
          }
          else
          {
            anchor.transform.GetChild(count).localScale = new Vector3(0.01f, reactor.lengths[count], 0.01f);
          }
        }
        anchor.transform.GetChild(count).position = Vector3.Lerp(partPos, gridPos, 0.5f);
        anchor.transform.GetChild(count).rotation = Quaternion.FromToRotation(Vector3.up, partPos - gridPos);
        count++;
      }
    }
  }
}