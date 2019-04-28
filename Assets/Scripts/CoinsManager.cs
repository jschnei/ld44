using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    CoinController activeCoin = null;

    // Start is called before the first frame update
    void Start()
    {
        activeCoin = GetCoinController(0);
        activeCoin.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: switch to Input.GetButtonDown
        if (Input.GetKeyDown("l"))
        {
            SwitchForward();
        } else if (Input.GetKeyDown("j"))
        {
            SwitchBackward();
        }
    }

    CoinController GetCoinController(int index)
    {
        return transform.GetChild(index)
                        .gameObject
                        .GetComponent<CoinController>();
    }

    void DeactivateIndex(int index)
    {
        GetCoinController(index).Deactivate();
    }

    void ActivateIndex(int index)
    {
        GetCoinController(index).Activate();
    }

    void SwitchToIndex(int nextIndex)
    {
        activeCoin.Deactivate();
        activeCoin = GetCoinController(nextIndex);
        activeCoin.Activate();
    }

    void SwitchForward()
    {
        int activeIndex = activeCoin.GetCoinsIndex();
        int nextIndex = (activeIndex + 1) % transform.childCount;
        SwitchToIndex(nextIndex);
    }

    void SwitchBackward()
    {
        int activeIndex = activeCoin.GetCoinsIndex();
        int nextIndex = activeIndex - 1;
        if (nextIndex < 0) nextIndex = transform.childCount - 1;
        SwitchToIndex(nextIndex);
    }

    public void DestroyCoin(CoinController coin)
    {
        int numCoins = transform.childCount;

        if(numCoins == 1)
        {
            Destroy(coin.gameObject);
            Debug.Log("Last coin destroyed");
            return;
        }

        if (coin == activeCoin)
        {
            SwitchForward();
        }

        Destroy(coin.gameObject);        
    }
}
