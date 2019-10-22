using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public Camera cam;
    int frameCount = 0;

    public class JointInfo
    {
        GameObject go;
        int offset;
        public JointInfo(GameObject parent, string name, int _offset) {
            go = parent.transform.Find(name).gameObject;
            offset = _offset * 2 * 3;
        }

        public void setTransform(CSVParsing csv, int index)
        {
            float x = float.Parse(csv.data[index][5 + offset]);// + Random.value * 0.1f - 0.05f;
            float y = float.Parse(csv.data[index][6 + offset]);// + Random.value * 0.1f - 0.05f;
            float z = float.Parse(csv.data[index][7 + offset]);// + Random.value * 0.1f - 0.05f;
            float rx = float.Parse(csv.data[index][8 + offset]);
            float ry = float.Parse(csv.data[index][9 + offset]);
            float rz = float.Parse(csv.data[index][10 + offset]);
            go.transform.position = new Vector3(x, y, z);
            Quaternion newQuaternion = new Quaternion();
            newQuaternion.Set(rx, ry, rz, 1);
            go.transform.rotation = newQuaternion;
        }
    }
    List<JointInfo> bodyJoints = new List<JointInfo>();

    // Start is called before the first frame update
    void Start()
    {
        bodyJoints.Add(new JointInfo(Player1, "Head", 0));
        bodyJoints.Add(new JointInfo(Player1, "LeftHand", 1));
        bodyJoints.Add(new JointInfo(Player1, "RightHand", 2));
        bodyJoints.Add(new JointInfo(Player2, "Head", 3));
        bodyJoints.Add(new JointInfo(Player2, "LeftHand", 4));
        bodyJoints.Add(new JointInfo(Player2, "RightHand", 5));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var csv = gameObject.GetComponent("CSVParsing") as CSVParsing;
        int index = frameCount + 1;

        foreach (var joint in bodyJoints)
        {
            joint.setTransform(csv, index);
        }

        frameCount = (frameCount + 1) % (csv.data.Count);
    }
}
