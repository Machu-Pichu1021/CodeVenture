using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLine : CodeBlock
{
    [SerializeField] private string argument;
    
    public override void Execute()
    {
        OutputHandler.instance.AddOutput(argument + "\n");
    }
}
