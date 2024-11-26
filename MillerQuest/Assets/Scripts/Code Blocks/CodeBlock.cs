using UnityEngine;

public abstract class CodeBlock : MonoBehaviour
{
    public int lineNumber;

    public abstract void Execute();
}
