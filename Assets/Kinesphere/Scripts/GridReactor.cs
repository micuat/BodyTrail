using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridReactor : MonoBehaviour
{
  public int i, j, k;
  public float[] lengths = new float[4];

  // Start is called before the first frame update
  void Start()
  {
  }

  public void Init(Transform parent, int _i, int _j, int _k)
  {
    float GridRes = parent.GetComponent<AnchorGrid>().GridRes;
    GameObject line = parent.GetComponent<AnchorGrid>().line;

    i = _i;
    j = _j;
    k = _k;
    transform.position = new Vector3(i / GridRes, j / GridRes, k / GridRes);
    transform.parent = parent;
    for (int ii = 0; ii < 4; ii++)
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
    int[] indices = { 1, 2, 4, 5 };
    foreach (var index in indices)
    {
      GameObject part = transform.parent.GetComponent<AnchorGrid>().GetParts()[index];
      Vector3 partPos = part.transform.position;
      Vector3 gridPos = transform.position;

      float ix = Mathf.Floor(partPos.x * GridRes + 0.5f);
      float iy = Mathf.Floor(partPos.y * GridRes + 0.5f);
      float iz = Mathf.Floor(partPos.z * GridRes + 0.5f);

      float s = Vector3.Distance(partPos, gridPos) * 0.5f;
      if ((ix - i) >= 0 && (ix - i) <= 1 &&
      (iy - j) >= 0 && (iy - j) <= 1 &&
      (iz - k) >= 0 && (iz - k) <= 1)
      {
        lengths[count] = Mathf.Lerp(lengths[count], s, 0.2f);
        transform.GetChild(count).localScale = new Vector3(0.01f, lengths[count], 0.01f);
      }
      else
      {
        lengths[count] = Mathf.Lerp(lengths[count], 0, 0.2f);
        if (lengths[count] < 0.01f)
        {
          transform.GetChild(count).localScale = new Vector3(0, 0, 0);
        }
        else
        {
          transform.GetChild(count).localScale = new Vector3(0.01f, lengths[count], 0.01f);
        }
      }
      transform.GetChild(count).position = Vector3.Lerp(partPos, gridPos, 0.5f);
      transform.GetChild(count).rotation = Quaternion.FromToRotation(Vector3.up, partPos - gridPos);
      count++;
    }
  }
}