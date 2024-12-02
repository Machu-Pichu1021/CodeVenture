using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private CodeBlock block;
    public CodeBlock Block { get => block; private set => block = value; }
}
