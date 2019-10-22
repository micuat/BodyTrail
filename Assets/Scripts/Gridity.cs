using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridity : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public GameObject line1;
    public GameObject line2;

    public float GridRes = 2;

    List<GameObject> parts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        parts.Add(Player1.transform.Find("Head").gameObject);
        parts.Add(Player1.transform.Find("LeftHand").gameObject);
        parts.Add(Player1.transform.Find("RightHand").gameObject);
        parts.Add(Player2.transform.Find("Head").gameObject);
        parts.Add(Player2.transform.Find("LeftHand").gameObject);
        parts.Add(Player2.transform.Find("RightHand").gameObject);

        int count = 0;
        foreach (var part in parts)
        {
            GameObject g = new GameObject("Grid");
            g.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            g.transform.SetParent(part.transform);
            for (int i = 0; i < 8; i++)
            {
                var line = line1;
                if (count < 3) line = line2;
                GameObject gc = Instantiate(line);
                gc.transform.SetParent(g.transform);
            }
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var part in parts)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject gc = part.transform.Find("Grid").GetChild(i).gameObject;

                Vector3 pos = part.transform.position;
                float ix = i % 2;
                float iy = (i / 2) % 2;
                float iz = (i / 4) % 2;
                Vector3 quant = new Vector3(Mathf.Floor(pos.x * GridRes + ix) / GridRes, Mathf.Floor(pos.y * GridRes + iy) / GridRes, Mathf.Floor(pos.z * GridRes + iz) / GridRes);

                gc.transform.position = Vector3.Lerp(pos, quant, 0.5f);
                gc.transform.rotation = Quaternion.FromToRotation(Vector3.up, pos - quant);
                float s = Vector3.Distance(pos, quant) * 5;
                float sm = Mathf.Max(s, 0.01f);
                gc.transform.localScale = new Vector3(0.1f, s, 0.1f);
            }
        }
    }
}
