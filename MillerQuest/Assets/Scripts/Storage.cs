using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage instance;

    [SerializeField] private List<GameObject> blocks = new List<GameObject>();

    private readonly Vector3 scaleModifier = Vector3.one * 0.4f;
    private const float xPos = 7f;
    private const float startY = 3.5f;
    private const float deltaY = 0.75f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CodeBlock block))
        {
            AddBlock(block.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CodeBlock block))
        {
            if (Input.GetMouseButtonUp(0))
            {
                AddBlock(block.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CodeBlock block))
        {
            foreach (GameObject b in blocks)
            {
                if (b.GetComponent<CodeBlock>() == block)
                {
                    blocks.Remove(b);
                    ArrangeBlocks();
                    break;
                }
            }
        }
    }

    public void AddBlock(GameObject block)
    {
        if (!blocks.Contains(block))
        {
            blocks.Add(block);
            ArrangeBlocks();
        }
    }

    public void ArrangeBlocks()
    {
        int i = 0;
        foreach (GameObject block in blocks)
        {
            block.transform.localScale = scaleModifier;
            block.transform.position = new Vector3(xPos, startY - (i * deltaY));
            DragAndDrop d = block.GetComponent<DragAndDrop>();
            d.SetValidLocation(block.transform.position);
            d.SetScale(scaleModifier);
            i++;
        }
    }
}
