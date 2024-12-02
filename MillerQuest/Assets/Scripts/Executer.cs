using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executer : MonoBehaviour
{
    [SerializeField] private int lineNumber = 0;

    private PuzzleObject currentPuzzle;

    private void Start()
    {
        currentPuzzle = FindObjectOfType<PuzzleObject>();
        if (currentPuzzle == null)
        {
            Debug.LogError("No puzzle could be found, returning to main menu...");
            SceneLoader.instance.LoadScene(0);
        }
        else if (FindObjectsByType<PuzzleObject>(FindObjectsSortMode.None).Length > 1)
        {
            Debug.LogError("More than one puzzle found, returning to main menu...");
            SceneLoader.instance.LoadScene(0);
        }

    }

    public void Run()
    {
        gameObject.SetActive(true);
        StartCoroutine(Execute());
    }

    public void HaltControl()
    {
        StopAllCoroutines();
    }

    private IEnumerator Execute()
    {
        transform.position = new Vector3(-4.6f, currentPuzzle.Slots[lineNumber].transform.position.y, 0);
        currentPuzzle.Slots[lineNumber].Block.Execute();
        yield return new WaitForSeconds(1f);
        lineNumber++;

        if (lineNumber < currentPuzzle.Slots.Length)
            StartCoroutine(Execute());
        else
            OnEnd();
    }

    private void OnEnd()
    {
        gameObject.SetActive(false);
        lineNumber = 0;
        if (currentPuzzle.checkSolution(OutputHandler.instance.Output))
        {
            //Display win screen
        }
        else
        {
            //Display x or whatever
        }
    }
}
