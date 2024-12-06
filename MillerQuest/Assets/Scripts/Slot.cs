using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private CodeBlock block;
    public CodeBlock Block { get => block; private set => block = value; }

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
}
