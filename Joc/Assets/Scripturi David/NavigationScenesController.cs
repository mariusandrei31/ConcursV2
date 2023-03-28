using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class NavigationScenesController : MonoBehaviour
{
    public LevelController levelController;
    public MainController mainController;
    public DialogController dialogController;

    public List<GameObject> gameObjects;

    public GameObject currentScene;
    public GameObject lockpikMiniGame;

    public ConversationStructure tryToLeave, tryToEnterCemetery, blockedStreets, blockedHouse, visitedHouse, tutorial, firtsTime, minigame;

    public GameObject minigameArrow1, minigameArrow2;

    private void Start()
    {
        if (PlayerPrefs.GetString("Level") == "1")
            dialogController.StartText(tutorial);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mainController.actionPoints == 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider)
            {
                if (int.TryParse(hit.collider.gameObject.name, out int val))
                {
                    if (val >= 0)
                    {
                        currentScene.SetActive(false);
                        gameObjects[val].SetActive(true);
                        currentScene = gameObjects[val];

                        Debug.Log(val);
                    }

                    if (val == -10)
                    {
                        if (mainController.canExit)
                        {
                            int v = int.Parse(PlayerPrefs.GetString("Level"));

                            if (PlayerPrefs.GetString("Level") == "5")
                                PlayerPrefs.DeleteKey("NewGame");
                            PlayerPrefs.SetString("Level", (v + 1).ToString());

                            SceneManager.LoadScene("Meniu");
                        }
                        else
                        {
                            dialogController.StartText(tryToLeave);
                        }
                    }
                    else if (val == -11)
                    {
                        dialogController.StartText(tryToEnterCemetery);
                    }
                    else if (val == -12)
                    {
                        dialogController.StartText(blockedStreets);
                    }
                    else if (val == -13)
                    {
                        dialogController.StartText(blockedHouse);
                    }
                    else if (val == -14)
                    {
                        dialogController.StartText(visitedHouse);
                    }
                    else if (val == -15)
                    {
                        currentScene.SetActive(false);
                        mainController.playerTransform = levelController.playerTransforms[int.Parse(PlayerPrefs.GetString("Level")) - 1];
                        mainController.focusPointTransform = levelController.focusPointTransforms[int.Parse(PlayerPrefs.GetString("Level")) - 1];
                        mainController.playerTransform.localPosition = levelController.spawnpoint[int.Parse(PlayerPrefs.GetString("Level")) - 1].localPosition;
                        mainController.target = mainController.playerTransform.localPosition;
                        mainController.PMAnim = levelController.animators[int.Parse(PlayerPrefs.GetString("Level")) - 1];
                        levelController.houses[int.Parse(PlayerPrefs.GetString("Level")) - 1].SetActive(true);
                        mainController.canAnimate = true;

                        if (PlayerPrefs.GetString("Level") == "1")
                            dialogController.StartText(firtsTime);
                    }
                    else if (val == -20 && PlayerPrefs.GetString("Level") == "5")
                    {
                        currentScene.SetActive(false);
                        lockpikMiniGame.SetActive(true);
                        minigameArrow1.name = "-13";
                        minigameArrow2.name = "-13";
                        dialogController.StartText(minigame);
                    }
                    else if (val == -20 && PlayerPrefs.GetString("Level") != "5")
                    {
                        dialogController.StartText(blockedHouse);
                    }
                }
            }
        }
    }
}
