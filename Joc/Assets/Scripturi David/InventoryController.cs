using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public MainController mainController;

    public ResourcesDatabase resourcesDatabase;
    public ResourcesStorage resourcesStorage;

    public StuffDatabase stuffDatabase;

    public RegisterOfCellection registerOfCellection;

    Dictionary<int, string> getName;
    Dictionary<string, int> getID;

    Dictionary<string, Sprite> getSprite;

    public List<Description> resourceDescription = new List<Description>();
    public List<Description> stuffDescription = new List<Description>();
    Dictionary<string, Description> getResouceDescription;
    Dictionary<string, Description> getStuffDescription;


    public Text _resourceName;
    public Text _stuffName;
    public Text _resourceDescription;
    public Text _stuffDescription;

    public Text stuf_name;
    public Text stuff_description;
    public Text crafting_requirements;

    List<InventorySlot> resources = new List<InventorySlot>();
    List<string> stuff = new List<string>();

    public GameObject ui_canvas;
    GraphicRaycaster ui_raycaster;
    PointerEventData click_data;
    List<RaycastResult> click_results;

    public Animator ISAnim;
    public Animator RPAnim;
    public Animator SPAnim;
    public Animator CPAnim;
    public Animator DHAnim;
    public Animator RDPAnim;
    public Animator SDPAnim;

    bool inventoryScreenOpen = false;
    bool stuffPageOpen = false;
    bool craftinPageOpen = false;
    public List<GameObject> slots = new List<GameObject>();

    public List<Sprite> stuffSprites = new List<Sprite>();
    List<GameObject> stuffObj = new List<GameObject>();

    public List<Recipe> recipes = new List<Recipe>();
    public List<string> stuffTags = new List<string>();
    Dictionary<string, Recipe> getRecipe;

    string currentSelectedStuff;

    public GameObject stuffPrefab;
    public Transform stuffParentTransform;
    public int linesNr, columnsNr;
    public int x_start, y_start;
    public int x_space, y_space;

    //private void OnApplicationQuit()
    //{
    //    Save();
    //}

    private void Start()
    {
        Repopulate();
        GetComponents();

        //Load();
    }

    private void Update()
    {
        IfClicked();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryScreenOpen)
                inventoryScreenOpen = false;
            else
                inventoryScreenOpen = true;

            stuffPageOpen = false;
            craftinPageOpen = false;

            bool open = ISAnim.GetBool("OpenInventoryScreen");

            DHAnim.SetBool("OpenDetailsHolder", false);
            if (open)
            {
                ClearStuff();

                ISAnim.SetBool("OpenInventoryScreen", false);
                mainController.actionPoints--;
                SetAnimVal(false, false, false);

                CloseResourceDescription();
                CloseStuffDescription();
            }
            else
            {
                UpdateResourcesPage();

                ISAnim.SetBool("OpenInventoryScreen", true);
                mainController.actionPoints++;
                SetAnimVal(true, false, false);
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

            if (resourcesDatabase.allResources.Contains(ui_element.name) && !inventoryScreenOpen)
            {
                AddResource(ui_element.name, 1);
                Destroy(ui_element);

                registerOfCellection.nrResourcesCollected[getID[ui_element.name]]++;

                PlayerPrefs.SetString("save", ui_element.name);
            }

            if (stuffDatabase.allStuff.Contains(ui_element.name) && !inventoryScreenOpen)
            {
                AddStuff(ui_element.name);
                Destroy(ui_element);

                registerOfCellection.nrStuffCollected++;
            }

            if (resourcesDatabase.allResources.Contains(ui_element.tag))
                OpenResourceDescription(ui_element.tag);


            if (stuffDatabase.allStuff.Contains(ui_element.tag) && stuffPageOpen)
                OpenStuffDescription(ui_element.tag);

            if (ui_element.name == "Recipe" && craftinPageOpen)
            {
                currentSelectedStuff = ui_element.tag;
                ShowCraftingDetails(ui_element.tag);
            }

            if (ui_element.name == "CraftingButton")
            {
                if (CanCraftStuff(currentSelectedStuff))
                {
                    CraftStuff(currentSelectedStuff);

                    registerOfCellection.nrStuffCrafted++;
                }
            }

            if (ui_element.name == "Resources")
            {
                craftinPageOpen = false;
                stuffPageOpen = false;

                DHAnim.SetBool("OpenDetailsHolder", false);

                UpdateResourcesPage();
                SetAnimVal(true, false, false);
                CloseStuffDescription();
                ClearStuff();
            }
            if (ui_element.name == "Stuff")
            {
                craftinPageOpen = false;
                stuffPageOpen = true;

                DHAnim.SetBool("OpenDetailsHolder", false);

                UpdateStuffPage();
                SetAnimVal(false, true, false);
                CloseResourceDescription();
            }
            if (ui_element.name == "Crafting")
            {
                craftinPageOpen = true;
                stuffPageOpen = false;

                SetAnimVal(false, false, true);
                CloseResourceDescription();
                CloseStuffDescription();
                ClearStuff();
            }
        }
    }

    void Repopulate()
    {
        getName = new Dictionary<int, string>();
        getID = new Dictionary<string, int>();

        getResouceDescription = new Dictionary<string, Description>();
        getStuffDescription = new Dictionary<string, Description>();
        for (int i = 0; i < resourcesDatabase.allResources.Count; i++)
        {
            getName[i] = resourcesDatabase.allResources[i];
            getID[resourcesDatabase.allResources[i]] = i;

            getResouceDescription[resourcesDatabase.allResources[i]] = resourceDescription[i];
        }

        getSprite = new Dictionary<string, Sprite>();
        for (int i = 0; i < stuffDatabase.allStuff.Count; i++)
        {
            getSprite[stuffDatabase.allStuff[i]] = stuffSprites[i];

            getStuffDescription[stuffDatabase.allStuff[i]] = stuffDescription[i];
        }

        getRecipe = new Dictionary<string, Recipe>();
        for (int i = 0; i < stuffTags.Count; i++)
            getRecipe[stuffTags[i]] = recipes[i];
    }

    void SetAnimVal(bool resources, bool stuff, bool crafting)
    {
        RPAnim.SetBool("OpenResourcesPage", resources);
        SPAnim.SetBool("OpenStuffPage", stuff);
        CPAnim.SetBool("OpenCraftingPage", crafting);
    }

    void OpenResourceDescription(string name)
    {
        _resourceName.text = name;
        _resourceDescription.text = getResouceDescription[name].description;

        RDPAnim.SetBool("OpenResourcesDescriptionPage", true);
    }

    void CloseResourceDescription()
    {
        RDPAnim.SetBool("OpenResourcesDescriptionPage", false);
    }

    void OpenStuffDescription(string name)
    {
        _stuffName.text = name;
        _stuffDescription.text = getStuffDescription[name].description;

        SDPAnim.SetBool("OpenStuffDescriptionPage", true);
    }

    void CloseStuffDescription()
    {
        SDPAnim.SetBool("OpenStuffDescriptionPage", false);
    }

    void AddResource(string name, int quntity)
    {
        int itemID = getID[name];
        for (int i = 0; i < resources.Count; i++)
        {
            if (resources[i].itemID == itemID)
            {
                resources[i].itemAmount++;
                return;
            }
        }

        InventorySlot inventorySlot = new InventorySlot();
        inventorySlot.itemID = itemID;
        inventorySlot.itemAmount = quntity;
        resources.Add(inventorySlot);
    }

    void AddStuff(string name)
    {
        stuff.Add(name);
    }

    void UpdateResourcesPage()
    {
        for (int i = 0; i < resourcesDatabase.allResources.Count; i++)
        {
            if (ContainsItem(resourcesDatabase.allResources[i]))
            {
                slots[i].GetComponentInChildren<Text>().text = GetInventorySlot(resourcesDatabase.allResources[i]).itemAmount.ToString();
            }
            else
            {
                slots[i].GetComponentInChildren<Text>().text = "0";
            }
        }
    }

    void UpdateStuffPage()
    {
        int nr = -1;
        for (int i = 0; i < linesNr; i++)
            for (int j = 0; j < columnsNr; j++)
            {
                nr++;
                if (nr < stuff.Count)
                {
                    GameObject obj = Instantiate(stuffPrefab);
                    obj.GetComponent<Image>().sprite = getSprite[stuff[nr]];
                    obj.tag = stuff[nr];
                    obj.transform.SetParent(stuffParentTransform);
                    obj.transform.localPosition = GetPosition(i, j);

                    stuffObj.Add(obj);
                }
            }
    }

    void ShowCraftingDetails(string name)
    {
        DHAnim.SetBool("OpenDetailsHolder", true);

        stuf_name.text = name;
        stuff_description.text = getStuffDescription[name].description;

        Recipe _recipe = getRecipe[name];
        string text = "";
        for (int i = 0; i < _recipe.recipe.Count; i++)
        {
            text += getName[_recipe.recipe[i].itemID];
            text += " x";
            text += _recipe.recipe[i].itemAmount.ToString();
            text += "\n";
        }
        crafting_requirements.text = text;
    }

    bool CanCraftStuff(string name)
    {
        Recipe _recipe = getRecipe[name];
        for (int i = 0; i < _recipe.recipe.Count; i++)
        {
            InventorySlot _slot = GetInventorySlot(getName[_recipe.recipe[i].itemID]);
            if (_slot == null)
                return false;
            else if (_slot.itemAmount < _recipe.recipe[i].itemAmount)
                return false;
        }
        return true;
    }

    void CraftStuff(string name)
    {
        AddStuff(name);

        Recipe _recipe = getRecipe[name];
        for (int i = 0; i < _recipe.recipe.Count; i++)
        {
            GetInventorySlot(getName[_recipe.recipe[i].itemID]).itemAmount -= _recipe.recipe[i].itemAmount;
        }
    }

    void ClearStuff()
    {
        for (int i = 0; i < stuffObj.Count; i++)
            Destroy(stuffObj[i]);
        stuffObj.Clear();
    }

    Vector3 GetPosition(int i, int j)
    {
        return new Vector3(j * x_space + x_start, -i * y_space + y_start, 0);
    }

    bool ContainsItem(string itemName)
    {
        for (int i = 0; i < resources.Count; i++)
        {
            if (resources[i].itemID == getID[itemName])
                return true;
        }
        return false;
    }

    InventorySlot GetInventorySlot(string itemName)
    {
        for (int i = 0; i < resources.Count; i++)
        {
            if (resources[i].itemID == getID[itemName])
                return resources[i];
        }
        return null;
    }

    void Save()
    {
        for (int i = 0; i < resourcesStorage.storage.Count; i++)
            resourcesStorage.storage[i] = 0;

        for (int i = 0; i < resources.Count; i++)
        {
            resourcesStorage.storage[resources[i].itemID] = resources[i].itemAmount;
        }

        string saveResources = "";
        for (int i = 0; i < resourcesStorage.storage.Count; i++)
            saveResources += resourcesStorage.storage[i].ToString() + '~';

        PlayerPrefs.SetString("saveResources", saveResources);

        string saveStuff = "";
        for (int i = 0; i < stuff.Count; i++)
            saveStuff += stuff[i] + '~';

        PlayerPrefs.SetString("saveStuff", saveStuff);
    }

    void Load()
    {
        if (PlayerPrefs.HasKey("saveResources"))
        {
            resources.Clear();
            string[] savedResources = PlayerPrefs.GetString("saveResources").Split('~');

            for (int i = 0; i < savedResources.Length - 1; i++)
            {
                if (int.Parse(savedResources[i]) > 0)
                    AddResource(getName[i], int.Parse(savedResources[i]));
            }

            stuff.Clear();
            string[] savedStuff = PlayerPrefs.GetString("saveStuff").Split('~');

            for (int i = 0; i < savedStuff.Length - 1; i++)
            {
                stuff.Add(savedStuff[i]);
            }
        }
    }
}
