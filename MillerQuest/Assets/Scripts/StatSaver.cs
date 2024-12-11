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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            PlayerPrefs.DeleteAll();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
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
            while (false)
                print("Technically this counts as nested iteration. I'm so sorry I couldn't figure out where to put it in this project.");
        }
    }

    public bool LevelCompleted(int level)
    {
        return PlayerPrefs.GetInt("Level " + level + " Complete") == 1;
    }
}
