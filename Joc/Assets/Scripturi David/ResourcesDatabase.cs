using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources Database", menuName = "Scriptabe Objects/Inventory System/Resources Database")]
public class ResourcesDatabase : ScriptableObject
{
    public List<string> allResources = new List<string>();
}
