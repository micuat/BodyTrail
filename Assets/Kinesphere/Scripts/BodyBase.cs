using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBase : MonoBehaviour
{
    protected List<GameObject> parts = new List<GameObject>();

    // Start is called before the first frame update
    protected void loadPlayers()
    {
        GameObject Player1 = GameObject.FindWithTag("Player");
        parts.Add(Player1.transform.Find("Head").gameObject);
        parts.Add(Player1.transform.Find("LeftHand").gameObject);
        parts.Add(Player1.transform.Find("RightHand").gameObject);
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "Head")
            {
                if (gameObj != parts[0])
                {
                    parts.Add(gameObj);
                }
            }
        }
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "LeftHand")
            {
                if (gameObj != parts[1])
                {
                    parts.Add(gameObj);
                }
            }
        }
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "RightHand")
            {
                if (gameObj != parts[2])
                {
                    parts.Add(gameObj);
                }
            }
        }
    }
}
