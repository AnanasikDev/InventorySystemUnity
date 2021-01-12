using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class playerRotation : MonoBehaviour
{
    float mousePos;
    float mousePosDelta;
    private void OnMouseDrag()
    {
        mousePosDelta = Input.mousePosition.x - mousePos;
        transform.Rotate(Vector3.down * mousePosDelta);
    }
    private void Update()
    {
        mousePos = Input.mousePosition.x;
    }
}
