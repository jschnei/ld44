﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    LevelManager level;

    // Start is called before the first frame update
    void Start()
    {
        level = gameObject.GetComponentInParent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CoinController coin = collider.gameObject.GetComponent<CoinController>();
        if (coin == null) return;

        level.WinLevel();
    }
}
