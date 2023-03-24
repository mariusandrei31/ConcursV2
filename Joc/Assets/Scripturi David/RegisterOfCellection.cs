using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Register Of Cellection", menuName = "Scriptabe Objects/Inventory System/Register Of Cellection")]
public class RegisterOfCellection : ScriptableObject
{
    public List<int> nrResourcesCollected = new List<int>();
    public int nrStuffCollected = 0;
    public int nrStuffCrafted = 0;
}
