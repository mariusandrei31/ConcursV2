using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class NavigationScenesController : MonoBehaviour
{
    public LevelController levelController;

    public MainController mainController;
    public DialogController dialogController;

    public List<GameObject> gameObjects;

    public GameObject currentScene;

    public ConversationStructure tryToLeave, tryToEnterCemetery, blockedStreets;

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
                    }
                }
            }
        }
    }
}
