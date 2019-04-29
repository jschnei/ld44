﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDispenserController : MonoBehaviour
{
    CoinsManager coinsManager;
    // Start is called before the first frame update
    void Start()
    {
        coinsManager = GameObject.FindWithTag("Player").GetComponent<CoinsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<CoinType> MakeChange(int amount)
    {
        List<CoinType> coins = new List<CoinType>();

        while (amount > 0)
        {
            if (amount >= 25)
            {
                coins.Add(CoinType.TWENTYFIVE);
                amount -= 25;
            }
            else if (amount >= 10)
            {
                coins.Add(CoinType.TEN);
                amount -= 10;
            }
            else if (amount >= 5)
            {
                coins.Add(CoinType.FIVE);
                amount -= 5;
            }
        }

        return coins;
    }

    public void DispenseChange(int amount)
    {
        List<CoinType> change = MakeChange(amount);
        StartCoroutine(DispensePartOfChange(change));
    }


    IEnumerator DispensePartOfChange(List<CoinType> change)
    {
        if (change.Count > 0)
        {
            CoinType firstCoin = change[0];
            change.RemoveAt(0);

            DispenseCoin(firstCoin);
            yield return new WaitForSeconds(0.8f);
            StartCoroutine(DispensePartOfChange(change));
        }
    }

    public void DispenseCoin(CoinType coinType)
    {
        coinsManager.AddCoin(coinType, transform.position);
    }
}
