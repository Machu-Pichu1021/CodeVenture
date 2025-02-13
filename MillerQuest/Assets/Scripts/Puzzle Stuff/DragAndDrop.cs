using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 validLocationPos;
    private Vector3 scale;

    private readonly Vector3 scaleModifier = Vector3.one * 1.1f;

    [SerializeField] private AudioClip pickupSFX;
    [SerializeField] private AudioClip putdownSFX;

    private void Start()
    {
        startPos = transform.position;
        validLocationPos = startPos;
    }

    public void SetValidLocation(Vector3 location)
    {
        validLocationPos = location;
    }

    public void SetScale(Vector3 _scale)
    {
        scale = _scale;
    }

    private void OnMouseDown()
    {
        AudioManager.instance.PlaySFX(pickupSFX);
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y);
        transform.localScale = scaleModifier;
    }

    private void OnMouseUp()
    {
        AudioManager.instance.PlaySFX(putdownSFX);
        transform.position = validLocationPos;
        transform.localScale = scale;
    }
}
