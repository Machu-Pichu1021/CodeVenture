using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSaver : MonoBehaviour
{
    public static StatSaver instance;

    [SerializeField] private Image[] levelButtons;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateButtonColors();
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteAll();
        UpdateButtonColors();
    }

    public void CompleteLevel(int level)
    {
        PlayerPrefs.SetInt("Level " + level + " Complete", 1);
    }

    public void UpdateButtonColors()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (LevelCompleted(i))
                levelButtons[i].color = Color.green;
            else
                levelButtons[i].color = Color.white;
        }
    }

    private bool LevelCompleted(int level)
    {
        return PlayerPrefs.GetInt("Level " + level + " Complete") == 1;
    }
}
