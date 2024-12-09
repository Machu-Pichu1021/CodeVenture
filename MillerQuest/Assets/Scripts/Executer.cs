using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executer : MonoBehaviour
{
    [SerializeField] private int lineNumber = 0;

    private PuzzleObject currentPuzzle;

    private const float xPos = -4.65f;

    [SerializeField] private AudioClip winSFX;

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

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
        OutputHandler.instance.ClearOutput();
        OutputHandler.instance.ChangeTextColor(Color.white);
        StartCoroutine(Execute());
    }

    public void HaltControl()
    {
        gameObject.SetActive(false);
        currentPuzzle.Slots[lineNumber].ShowLineNumber();
        StopAllCoroutines();
    }

    private IEnumerator Execute()
    {
        transform.position = new Vector3(xPos, currentPuzzle.Slots[lineNumber].transform.position.y);
        if (currentPuzzle.Slots[lineNumber].Block != null)
        {
            if (lineNumber != 0)
                currentPuzzle.Slots[lineNumber - 1].ShowLineNumber();
            currentPuzzle.Slots[lineNumber].HideLineNumber();
            currentPuzzle.Slots[lineNumber].Block.Execute();
        }
        else
        {
            HaltControl();
            ErrorLogger.instance.LogError("Error Code 0: No Code Block Found.", lineNumber);
            lineNumber = 0;
        }

        yield return new WaitForSeconds(1f);
        lineNumber++;

        if (lineNumber < currentPuzzle.Slots.Length)
            StartCoroutine(Execute());
        else
            OnEnd();
    }

    private void OnEnd()
    {
        currentPuzzle.Slots[lineNumber - 1].ShowLineNumber();
        gameObject.SetActive(false);
        lineNumber = 0;
        if (currentPuzzle.checkSolution(OutputHandler.instance.Output))
        {
            MusicManager.instance.Stop();
            AudioManager.instance.PlaySFX(winSFX);
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }
    }
}
