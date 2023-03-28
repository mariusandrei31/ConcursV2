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
    public LevelController levelController;

    ConversationStructure conversation;
    public ConversationStructure conv_1, conv_2;

    public Animator DSAnim;

    int structureID;

    public Text conversationText;

    List<GameObject> choicesObj = new List<GameObject>();
    public List<GameObject> sceenes = new List<GameObject>();

    public GameObject ui_canvas;
    GraphicRaycaster ui_raycaster;

    PointerEventData click_data;
    List<RaycastResult> click_results;

    public GameObject choicePrefab;
    public Transform parentTransform;

    public int y_start, y_distance;

    [HideInInspector]
    public bool canChoose = true;

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

            if (int.TryParse(ui_element.name, out int val) && canChoose)
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
                    mainController.canOpenScreens = true;

                    if (mainController.loadSceene)
                    {
                        mainController.loadSceene = false;
                        mainController.canExit = true;

                        sceenes[int.Parse(PlayerPrefs.GetString("Level")) - 1].SetActive(true);
                        levelController.houses[int.Parse(PlayerPrefs.GetString("Level")) - 1].SetActive(false);
                        levelController.targetArrow[int.Parse(PlayerPrefs.GetString("Level")) - 1].name = "-14";
                        levelController.targetArrow[int.Parse(PlayerPrefs.GetString("Level")) - 1].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
            }

            if (ui_element.name == "Dialog_1" || ui_element.name == "Dialog_2")
            {
                StartText(conv_1);
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

    public void StartText(ConversationStructure _conversation)
    {
        mainController.canOpenScreens = false;

        SetDialogOnScreen(_conversation);
        UpdateConversation(0);
        OperateDialogScreen(true);
        mainController.actionPoints++;
    }

    Vector3 GetPosition(int val)
    {
        return new Vector3(0, y_start - y_distance * val, 0);
    }
}
