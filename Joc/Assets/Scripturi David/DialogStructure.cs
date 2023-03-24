using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogStructure
{
    public int ID;
    public string proposition;
    public List<ChoiceStructure> choices = new List<ChoiceStructure>();
}
