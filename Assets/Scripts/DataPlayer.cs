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
    Dictionary<string, int> indexOffset = new Dictionary<string, int>();

    int instanceIndex = 0;

    public float radius = 50f;

    Transform InitTransform() {
        int index = (int)Mathf.Floor(Random.value * prefabs.Length);
        float scale = Random.value * 0.05f;
        Transform t = Instantiate(prefabs[index]);
        t.localScale = new Vector3(scale, scale, scale);
        t.localPosition = new Vector3(1000000, 0, 0);// Random.insideUnitSphere * radius;
        t.SetParent(transform);
        return t;
    }
    // Start is called before the first frame update
    void Start()
    {
        instanceList.Add("head", new List<Transform>());
        instanceList.Add("left", new List<Transform>());
        instanceList.Add("right", new List<Transform>());
        indexOffset.Add("head", 0);
        indexOffset.Add("left", 1);
        indexOffset.Add("right", 2);
        for (int i = 0; i < instances; i++)
        {
            instanceList["head"].Add(InitTransform());
            instanceList["left"].Add(InitTransform());
            instanceList["right"].Add(InitTransform());
        }
    }

    void SpawnObject(int index, string key) {
        var csv = gameObject.GetComponent("CSVParsing") as CSVParsing;
        int offset = indexOffset[key] * 2 * 3;
        float x = float.Parse(csv.data[index][5 + offset]);// + Random.value * 0.1f - 0.05f;
        float y = float.Parse(csv.data[index][6 + offset]);// + Random.value * 0.1f - 0.05f;
        float z = float.Parse(csv.data[index][7 + offset]);// + Random.value * 0.1f - 0.05f;
        float rx = float.Parse(csv.data[index][8 + offset]);
        float ry = float.Parse(csv.data[index][9 + offset]);
        float rz = float.Parse(csv.data[index][10 + offset]);
        // trailH.transform.position = new Vector3(x, y, z);

        Transform t = instanceList[key][instanceIndex];
        t.position = new Vector3(x, y, z);
        Quaternion newQuaternion = new Quaternion();
        newQuaternion.Set(rx, ry, rz, 1);
        t.rotation = newQuaternion;
        float scale = Random.value * 0.05f;//2f;
        t.localScale = new Vector3(scale, scale, scale);

        var rb = t.GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, 0, 1) * 10.1f);
    }

    // Update is called once per frame
    void Update()
    {
        var csv = gameObject.GetComponent("CSVParsing") as CSVParsing;
        int index = frameCount + 1;

        // foreach(var t in instanceList["head"]) {
        //     t.localScale = t.localScale * 0.93f;
        // }
        // foreach(var t in instanceList["left"]) {
        //     t.localScale = t.localScale * 0.93f;
        // }
        // foreach(var t in instanceList["right"]) {
        //     t.localScale = t.localScale * 0.93f;
        // }
        SpawnObject(index, "head");
        SpawnObject(index, "left");
        SpawnObject(index, "right");
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
