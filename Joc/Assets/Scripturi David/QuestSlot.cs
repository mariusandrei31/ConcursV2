using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestSlot
{
    public string questName = "";
    [TextArea(25, 5)]
    public string questDescription = "";
}
