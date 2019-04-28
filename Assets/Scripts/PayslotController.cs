using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayslotController : MonoBehaviour
{
    public int cost = 10;
    public GameObject changeDispenserObject;
    ChangeDispenserController changeDispenser;

    // Start is called before the first frame update
    void Start()
    {
        changeDispenser = changeDispenserObject.GetComponent<ChangeDispenserController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(cost > 0)
        {
            Debug.Log("Triggered");
            CoinController coin = collider.gameObject.GetComponent<CoinController>();
            if (coin == null) return;

            Debug.Log("Spend Coin!");
            cost -= coin.GetValue();
            // Destroy coin
            coin.DestroyCoin();

            // Destroy door

            int change = -cost;
            if (cost <= 0)
            {
                Debug.Log("Door opened");
                Debug.Log("Make change: " + change.ToString());
                changeDispenser.DispenseChange(change);
                gameObject.GetComponentInParent<PaywallAnimationController>().OpenDoor();
            }
        }

    }
}
