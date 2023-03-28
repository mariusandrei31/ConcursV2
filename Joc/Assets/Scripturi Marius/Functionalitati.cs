using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functionalitati : MonoBehaviour
{
    public MainController mainController;
    public QuestController questController;
    public InventoryController inventoryController;
    public AchievementController achievementController;
    public DialogController dialogController;

    public GameObject PlayMeniu, Options, Meniu, Legenda;
    public bool activ_M = false, activ_L = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!activ_M)
            {
                PlayMeniu.SetActive(true);
                activ_M = true;
                mainController.actionPoints++;
            }
            else
            {
                PlayMeniu.SetActive(false);
                mainController.actionPoints--;
                activ_M = false;
                Options.SetActive(false);
                Meniu.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!activ_L)
            {
                Legenda.SetActive(true);
                activ_L = true;
                mainController.actionPoints++;
                dialogController.canChoose = false;
            }
            else
            {
                Legenda.SetActive(false);
                mainController.actionPoints--;
                activ_L = false;
                dialogController.canChoose = true;
            }
        }
    }

    public void ResumeGame()
    {
        PlayMeniu.SetActive(false);
        activ_M = false;
        mainController.actionPoints--;
    }

    public void BackToMainMenu()
    {
        questController.Save();
        inventoryController.Save();
        achievementController.Save();
    }
}
