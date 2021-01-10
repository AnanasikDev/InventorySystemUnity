using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public Item[] items;
    private void Start()
    {
        items = new Item[transform.childCount];
        for (int i = 0; i < items.Length; i++)
        {
            
            items[i] = transform.GetChild(i).GetComponent<Item>();
            items[i].ID = i;
        }
    }
}
