using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functionalitati : MonoBehaviour
{
    public MainController mainController;

    public GameObject PlayMeniu, Options, Meniu,Legenda;
    public bool activM = false,activL=false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activM == false)
            {
                PlayMeniu.SetActive(true);
                activM = true;
                mainController.actionPoints++;
            }
            else
            {
                PlayMeniu.SetActive(false);
                mainController.actionPoints--;
                activM = false;
                Options.SetActive(false);
                Meniu.SetActive(true);

            }

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (activL == false)
            {
                Legenda.SetActive(true);
                activL = true;
                mainController.actionPoints++;
            }
            else
            {
                Legenda.SetActive(false);
                mainController.actionPoints--;
                activL = false;
            }

        }
    }
}
