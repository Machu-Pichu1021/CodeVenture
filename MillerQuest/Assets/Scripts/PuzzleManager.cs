using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzles;

    public void CreatePuzzleObject(int index)
    {
        Instantiate(puzzles[index]);
    }
}
