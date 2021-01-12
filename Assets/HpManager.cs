using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public int Hp;
    public Text text;

    void Start()
    {
        UpdateSign();
    }
    void Push(int a, bool autoUpdate = true)
    {
        Hp += a;
        if (autoUpdate)
            UpdateSign();
    }
    bool Pull(int a, bool autoUpdate = true)
    {
        if (Hp - a < 0) return false;
        Hp -= a;
        if (autoUpdate)
            UpdateSign();
        return true;
    }
    void UpdateSign()
    {
        text.text = $"hp <size=18>{Hp}</size>/100";
    }
}
