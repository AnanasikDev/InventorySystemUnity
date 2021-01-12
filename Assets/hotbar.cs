using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotbar : MonoBehaviour
{
    GameObject panel;
    int activeIndex;
    GameObject activeItem;
    public Slot[] slots;
    int hotbarCapacity;
    public GameObject InvObj;
    Inventroy inventory;
    void Start()
    {
        panel = transform.GetChild(0).gameObject;

        int childCount = panel.transform.childCount;
        hotbarCapacity = childCount - 1;  // - 2 because of HighLighter //and elems
        slots = new Slot[hotbarCapacity];
        for (int i = 0; i < hotbarCapacity; i++)
        {
            slots[i] = panel.transform.GetChild(i).GetComponent<Slot>();
        }
        inventory = InvObj.GetComponent<Inventroy>();
    }
    void Update()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel != 0 && Time.timeScale > 0)
        {
            if (wheel >= 0.1f)
            {
                activeIndex++;
            }
            if (wheel <= -0.1f)
            {
                activeIndex--;
            }
            if (activeIndex >= hotbarCapacity) activeIndex = 0;
            if (activeIndex < 0) activeIndex = hotbarCapacity - 1;
            ChangeActiveItem();
        }
    }
    void ChangeActiveItem()
    {
        foreach (Slot s in slots)
        {
            if (!s.Empty) s.Items.gameObject.SetActive(false);
        }
        activeItem = slots[activeIndex].gameObject;
        activeItem.gameObject.SetActive(true);
        inventory.ActiveSlotHighLighter.transform.position = slots[activeIndex].transform.position;
    }
}
