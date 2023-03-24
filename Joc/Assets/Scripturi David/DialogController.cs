using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public MainController mainController;

    ConversationStructure conversation;
    public ConversationStructure conv_1, conv_2;

    public Animator DSAnim;

    int structureID;

    public Text conversationText;

    List<GameObject> choicesObj = new List<GameObject>();

    public GameObject ui_canvas;
    GraphicRaycaster ui_raycaster;

    PointerEventData click_data;
    List<RaycastResult> click_results;

    public GameObject choicePrefab;
    public Transform parentTransform;

    public int y_start, y_distance;

    private void Start()
    {
        GetComponents();
    }

    private void Update()
    {
        IfClicked();
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

            if (int.TryParse(ui_element.name, out int val))
            {
                if (val >= 0)
                {
                    structureID = val;
                    UpdateConversation(structureID);
                }
                else
                {
                    OperateDialogScreen(false);
                    mainController.actionPoints--;
                }
            }

            if (ui_element.name == "Dialog_1" || ui_element.name == "Dialog_2")
            {
                SetDialogOnScreen(conv_1);
                UpdateConversation(0);
                OperateDialogScreen(true);
                mainController.actionPoints++;
            }
        }
    }

    void CreateChoices(DialogStructure dialogStructure)
    {
        if (dialogStructure.choices.Count > 0)
        {
            for (int i = 0; i < dialogStructure.choices.Count; i++)
            {
                GameObject obj = Instantiate(choicePrefab);
                obj.name = dialogStructure.choices[i].sendID.ToString();
                obj.GetComponentInChildren<Text>().text = dialogStructure.choices[i].text;
                obj.transform.SetParent(parentTransform);
                obj.transform.localPosition = GetPosition(i);
                choicesObj.Add(obj);
            }
        }
        else
        {
            GameObject obj = Instantiate(choicePrefab);
            obj.name = "-1";
            obj.GetComponentInChildren<Text>().text = "Exit";
            obj.transform.SetParent(parentTransform);
            obj.transform.localPosition = GetPosition(0);
            choicesObj.Add(obj);
        }
    }

    void DestroyChoices()
    {
        for (int i=0; i<choicesObj.Count; i++)
            Destroy(choicesObj[i]);
    }

    void UpdateConversation(int _structureID)
    {
        conversationText.text = conversation.dialogStructure[_structureID].proposition;
        DestroyChoices();
        CreateChoices(conversation.dialogStructure[_structureID]);
    }

    void OperateDialogScreen(bool open)
    {
        DSAnim.SetBool("OpenDialogScreen", open);
    }

    void SetDialogOnScreen(ConversationStructure _conversation)
    {
        conversation = _conversation;
        structureID = 0;
    }

    Vector3 GetPosition(int val)
    {
        return new Vector3(0, y_start - y_distance * val, 0);
    }
}