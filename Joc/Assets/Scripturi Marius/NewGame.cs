using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    private void OnMouseDown()
    {
        OK.ok = false;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("Level", "1");

        SceneManager.LoadScene("Game");
    }
}
