using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorGrid : BodyBase
{
  public GameObject Anchor;
  public GameObject line;

  public float GridRes = 1;

  public class AnchorInfo
  {
    public GameObject go;
    public int i, j, k;
    public float[] lengths = new float[4];

    public AnchorInfo(int _i, int _j, int _k, GameObject a)
    {
      i = _i;
      j = _j;
      k = _k;
      go = a;
    }
  }
  List<AnchorInfo> Anchors = new List<AnchorInfo>();

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
          gc.transform.position = new Vector3(i / GridRes, j / GridRes, k / GridRes);
          gc.transform.parent = transform;
          Anchors.Add(new AnchorInfo(i, j, k, gc));
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
        Vector3 gridPos = anchor.go.transform.position;

        float ix = Mathf.Floor(partPos.x * GridRes + 0.5f);
        float iy = Mathf.Floor(partPos.y * GridRes + 0.5f);
        float iz = Mathf.Floor(partPos.z * GridRes + 0.5f);

        float s = Vector3.Distance(partPos, gridPos) * 0.5f;
        if ((ix - anchor.i) >= 0 && (ix - anchor.i) <= 1 &&
        (iy - anchor.j) >= 0 && (iy - anchor.j) <= 1 &&
        (iz - anchor.k) >= 0 && (iz - anchor.k) <= 1)
        {
          anchor.lengths[count] = Mathf.Lerp(anchor.lengths[count], s, 0.2f);
          anchor.go.transform.GetChild(count).localScale = new Vector3(0.01f, anchor.lengths[count], 0.01f);
        }
        else
        {
          anchor.lengths[count] = Mathf.Lerp(anchor.lengths[count], 0, 0.2f);
          if (anchor.lengths[count] < 0.01f)
          {
            anchor.go.transform.GetChild(count).localScale = new Vector3(0, 0, 0);
          }
          else
          {
            anchor.go.transform.GetChild(count).localScale = new Vector3(0.01f, anchor.lengths[count], 0.01f);
          }
        }
        anchor.go.transform.GetChild(count).position = Vector3.Lerp(gridPos, partPos, 0.5f);
        anchor.go.transform.GetChild(count).rotation = Quaternion.FromToRotation(Vector3.up, partPos - gridPos);
        count++;
      }
    }
  }
}