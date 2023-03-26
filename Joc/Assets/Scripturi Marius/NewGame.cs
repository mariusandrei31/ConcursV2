using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Game");
        OK.ok = false;
        PlayerPrefs.DeleteAll();
    }
}
