using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayslotController : MonoBehaviour
{
    DoorController door;

    // Start is called before the first frame update
    void Start()
    {
        door = gameObject.GetComponentInParent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CoinController coin = collider.gameObject.GetComponent<CoinController>();
        if (coin == null) return;

        door.ProcessCoin(coin);
    }
}
