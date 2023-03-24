using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stuff Database", menuName = "Scriptabe Objects/Inventory System/Stuff Database")]
public class StuffDatabase : ScriptableObject
{
    public List<string> allStuff = new List<string>();
}
