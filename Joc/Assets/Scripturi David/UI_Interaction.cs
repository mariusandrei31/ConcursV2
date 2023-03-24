using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Interaction : MonoBehaviour
{
    public List<GameObject> imageList = new List<GameObject>();
    public List<GameObject> holderList = new List<GameObject>();

    public GameObject ui_canvas;
    GraphicRaycaster ui_raycaster;

    PointerEventData click_data;
    List<RaycastResult> click_results;

    GameObject imageObj = null;
    Vector3 imagePos;
    bool fallow = false;
    float difX = 0f, difY = 0f;

    GameObject holderObj = null;
    int nrHolderPresed = 0;

    private void Start()
    {
        GetComponents();
    }

    private void Update()
    {
        IfClicked();

        if (fallow)
            imageObj.transform.localPosition = GetPosition();
    }

    void GetComponents()
    {
        ui_raycaster = ui_canvas.GetComponent<GraphicRaycaster>();
        click_data = new PointerEventData(EventSystem.current);
        click_results = new List<RaycastResult>();
    }

    void IfClicked()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
            GetUiElementsClicked();
    }

    void GetUiElementsClicked()
    {
        click_data.position = Mouse.current.position.ReadValue();
        click_results.Clear();

        ui_raycaster.Raycast(click_data, click_results);

        foreach (RaycastResult result in click_results)
        {
            GameObject ui_element = result.gameObject;

            if (imageList.Contains(ui_element))
            {
                imageObj = ui_element;
                break;
            }
        }

        foreach (RaycastResult result in click_results)
        {
            GameObject ui_element = result.gameObject;

            if (holderList.Contains(ui_element))
            {
                holderObj = ui_element;
                break;
            }
        }

        int nrImg = 0;
        foreach (RaycastResult result in click_results)
        {
            GameObject ui_element = result.gameObject;

            if (imageList.Contains(ui_element))
            {
                nrImg++;
            }
        }

        if (holderObj)
        {
            if (imageObj && nrImg == 1)
            {
                nrHolderPresed++;
            }
            else
                holderObj = null;
        }

        if (imageObj)
        {
            if (nrHolderPresed%2==0)
            {
                holderObj = null;

                if (fallow)
                {
                    fallow = false;
                    imageObj = null;
                }
                else
                {
                    fallow = true;
                    imagePos = imageObj.transform.localPosition;

                    difX = GetPosition_OF().x - imagePos.x;
                    difY = GetPosition_OF().y - imagePos.y;
                }
            }
            else
            {
                fallow = false;
                imageObj.transform.localPosition = holderObj.transform.localPosition;
                imageObj = null;
            }
        }
    }

    Vector3 GetPosition()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 0f;
        pos.x -= 960f;
        pos.y -= 540f;
        pos.x -= difX;
        pos.y -= difY;

        return pos;
    }

    Vector3 GetPosition_OF()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 0f;
        pos.x -= 960f;
        pos.y -= 540f;

        return pos;
    }
}