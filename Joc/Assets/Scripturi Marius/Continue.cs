using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    Player player;
    private void OnMouseDown()
    {
        if (PlayerPrefs.HasKey("NewGame"))
        {
            OK.ok = true;
            SceneManager.LoadScene("Game");
        }
    }
}
