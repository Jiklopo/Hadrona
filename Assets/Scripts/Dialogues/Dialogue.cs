using System;
using UnityEngine;
[Serializable]
public struct Dialogue
{
    public string name;
    [TextArea(3, 5)]
    public string text;
}
