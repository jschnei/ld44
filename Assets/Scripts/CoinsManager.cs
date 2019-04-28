using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public GameObject NICKEL_PREFAB;
    public GameObject DIME_PREFAB;
    public GameObject QUARTER_PREFAB;

    CoinController activeCoin = null;

    GameObject GetPrefab(CoinType coinType)
    {
        switch (coinType)
        {
            case CoinType.FIVE:
                return NICKEL_PREFAB;
            case CoinType.TEN:
                return DIME_PREFAB;
            case CoinType.TWENTYFIVE:
                return QUARTER_PREFAB;
            default:
                Debug.LogError("Invalid CoinType");
                return null;
        }
    }

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
        } else if (Input.GetKeyDown("q"))
        {
            AddCoin(CoinType.FIVE, new Vector2(0,0));
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

    public void AddCoin(CoinType coinType, Vector2 location)
    {
        GameObject prefab = GetPrefab(coinType);
        GameObject coinObject = Instantiate(prefab);

        coinObject.transform.parent = gameObject.transform;

        CoinController coin = coinObject.GetComponent<CoinController>();
        coin.SetLocation(location);
    }

}
