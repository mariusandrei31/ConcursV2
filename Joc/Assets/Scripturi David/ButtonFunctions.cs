using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ToWorld_1()
    {
        SceneManager.LoadScene("World_1");
    }

    public void ToWorld_2()
    {
        SceneManager.LoadScene("World_2");
    }

    public void ToWorld_3()
    {
        SceneManager.LoadScene("World_3");
    }

    public void ToWorld_4()
    {
        SceneManager.LoadScene("World_4");
    }

    public void ToWorld_5()
    {
        SceneManager.LoadScene("World_5");
    }

    public void ToWorld_6()
    {
        SceneManager.LoadScene("World_6");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
