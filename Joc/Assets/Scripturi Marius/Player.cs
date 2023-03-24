using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public List<bool> camere = new List<bool>();
    public List<bool> camereZero = new List<bool>();
    public void Continue()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        camere = data.camere;
    }
    public void NewGame()
    {
        camere = camereZero;
        SaveSystem.SavePlayer(this);
    }
    public void SaveGame()
    {
        SaveSystem.SavePlayer(this);
    }
    void Start()
    {
        Scene joc = SceneManager.GetActiveScene();
        if (OK.ok == true)
            Continue();
        else
            NewGame();
    }
}
