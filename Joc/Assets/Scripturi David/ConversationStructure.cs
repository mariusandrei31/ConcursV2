using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Scriptabe Objects/Dialog System/Conversation")]
public class ConversationStructure : ScriptableObject
{
    public List<DialogStructure> dialogStructure = new List<DialogStructure>();
}
