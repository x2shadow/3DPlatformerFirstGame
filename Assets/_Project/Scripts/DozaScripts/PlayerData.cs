using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class PlayerData
{
    [SerializeField]
    static int coins;
    static int Coins
    {
        get { return coins; }
        set
        {
            if (value < 0)
            {
                coins = 0;
            }
            else
            {
                coins = value;
            }
        }
    }

    public static void SetZeroCoins()
    {
        Coins = 0;
    }

    public static int GetCoinsNumber()
    {
        return Coins;
    }

    public static void AddCoins(int n)
    {
        Coins += n;
    }

    public static bool SpendCoins(int n)
    {
        if (Coins < n)
        {
            return false;
        }
        else
        {
            Coins -= n;
            return true;
        }
    }
}
