using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    public MainController mainController;

    Dictionary<int, string> getName;
    Dictionary<string, int> getID;

    public ResourcesDatabase resourcesDatabase;

    public RegisterOfCellection registerOfCellection;

    public Animator ASAnim;

    public int resourceChangeOfAmount;
    public List<AchievementStructure_1> resourceBasedAchievements_1 = new List<AchievementStructure_1>();
    List<AchievementStructure_1> resourceBasedAchievements_2 = new List<AchievementStructure_1>();
    List<AchievementStructure_1> resourceBasedAchievements_3 = new List<AchievementStructure_1>();

    public string stuffBasedAchievementName;
    public string craftedStuffBasedAchievementName;
    public int stuffBasedAchievementAmount;
    public int craftedStuffBasedAchievementAmount;
    public int stuffChangeOfAmount;
    public int craftedStuffChangeOfAmount;

    public string secretAchievementName;
    public string secretAchievementDescription;

    public Sprite stars_0, stars_1, stars_2, stars_3;

    List<GameObject> achievementsObj = new List<GameObject>();

    public GameObject achievementHolderPrefab;
    public Transform parentTransform;
    public float y_position;
    public float y_distance;

    public GameObject ui_canvas;
    GraphicRaycaster ui_raycaster;

    PointerEventData click_data;
    List<RaycastResult> click_results;

    private void Start()
    {
        Repopulate();
        GetComponents();
        DisplayAchievements();

        if (PlayerPrefs.HasKey("secretAchievementStatus"))
            CreateSecretAchievement(achievementsObj.Count, secretAchievementName, secretAchievementDescription);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            bool open = ASAnim.GetBool("OpenAchievementScreen");

            if (open)
            {
                ASAnim.SetBool("OpenAchievementScreen", false);
                mainController.actionPoints--;
            }
            else
            {
                UpdateItemBasedAchievements();
                ASAnim.SetBool("OpenAchievementScreen", true);
                mainController.actionPoints++;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        IfClicked();
    }

    void Repopulate()
    {
        for (int i = 0; i < resourceBasedAchievements_1.Count; i++)
        {
            AchievementStructure_1 structure_1 = new AchievementStructure_1(resourceBasedAchievements_1[i].achievementName, resourceBasedAchievements_1[i].itemID, resourceBasedAchievements_1[i].itemAmount);
            structure_1.itemAmount += resourceChangeOfAmount;
            resourceBasedAchievements_2.Add(structure_1);

            AchievementStructure_1 structure_2 = new AchievementStructure_1(resourceBasedAchievements_1[i].achievementName, resourceBasedAchievements_1[i].itemID, resourceBasedAchievements_1[i].itemAmount);
            structure_2.itemAmount += resourceChangeOfAmount * 2;
            resourceBasedAchievements_3.Add(structure_2);
        }

        getName = new Dictionary<int, string>();
        getID = new Dictionary<string, int>();

        for (int i = 0; i < resourcesDatabase.allResources.Count; i++)
        {
            getName[i] = resourcesDatabase.allResources[i];
            getID[resourcesDatabase.allResources[i]] = i;
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

            if (ui_element.name == "SecretAchievementTrigger")
            {
                Destroy(ui_element);
                DebugCreateSecretAchievement(achievementsObj.Count, secretAchievementName, secretAchievementDescription);

                PlayerPrefs.SetString("secretAchievementStatus", "ceva");
            }
        }
    }

    void DisplayAchievements()
    {
        for (int i=0; i<resourceBasedAchievements_1.Count + 2; i++)
        {
            achievementsObj.Add(CreateAchievement(i));
        }
    }

    GameObject CreateAchievement(int val)
    {
        GameObject obj = Instantiate(achievementHolderPrefab);
        obj.transform.SetParent(parentTransform);
        obj.transform.localPosition = GetPosition(val);
        return obj;
    }

    void CreateSecretAchievement(int val, string name, string description)
    {
        GameObject obj = Instantiate(achievementHolderPrefab);
        obj.transform.SetParent(parentTransform);
        Vector3 pos = GetPosition(val);
        //pos.x = 691.5f;
        obj.transform.localPosition = pos;

        obj.transform.GetChild(0).GetComponent<Text>().text = name;
        obj.transform.GetChild(1).GetComponent<Text>().text = description;
        obj.transform.GetChild(2).GetComponent<Image>().sprite = stars_3;
    }

    void DebugCreateSecretAchievement(int val, string name, string description)
    {
        GameObject obj = Instantiate(achievementHolderPrefab);
        obj.transform.SetParent(parentTransform);
        Vector3 pos = GetPosition(val);
        obj.transform.localPosition = pos;

        obj.transform.GetChild(0).GetComponent<Text>().text = name;
        obj.transform.GetChild(1).GetComponent<Text>().text = description;
        obj.transform.GetChild(2).GetComponent<Image>().sprite = stars_3;
    }

    Vector3 GetPosition(int i)
    {
        return new Vector3(0, y_position - y_distance * i, 0);
    }

    void UpdateItemBasedAchievements()
    {
        for (int i=0; i<achievementsObj.Count - 2; i++)
        {
            if (resourceBasedAchievements_3[i].itemAmount <= registerOfCellection.nrResourcesCollected[resourceBasedAchievements_3[i].itemID])
            {
                ChangeAchievementValues(achievementsObj[i], resourceBasedAchievements_3[i].achievementName, "Completat!", stars_3);
            }
            else if (resourceBasedAchievements_2[i].itemAmount <= registerOfCellection.nrResourcesCollected[resourceBasedAchievements_2[i].itemID])
            {
                ChangeAchievementValues(achievementsObj[i], resourceBasedAchievements_3[i].achievementName, "Colecteaza " + resourceBasedAchievements_3[i].itemAmount.ToString() + " " + getName[resourceBasedAchievements_3[i].itemID], stars_3);
            }
            else if (resourceBasedAchievements_1[i].itemAmount <= registerOfCellection.nrResourcesCollected[resourceBasedAchievements_1[i].itemID])
            {
                ChangeAchievementValues(achievementsObj[i], resourceBasedAchievements_2[i].achievementName, "Colecteaza " + resourceBasedAchievements_2[i].itemAmount.ToString() + " " + getName[resourceBasedAchievements_2[i].itemID], stars_1);
            }
            else
            {
                ChangeAchievementValues(achievementsObj[i], resourceBasedAchievements_1[i].achievementName, "Colecteaza " + resourceBasedAchievements_1[i].itemAmount.ToString() + " " + getName[resourceBasedAchievements_1[i].itemID], stars_0);
            }
        }

        if (stuffBasedAchievementAmount + 2 * stuffChangeOfAmount <= registerOfCellection.nrStuffCollected)
        {
            ChangeAchievementValues(achievementsObj[resourceBasedAchievements_1.Count], stuffBasedAchievementName, "Completat!", stars_3);
        }
        else if (stuffBasedAchievementAmount + stuffChangeOfAmount <= registerOfCellection.nrStuffCollected)
        {
            ChangeAchievementValues(achievementsObj[resourceBasedAchievements_1.Count], stuffBasedAchievementName, "Colecteaza " + (stuffBasedAchievementAmount + 2 * stuffChangeOfAmount).ToString() + " " + "Stuff", stars_2);
        }
        else if (stuffBasedAchievementAmount <= registerOfCellection.nrStuffCollected)
        {
            ChangeAchievementValues(achievementsObj[resourceBasedAchievements_1.Count], stuffBasedAchievementName, "Colecteaza " + (stuffBasedAchievementAmount + stuffChangeOfAmount).ToString() + " " + "Stuff", stars_1);
        }
        else
        {
            ChangeAchievementValues(achievementsObj[resourceBasedAchievements_1.Count], stuffBasedAchievementName, "Colecteaza " + (stuffBasedAchievementAmount).ToString() + " " + "Stuff", stars_0);
        }

        if (craftedStuffBasedAchievementAmount + 2 * craftedStuffChangeOfAmount <= registerOfCellection.nrStuffCrafted)
        {
            ChangeAchievementValues(achievementsObj[resourceBasedAchievements_1.Count + 1], craftedStuffBasedAchievementName, "Completat!", stars_3);
        }
        else if (craftedStuffBasedAchievementAmount + craftedStuffChangeOfAmount <= registerOfCellection.nrStuffCrafted)
        {
            ChangeAchievementValues(achievementsObj[resourceBasedAchievements_1.Count + 1], craftedStuffBasedAchievementName, "Craft-eaza " + (craftedStuffBasedAchievementAmount + 2 * craftedStuffChangeOfAmount).ToString() + " " + "Stuff", stars_2);
        }
        else if (craftedStuffBasedAchievementAmount <= registerOfCellection.nrStuffCrafted)
        {
            ChangeAchievementValues(achievementsObj[resourceBasedAchievements_1.Count + 1], craftedStuffBasedAchievementName, "Craft-eaza " + (craftedStuffBasedAchievementAmount + craftedStuffChangeOfAmount).ToString() + " " + "Stuff", stars_1);
        }
        else
        {
            ChangeAchievementValues(achievementsObj[resourceBasedAchievements_1.Count + 1], craftedStuffBasedAchievementName, "Craft-eaza " + (craftedStuffBasedAchievementAmount).ToString() + " " + "Stuff", stars_0);
        }
    }

    void ChangeAchievementValues(GameObject obj, string name, string task, Sprite sprite)
    {
        obj.transform.GetChild(0).GetComponent<Text>().text = name;
        obj.transform.GetChild(1).GetComponent<Text>().text = task;
        obj.transform.GetChild(2).GetComponent<Image>().sprite = sprite;
    }

    void Save()
    {
        string nrResourcesCollected = "";
        for (int i=0; i<registerOfCellection.nrResourcesCollected.Count; i++)
        {
            nrResourcesCollected += registerOfCellection.nrResourcesCollected[i].ToString() + '~';
        }

        PlayerPrefs.SetString("nrResourcesCollected", nrResourcesCollected);
        PlayerPrefs.SetString("nrStuffCollected", registerOfCellection.nrStuffCollected.ToString());
        PlayerPrefs.SetString("nrStuffCrafted", registerOfCellection.nrStuffCrafted.ToString());
    }

    void Load()
    {
        string[] saved = PlayerPrefs.GetString("nrResourcesCollected").Split('~');
        for (int i=0; i < saved.Length - 1; i++)
        {
            registerOfCellection.nrResourcesCollected[i] = int.Parse(saved[i]);
        }

        registerOfCellection.nrStuffCollected = int.Parse(PlayerPrefs.GetString("nrStuffCollected"));
        registerOfCellection.nrStuffCrafted = int.Parse(PlayerPrefs.GetString("nrStuffCrafted"));
    }
}
