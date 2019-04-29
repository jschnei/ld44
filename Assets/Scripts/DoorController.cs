using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    ChangeDispenserController changeDispenser;
    PayslotController payslotController;
    DoorMonitorController doorMonitor;

    DoorAnimationController paywallAnimation;

    public AudioClip depositAudio;
    public AudioClip doorOpenAudio;
    public AudioClip paywallActivateAudio;

    public int cost = 10;

    // Start is called before the first frame update
    void Start()
    {
        changeDispenser = gameObject.GetComponentInChildren<ChangeDispenserController>();
        payslotController = gameObject.GetComponentInChildren<PayslotController>();
        doorMonitor = gameObject.GetComponentInChildren<DoorMonitorController>();

        paywallAnimation = gameObject.GetComponent<DoorAnimationController>();

        doorMonitor.DisplayBalance(cost);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessCoin(CoinController coin)
    {
        if (cost <= 0) return;
        
        this.GetComponent<AudioSource>().PlayOneShot(depositAudio, 0.5f);

        // Destroy coin
        cost -= coin.GetValue();
        coin.DestroyCoin();

        int change = -cost;
        if (cost <= 0)
        {
            // Destroy door
            Debug.Log("Door opened");
            Debug.Log("Make change: " + change.ToString());
            changeDispenser.DispenseChange(change);

            paywallAnimation.OpenDoor();
            doorMonitor.DisplayOpen();
            this.GetComponent<AudioSource>().PlayOneShot(doorOpenAudio, 0.5f);
            StartCoroutine(PlayPaywallActivateAudio());
        }
        else
        {
            doorMonitor.DisplayBalance(cost);
        }
        
    }

    IEnumerator PlayPaywallActivateAudio() {
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<AudioSource>().PlayOneShot(paywallActivateAudio, 0.5f);
    }
}
