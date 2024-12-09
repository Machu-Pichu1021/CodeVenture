using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleObject : MonoBehaviour
{
    [SerializeField] private string puzzleName;
    [SerializeField] [TextArea] private string puzzleDescription;
    [SerializeField] private Sprite puzzleImage;

    [SerializeField] private int numLines;
    public int NumLines { get => numLines; private set => numLines = value; }

    private Slot[] slots;
    public Slot[] Slots { get => slots; private set => slots = value; }
    private GameObject slotPrefab;
    private const float slotStartY = 3.5f;
    private const float slotYDecrement = 1.5f;

    [SerializeField] private GameObject[] blocks;

    [SerializeField] [TextArea] private string expectedOutput;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        slotPrefab = Resources.Load<GameObject>("Prefabs/Slot Prefab");
        //Delete later
        //LoadPuzzle();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            Destroy(gameObject);
        else
            LoadPuzzle();
    }

    private void LoadPuzzle()
    {
        slots = new Slot[numLines];
        float yPos = slotStartY;
        for (int i = 0; i < numLines; i++)
        {
            GameObject slot = Instantiate(slotPrefab, new Vector3(0, yPos, 0), Quaternion.identity);
            Slot s = slot.GetComponent<Slot>();
            slots[i] = s;
            s.SetLineNumberText($"{i + 1}");
            yPos -= slotYDecrement;
        }

        foreach (GameObject block in blocks)
        {
            GameObject b = Instantiate(block);
            Storage.instance.AddBlock(b);
        }

        FindObjectOfType<PuzzleDescription>().GetPuzzleData(puzzleName, puzzleDescription, puzzleImage);
    }

    public bool checkSolution(string solution)
    {
        return solution == expectedOutput;
    }
}
