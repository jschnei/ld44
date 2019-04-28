using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int cost = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Triggered");
        CoinController coin = collider.gameObject.GetComponent<CoinController>();
        if (coin == null) return;

        Debug.Log("Spend Coin!");
        cost -= coin.GetValue();
        // Destroy coin
        coin.DestroyCoin();

        // Destroy door

        if(cost <= 0)
        {
            Debug.Log("Door opened");
            Destroy(gameObject);
        }
    }
}
