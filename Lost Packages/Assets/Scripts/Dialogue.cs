using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//Klasse, die alle Elemente eines Dialogs enthält
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
