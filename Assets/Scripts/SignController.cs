using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    int coinsOnSign = 0;

    GameObject signText;

    // Start is called before the first frame update
    void Start()
    {
        signText = transform.Find("SignText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReconfigureUI()
    {
        if(coinsOnSign > 0)
        {
            signText.GetComponent<MeshRenderer>().enabled = true;
        } else
        {
            signText.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CoinController coin = collider.gameObject.GetComponent<CoinController>();
        if (coin == null) return;

        coinsOnSign += 1;

        ReconfigureUI();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        CoinController coin = collider.gameObject.GetComponent<CoinController>();
        if (coin == null) return;

        coinsOnSign -= 1;

        ReconfigureUI();
    }
}
