using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functionalitati : MonoBehaviour
{
    public MainController mainController;
    public QuestController questController;
    public InventoryController inventoryController;
    public AchievementController achievementController;

    public GameObject PlayMeniu, Options, Meniu;
    public bool activ = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!activ)
            {
                PlayMeniu.SetActive(true);
                activ = true;
                mainController.actionPoints++;
            }
            else
            {
                PlayMeniu.SetActive(false);
                mainController.actionPoints--;
                activ = false;
                Options.SetActive(false);
                Meniu.SetActive(true);

            }

        }
    }

    public void ResumeGame()
    {
        PlayMeniu.SetActive(false);
        activ = false;
        mainController.actionPoints--;
    }

    public void BackToMainMenu()
    {
        questController.Save();
        inventoryController.Save();
        achievementController.Save();
    }
}
