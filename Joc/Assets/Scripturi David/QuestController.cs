using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public MainController mainController;

    public QuestDatabase questDatabase;

    Dictionary<int, string> getQuestName;
    Dictionary<string, int> getQuestID;

    List<GameObject> questObjList = new List<GameObject>();
    List<int> questList = new List<int>();

    public int distanceY;
    public GameObject questIconPrefab;
    public Transform parentTransform;
    public GameObject questsHollder;
    public Text descriptionText;
    public Text noQuests;

    public GameObject questScreen;
    Animator questAnimator;

    public GameObject ui_canvas;
    GraphicRaycaster ui_raycaster;
    PointerEventData click_data;
    List<RaycastResult> click_results;

    bool questScreenOpen = false;
    private void OnApplicationQuit()
    {
        Save();
    }

    private void Start()
    {
        questAnimator = questScreen.GetComponent<Animator>();
        GetComponents();
        Repopulate();

        if(OK.ok)
            Load();
    }

    private void Update()
    {
        IfClicked();

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.Q) && mainController.canOpenScreens)
        {
            if (questScreenOpen)
                questScreenOpen = false;
            else
                questScreenOpen = true;

            bool open = questAnimator.GetBool("OpenQuestScreen");
            if (open)
            {
                HideQuests();
                questAnimator.SetBool("OpenQuestScreen", false);
                mainController.actionPoints--;
            }
            else
            {
                if (questList.Count == 0)
                    NoQuests();
                else
                    DisplayQuests();
                questAnimator.SetBool("OpenQuestScreen", true);
                mainController.actionPoints++;
            }
        }
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
                descriptionText.text = questDatabase.allQuests[questList[val]].questDescription;
            }

            if (getQuestID.ContainsKey(ui_element.name) && !questScreenOpen)
            {
                if (!questList.Contains(getQuestID[ui_element.name]))
                    AddQuest(ui_element.name);
                Destroy(ui_element);
            }
        }
    }

    void Repopulate()
    {
        getQuestName = new Dictionary<int, string>();
        getQuestID = new Dictionary<string, int>();
        for (int i=0; i<questDatabase.allQuests.Count; i++)
        {
            getQuestName[i] = questDatabase.allQuests[i].questName;
            getQuestID[questDatabase.allQuests[i].questName] = i;
        }
    }
    void NoQuests()
    {
        noQuests.text = "You have no active quests at the moment.";
        questsHollder.transform.localPosition = new Vector3(1000, 1000, 0);
    }

    void DisplayQuests()
    {
        noQuests.text = "";
        questsHollder.transform.localPosition = new Vector3(-400, 0, 0);
        questObjList = new List<GameObject>();
        for (int i = 0; i < questList.Count; i++)
        {
            GameObject obj = Instantiate(questIconPrefab);

            obj.transform.SetParent(parentTransform);
            obj.transform.localPosition = GetPosition(i);
            obj.name = i.ToString();

            obj.GetComponentInChildren<Text>().text = getQuestName[questList[i]];

            questObjList.Add(obj);
        }
    }

    void HideQuests()
    {
        for (int i = 0; i < questObjList.Count; i++)
            Destroy(questObjList[i]);
        descriptionText.text = "";
    }

    void AddQuest(string questName)
    {
        questList.Add(getQuestID[questName]);
    }

    public void Save()
    {
        string saveQuests = "";
        for (int i = 0; i < questList.Count; i++)
            saveQuests += questList[i].ToString() + '~';

        PlayerPrefs.SetString("saveQuests", saveQuests);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("saveQuests"))
        {
            questList.Clear();
            string[] savedQuests = PlayerPrefs.GetString("saveQuests").Split('~');
            for (int i = 0; i < savedQuests.Length - 1; i++)
            {
                questList.Add(int.Parse(savedQuests[i]));
            }
        }
    }

    Vector3 GetPosition(int nr)
    {
        Vector3 pos = new Vector3(0, 200, 0);
        pos.y -= distanceY * nr;
        return pos;
    }
}