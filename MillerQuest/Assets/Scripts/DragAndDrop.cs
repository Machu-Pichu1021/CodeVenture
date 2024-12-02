using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y);
    }

    private void OnMouseUp()
    {
        transform.position = startPos;
    }
}
