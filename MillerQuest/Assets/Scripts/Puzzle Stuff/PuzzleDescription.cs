using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text puzzleNameText;
    [SerializeField] private TMP_Text puzzleDescriptionText;
    [SerializeField] private Image puzzleImageHolder;

    public void GetPuzzleData(string name, string description, Sprite image)
    {
        puzzleNameText.text = name;
        puzzleDescriptionText.text = description;
        puzzleImageHolder.sprite = image;
    }

    public void HideDescription()
    {
        gameObject.SetActive(false);
    }

    public void ShowDescription()
    {
        gameObject.SetActive(true);
    }
}
