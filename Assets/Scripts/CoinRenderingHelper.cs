using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRenderingHelper : MonoBehaviour
{

    public float rotation = 0f;

    List<Transform> eyes = new List<Transform>();
    Transform closedEyes;
    Transform coinText;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) {
            if (child.tag == "eye") {
                eyes.Add(child);
            } else if (child.tag == "coin text") {
                coinText = child;
            }
        }

        Debug.Assert(eyes.Count == 2, "how can mirrors be real if our eyes aren't real");
        Debug.Assert(coinText != null);
        Debug.Assert(coinText != null);
    }

    // Update is called once per frame
    void Update()
    {
        eyes[0].transform.position = Quaternion.AngleAxis(rotation, Vector3.forward) * new Vector3(-0.1875f, 0.25f, 0f) + this.transform.position;
        eyes[1].transform.position = Quaternion.AngleAxis(rotation, Vector3.forward) * new Vector3(0.1875f, 0.25f, 0f) + this.transform.position;
        coinText.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
    }
}
