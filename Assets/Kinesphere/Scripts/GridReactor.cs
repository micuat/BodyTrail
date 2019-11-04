using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridReactor : MonoBehaviour
{
  public int i, j, k;
  public float[] lengths;
  public float[] lastTimes;
  public bool[] inRange;

  // Start is called before the first frame update
  void Start()
  {
  }

  public void Init(Transform parent, int _i, int _j, int _k)
  {
    AnchorGrid grid = parent.GetComponent<AnchorGrid>();
    float GridRes = grid.GridRes;
    GameObject line = grid.line;

    i = _i;
    j = _j;
    k = _k;
    transform.position = new Vector3(i / GridRes, j / GridRes, k / GridRes);
    transform.parent = parent;

    int numParts = grid.GetParts().Count;
    lengths = new float[numParts];
    lastTimes = new float[numParts];
    inRange = new bool[numParts];
    for (int ii = 0; ii < numParts; ii++)
    {
      GameObject gl = Instantiate(line);
      gl.transform.parent = transform;
      gl.transform.localPosition = new Vector3(0, 0, 0);
    }

  }
  // Update is called once per frame
  void Update()
  {
    float GridRes = transform.parent.GetComponent<AnchorGrid>().GridRes;
    int count = 0;
    foreach (var part in transform.parent.GetComponent<AnchorGrid>().GetParts())
    {
      Vector3 partPos = part.transform.position;
      Vector3 gridPos = transform.position;

      float ix = partPos.x * GridRes - i;
      float iy = partPos.y * GridRes - j;
      float iz = partPos.z * GridRes - k;

      float s = Vector3.Distance(partPos, gridPos) * 0.5f;
      if (ix >= -1 && ix <= 1 &&
      iy >= -1 && iy <= 1 &&
      iz >= -1 && iz <= 1)
      {
        // within range
        if (!inRange[count])
        {
          inRange[count] = true;
          lastTimes[count] = Time.time;
        }
        float t = Time.time - lastTimes[count];
        if (t > 1) t = 1;
        lengths[count] = Equations.EaseInOutCubic(t, 0, s, 1);
      }
      else
      {
        if (inRange[count])
        {
          inRange[count] = false;
          lastTimes[count] = Time.time;
        }
        float t = Time.time - lastTimes[count];
        float td = 0.5f;
        if (t > td) t = td;
        lengths[count] = Equations.EaseInOutCubic(t, s, -s, td);
      }
      if (lengths[count] < 0.01f)
      {
        transform.GetChild(count).localScale = new Vector3(0, 0, 0);
      }
      else
      {
        transform.GetChild(count).localScale = new Vector3(0.01f, lengths[count], 0.01f);
      }
      transform.GetChild(count).position = Vector3.Lerp(partPos, gridPos, 0.5f);
      transform.GetChild(count).rotation = Quaternion.FromToRotation(Vector3.up, partPos - gridPos);
      count++;
    }
  }
}