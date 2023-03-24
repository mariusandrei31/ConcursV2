using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Database", menuName = "Scriptabe Objects/Quests System/Quest Database")]
public class QuestDatabase : ScriptableObject
{
    public List<QuestSlot> allQuests = new List<QuestSlot>();
}
