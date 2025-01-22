using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platformer
{
    public class CoinManager : MonoBehaviour
    {
        public static CoinManager Instance;

        [SerializeField] TextMeshProUGUI coins;

        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            coins.text = PlayerData.GetCoinsNumber().ToString();
        }

        public void SetCoinsNumber(int coinsNumber)
        {
            coins.text = coinsNumber.ToString();
        }
    }
}
