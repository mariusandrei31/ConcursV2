using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stuff Storage", menuName = "Scriptabe Objects/Inventory System/Stuff Storage")]
public class StuffStorage : ScriptableObject
{
    public List<string> storage = new List<string>();
}
