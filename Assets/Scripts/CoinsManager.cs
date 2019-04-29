using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public GameObject NICKEL_PREFAB;
    public GameObject DIME_PREFAB;
    public GameObject QUARTER_PREFAB;

    LevelManager level;
    CoinController activeCoin;

    public AudioClip swapAudio;

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
        level = gameObject.GetComponentInParent<LevelManager>();

        activeCoin = GetCoinController(0);
        ActivateCoin(activeCoin);
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

    void ActivateCoin(CoinController coin)
    {
        coin.SlowActivate();
        level.FocusCamera(coin.gameObject);
    }

    void DeactivateCoin(CoinController coin)
    {
        coin.Deactivate();
    }

    void SwitchToIndex(int nextIndex)
    {
        DeactivateCoin(activeCoin);
        activeCoin = GetCoinController(nextIndex);
        ActivateCoin(activeCoin);
        this.GetComponent<AudioSource>().PlayOneShot(swapAudio, 0.8f);
    }

    void SwitchForward()
    {
        if (activeCoin == null) return;

        int activeIndex = activeCoin.GetCoinsIndex();
        int nextIndex = (activeIndex + 1) % transform.childCount;
        SwitchToIndex(nextIndex);
    }

    void SwitchBackward()
    {
        if (activeCoin == null) return;

        int activeIndex = activeCoin.GetCoinsIndex();
        int nextIndex = activeIndex - 1;
        if (nextIndex < 0) nextIndex = transform.childCount - 1;
        SwitchToIndex(nextIndex);
    }

    void ReconfigureUIs()
    {
        foreach (CoinController coin in gameObject.GetComponentsInChildren<CoinController>())
        {
            coin.ReconfigureUI();
        }
    }

    public void DestroyCoin(CoinController coin)
    {
        int numCoins = transform.childCount;

        if(numCoins == 1)
        {
            Destroy(coin.gameObject);
            Debug.Log("Last coin destroyed");
            activeCoin = null;
            return;
        }

        if (coin == activeCoin)
        {
            SwitchForward();
        }

        Destroy(coin.gameObject);

        ReconfigureUIs();
    }

    public void AddCoin(CoinType coinType, Vector2 location)
    {
        GameObject prefab = GetPrefab(coinType);
        GameObject coinObject = Instantiate(prefab);

        coinObject.transform.parent = gameObject.transform;

        CoinController coin = coinObject.GetComponent<CoinController>();
        coin.SetLocation(location);

        if (activeCoin == null)
        {
            ActivateCoin(coin);
            activeCoin = coin;
        }

        ReconfigureUIs();
    }

}
