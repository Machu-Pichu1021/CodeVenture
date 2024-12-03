using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private Vector3 validLocationPos;

    private void Start()
    {
        startPos = transform.position;
        validLocationPos = startPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Valid"))
            validLocationPos = collision.transform.position;
        print(validLocationPos);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Valid"))
            validLocationPos = startPos;
        print(validLocationPos);
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y);
    }

    private void OnMouseUp()
    {
        transform.position = validLocationPos;
    }
}
