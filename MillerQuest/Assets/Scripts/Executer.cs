using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executer : MonoBehaviour
{
    public static Executer instance;

    [SerializeField] private int lineNumber = 0;
    public int LineNumber { get => lineNumber; private set => lineNumber = value; }

    private PuzzleObject currentPuzzle;

    private const float xPos = -4.65f;

    [SerializeField] private AudioClip winSFX;

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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
        lineNumber = 0;
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
            ErrorLogger.instance.LogError("Error Code 0: No Code Block Found.");

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
        lineNumber = 0;
        VariableTracker.instance.ClearVariables();
        if (currentPuzzle.checkSolution(OutputHandler.instance.Output))
        {
            MusicManager.instance.Stop();
            AudioManager.instance.PlaySFX(winSFX);
            StatSaver.instance.CompleteLevel(currentPuzzle.PuzzleNum);
            loseScreen.SetActive(false);
            winScreen.SetActive(true);
        }
        else
            loseScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
