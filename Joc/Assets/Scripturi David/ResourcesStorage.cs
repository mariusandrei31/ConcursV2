using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources Storage", menuName = "Scriptabe Objects/Inventory System/Resources Storage")]
public class ResourcesStorage : ScriptableObject
{
    public List<int> storage = new List<int>();
}
