using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRenderingHelper : MonoBehaviour
{

    public float rotation = 0f;
    public bool eyesOpen = true;
    public CoinType coinType = CoinType.TEN;

    List<Transform> openEyes = new List<Transform>();
    Transform closedEyes;
    Transform coinText;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) {
            if (child.tag == "eye") {
                openEyes.Add(child);
            } else if (child.tag == "coin text") {
                coinText = child;
            } else if (child.tag == "closed eyes") {
                closedEyes = child;
            }
        }

        Debug.Assert(openEyes.Count == 2, "how can mirrors be real if our eyes aren't real");
        Debug.Assert(coinText != null);
        Debug.Assert(closedEyes != null);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eye1Pos = Vector3.zero;
        Vector3 eye2Pos = Vector3.zero;

        if (coinType == CoinType.TEN) {
            eye1Pos = new Vector3(-0.1875f, 0.25f, 0f);
            eye2Pos = new Vector3(0.1875f, 0.25f, 0f);
        } else if (coinType == CoinType.FIVE) {
            eye1Pos = new Vector3(-0.25f, 0.375f, 0f);
            eye2Pos = new Vector3(0.25f, 0.375f, 0f);
        } else if (coinType == CoinType.TWENTYFIVE) {
            eye1Pos = new Vector3(-0.375f, 0.5f, 0f);
            eye2Pos = new Vector3(0.375f, 0.5f, 0f);
        }

        openEyes[0].transform.position = Quaternion.AngleAxis(rotation, Vector3.forward) * eye1Pos + this.transform.position;
        openEyes[1].transform.position = Quaternion.AngleAxis(rotation, Vector3.forward) * eye2Pos + this.transform.position;
        coinText.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        closedEyes.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

        if (eyesOpen) {
            foreach (Transform eye in openEyes) {
                eye.gameObject.SetActive(true);
            }
            closedEyes.gameObject.SetActive(false);
        } else {
            foreach (Transform eye in openEyes) {
                eye.gameObject.SetActive(false);
            }
            closedEyes.gameObject.SetActive(true);
        }
    }
}
