using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothing : BodyBase
{
    public GameObject line;
    public float extrapolate = 1.0f;

    List<GameObject> lines = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        base.loadPlayers();
        for (int i = 0; i < 50; i++)
        {
            GameObject gc = Instantiate(line);
            gc.transform.SetParent(transform);
            lines.Add(gc);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mLeft = parts[1].transform.position;
        Vector3 mRight = parts[2].transform.position;
        Vector3 oLeft = parts[4].transform.position;
        Vector3 oRight = parts[5].transform.position;
        Vector3 centroid1 = Vector3.Lerp(mLeft, mRight, 0.5f);
        Vector3 centroid2 = Vector3.Lerp(oLeft, oRight, 0.5f);
        float length1 = Vector3.Distance(mLeft, mRight);
        float length2 = Vector3.Distance(oLeft, oRight);
        for (int i = 0; i < 50; i++)
        {
            GameObject gc = lines[i];
            float lerp = ((float)i / 50) * (1 + extrapolate) - extrapolate * 0.5f;
            gc.transform.position = centroid1 * (1 - lerp) + centroid2 * lerp;
            Vector3 leftLerp = mLeft * (1-lerp) + oRight * lerp;
            Vector3 rightLerp = mRight * (1 - lerp) + oLeft * lerp;
            gc.transform.rotation = Quaternion.FromToRotation(Vector3.up, leftLerp - rightLerp);
            float s = (length1 * (1 - lerp) + length2 * lerp) * 0.5f;
            gc.transform.localScale = new Vector3(0.01f, s, 0.01f);
        }
    }
}
