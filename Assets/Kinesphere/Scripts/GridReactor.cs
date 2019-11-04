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
  }
}