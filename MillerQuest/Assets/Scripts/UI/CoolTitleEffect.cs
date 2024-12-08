using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTitleEffect : MonoBehaviour
{
    [SerializeField] private GameObject[] letters;
    private float timeActive;

    private void Update()
    {
        timeActive += Time.deltaTime;
        for (int i = 0; i < letters.Length; i++)
        {
            GameObject letter = letters[i];
            letter.transform.localPosition = new Vector3(letter.transform.localPosition.x, Mathf.Sin(timeActive * 1.5f - i) * 50);
        }
    }
}
