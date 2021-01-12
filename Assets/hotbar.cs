using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hotbar : MonoBehaviour
{
    GameObject panel;
    int activeIndex;
    GameObject activeItem;
    public Slot[] slots;
    int hotbarCapacity;
    public GameObject InvObj;
    Inventroy inventory;
    public Image ActiveSlotHighLighter;
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
        ActiveSlotHighLighter.transform.position = slots[0].transform.position;
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
            if (!s.Empty) s.Items.self.gameObject.SetActive(false);
            //if (!s.Empty) s.Items.gameObject.SetActive(true);
        }
        activeItem = slots[activeIndex].gameObject;
        if (!slots[activeIndex].Empty)
            slots[activeIndex].Items.self.gameObject.SetActive(true);
        ActiveSlotHighLighter.transform.position = slots[activeIndex].transform.position;
    }
}
