using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLine : MonoBehaviour
{
    [SerializeField] private string argument;

    public void Execute()
    {
        OutputHandler.instance.AddOutput(argument + "\n");
    }
}
