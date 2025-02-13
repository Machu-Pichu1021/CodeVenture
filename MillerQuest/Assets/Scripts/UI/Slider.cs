using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    private Vector3 startPos;
    private float speed = 3750;

    private bool finished;
    public bool Finished { get => finished; }

    private void Start()
    {
        startPos = transform.position;
    }

    public IEnumerator Slide()
    {
        finished = false;
        float startXPos = transform.position.x;
        while (transform.position.x > startXPos - Screen.width)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            yield return null;
        }
        transform.position = new Vector2(startXPos - Screen.width, transform.position.y);
        finished = true;
    }

    public void ResetPosition()
    {
        transform.position = startPos;
    }
}
