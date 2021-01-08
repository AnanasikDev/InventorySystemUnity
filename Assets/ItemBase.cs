using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public Transform AllItems;
    public Item[] items;
    private void Start()
    {
        items = new Item[AllItems.childCount];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = AllItems.GetChild(i).GetComponent<Item>();
        }
    }
}
