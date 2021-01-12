using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyManager : MonoBehaviour
{
    public int Balance;
    public Text text;

    void Start()
    {
        UpdateSign();
    }
    void Push(int a, bool autoUpdate = true)
    {
        Balance += a;
        if (autoUpdate)
            UpdateSign();
    }
    bool Pull(int a, bool autoUpdate = true)
    {
        if (Balance - a < 0) return false;
        Balance -= a;
        if (autoUpdate)
            UpdateSign();
        return true;
    }
    void UpdateSign()
    {
        text.text = Balance.ToString();
    }
}
