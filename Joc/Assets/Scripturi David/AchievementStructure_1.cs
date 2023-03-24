using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementStructure_1
{
    public string achievementName;
    public int itemID;
    public int itemAmount;

    public AchievementStructure_1(string achievementName, int itemID, int itemAmount)
    {
        this.achievementName = achievementName;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
    }
}
