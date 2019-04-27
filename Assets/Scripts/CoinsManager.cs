using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public int activeIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        CoinController startCoin = GetCoinController(activeIndex);
        startCoin.Activate();
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
                        .GetComponent(typeof(CoinController)) as CoinController;
    }

    void SwitchToIndex(int nextIndex)
    {
        CoinController oldController = GetCoinController(activeIndex);
        CoinController newController = GetCoinController(nextIndex);

        oldController.Deactivate();
        newController.Activate();

        activeIndex = nextIndex;
    }

    void SwitchForward()
    {
        int nextIndex = (activeIndex + 1) % transform.childCount;
        SwitchToIndex(nextIndex);
    }

    void SwitchBackward()
    {
        int nextIndex = activeIndex - 1;
        if (nextIndex < 0) nextIndex = transform.childCount - 1;
        SwitchToIndex(nextIndex);
    }
}
