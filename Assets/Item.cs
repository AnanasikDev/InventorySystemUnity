using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Item : MonoBehaviour, IPointerClickHandler
{
    public GameObject self;
    public Image icon;
    public int maxAmount = 4;
    bool attachedToCursor = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Clicked");
        attachedToCursor = true;
    }
    private void Update()
    {
        if (attachedToCursor && Input.GetMouseButtonUp(0))
        {
            attachedToCursor = false;
        }
        if (attachedToCursor) 
        { 
            transform.position = Input.mousePosition; 
            transform.SetAsFirstSibling(); 
        }
    }
}
