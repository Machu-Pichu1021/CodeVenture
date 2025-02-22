using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private CodeBlock block;
    public CodeBlock Block { get => block; private set => block = value; }

    [SerializeField] private TMP_Text lineNumberText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CodeBlock block))
        {
            if ((this.block == null))
            {
                DragAndDrop d = block.GetComponent<DragAndDrop>();
                d.SetValidLocation(transform.position);
                d.SetScale(Vector3.one);
                this.block = block;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CodeBlock block))
        {
            if ((this.block == null) && Input.GetMouseButtonUp(0))
            {
                DragAndDrop d = block.GetComponent<DragAndDrop>();
                d.SetValidLocation(transform.position);
                d.SetScale(Vector3.one);
                this.block = block;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CodeBlock block))
        {
            if (this.block == block)
            {
                this.block = null;
            }
        }
    }

    public void HideLineNumber()
    {
        lineNumberText.gameObject.SetActive(false);
    }

    public void ShowLineNumber()
    {
        lineNumberText.gameObject.SetActive(true);
    }

    public void SetLineNumberText(string lineNumber)
    {
        lineNumberText.text = lineNumber;
    }
}
