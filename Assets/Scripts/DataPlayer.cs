using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer : MonoBehaviour
{
    public GameObject trailH;
    public GameObject trailL;
    public GameObject trailR;
    public Camera cam;
    int frameCount = 0;

    public Transform[] prefabs;

    public int instances = 5000;
    Dictionary<string, List<Transform>> instanceList = new Dictionary<string, List<Transform>>();

    // List<Transform> instanceListH = new List<Transform>();
    // List<Transform> instanceListL = new List<Transform>();
    // List<Transform> instanceListR = new List<Transform>();
    int instanceIndex = 0;

    public float radius = 50f;


    // Start is called before the first frame update
    void Start()
    {
        instanceList.Add("head", new List<Transform>());
        instanceList.Add("left", new List<Transform>());
        instanceList.Add("right", new List<Transform>());
        for (int i = 0; i < instances; i++)
        {
            int index = (int)Mathf.Floor(Random.value * prefabs.Length);
            float scale = Random.value * 0.05f;
            Transform t = Instantiate(prefabs[index]);
            t.localScale = new Vector3(scale, scale, scale);
            t.localPosition = new Vector3(1000000, 0, 0);// Random.insideUnitSphere * radius;
            t.SetParent(transform);
            instanceList["head"].Add(t);

            t = Instantiate(prefabs[index]);
            t.localScale = new Vector3(scale, scale, scale);
            t.localPosition = new Vector3(1000000, 0, 0);// Random.insideUnitSphere * radius;
            t.SetParent(transform);
            instanceList["left"].Add(t);

            t = Instantiate(prefabs[index]);
            t.localScale = new Vector3(scale, scale, scale);
            t.localPosition = new Vector3(1000000, 0, 0);// Random.insideUnitSphere * radius;
            t.SetParent(transform);
            instanceList["right"].Add(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var csv = gameObject.GetComponent("CSVParsing") as CSVParsing;
        int index = frameCount + 1;
        {
            float x = float.Parse(csv.data[index][5]);// + Random.value * 0.1f - 0.05f;
            float y = float.Parse(csv.data[index][6]);// + Random.value * 0.1f - 0.05f;
            float z = float.Parse(csv.data[index][7]);// + Random.value * 0.1f - 0.05f;
            float rx = float.Parse(csv.data[index][8]);
            float ry = float.Parse(csv.data[index][9]);
            float rz = float.Parse(csv.data[index][10]);
            // trailH.transform.position = new Vector3(x, y, z);

            Transform t = instanceList["head"][instanceIndex];
            t.position = new Vector3(x, y, z);
            Quaternion newQuaternion = new Quaternion();
            newQuaternion.Set(rx, ry, rz, 1);
            t.rotation = newQuaternion;
        }
        {
            float x = float.Parse(csv.data[index][5 + 3 * 2]);
            float y = float.Parse(csv.data[index][6 + 3 * 2]);
            float z = float.Parse(csv.data[index][7 + 3 * 2]);
            float rx = float.Parse(csv.data[index][8 + 3 * 2]);
            float ry = float.Parse(csv.data[index][9 + 3 * 2]);
            float rz = float.Parse(csv.data[index][10 + 3 * 2]);
            // trailL.transform.position = new Vector3(x, y, z);

            Transform t = instanceList["left"][instanceIndex];
            t.position = new Vector3(x, y, z);
            Quaternion newQuaternion = new Quaternion();
            newQuaternion.Set(rx, ry, rz, 1);
            t.rotation = newQuaternion;
        }
        {
            float x = float.Parse(csv.data[index][5 + 3 * 4]);
            float y = float.Parse(csv.data[index][6 + 3 * 4]);
            float z = float.Parse(csv.data[index][7 + 3 * 4]);
            float rx = float.Parse(csv.data[index][8 + 3 * 4]);
            float ry = float.Parse(csv.data[index][9 + 3 * 4]);
            float rz = float.Parse(csv.data[index][10 + 3 * 4]);
            // trailR.transform.position = new Vector3(x, y, z);

            Transform t = instanceList["right"][instanceIndex];
            t.position = new Vector3(x, y, z);
            Quaternion newQuaternion = new Quaternion();
            newQuaternion.Set(rx, ry, rz, 1);
            t.rotation = newQuaternion;
        }
        {
            float x = float.Parse(csv.data[index][5 + 3 * 6]);
            float y = float.Parse(csv.data[index][6 + 3 * 6]);
            float z = float.Parse(csv.data[index][7 + 3 * 6]);
            float rx = float.Parse(csv.data[index][8 + 3 * 6]);
            float ry = float.Parse(csv.data[index][9 + 3 * 6]);
            float rz = float.Parse(csv.data[index][10 + 3 * 6]);

            Transform t = cam.transform;
            t.position = Vector3.Lerp(t.position, new Vector3(x, y, z), 0.1f);
            Quaternion newQuaternion = new Quaternion();
            newQuaternion.Set(rx, ry, rz, 1);
            t.rotation = Quaternion.Lerp(t.rotation, newQuaternion, 0.1f);
        }
        frameCount = (frameCount + 1) % (csv.data.Count);
        instanceIndex = (instanceIndex + 1) % instances;
    }
}
