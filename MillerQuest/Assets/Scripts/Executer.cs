using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executer : MonoBehaviour
{
    private CodeBlock[] blocks;

    [SerializeField] private int lineNumber;

    private void Start()
    {
        blocks = FindObjectsByType<CodeBlock>(FindObjectsSortMode.None);
        Array.Sort(blocks, (block1, block2) => block1.lineNumber.CompareTo(block2.lineNumber));
    }

    public void Run()
    {
        StartCoroutine(Execute());
    }

    public void HaltControl()
    {
        StopAllCoroutines();
    }

    private IEnumerator Execute()
    {
        blocks[lineNumber].Execute();
        yield return new WaitForSeconds(1f);
        lineNumber++;

        if (lineNumber < blocks.Length)
            StartCoroutine(Execute());
        else
            OnEnd();
    }

    private void OnEnd()
    {
        //Check if the output is equal to the expected output
    }
}
