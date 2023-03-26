using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NavigationScenesController : MonoBehaviour
{
    public MainController mainController;
    public DialogController dialogController;

    public List<GameObject> gameObjects;

    public GameObject currentScene;

    public ConversationStructure tryToLeave;

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
                    }

                    if (val == -1)
                    {
                        dialogController.StartText(tryToLeave);
                    }
                }
            }
        }
    }
}
