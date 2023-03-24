using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Active Quests", menuName = "Scriptabe Objects/Quests System/Active Quests")]
public class ActiveQuests : ScriptableObject
{
    public List<int> activeQuests = new List<int>();
}
