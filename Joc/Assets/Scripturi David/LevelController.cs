using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [HideInInspector]
    public bool setupLevel = true;

    public List<GameObject> streetArrows_Level1 = new List<GameObject>();
    public List<GameObject> streetArrows_Level2 = new List<GameObject>();
    public List<GameObject> streetArrows_Level3 = new List<GameObject>();
    public List<GameObject> streetArrows_Level4 = new List<GameObject>();
    public List<GameObject> streetArrows_Level5 = new List<GameObject>();

    public GameObject targetArrow_Level1;
    public GameObject targetArrow_Level2;
    public GameObject targetArrow_Level3;
    public GameObject targetArrow_Level4;
    public GameObject targetArrow_Level5;

    public List<GameObject> houses = new List<GameObject>();
    public List<GameObject> backSceene = new List<GameObject>();

    public List<Transform> playerTransforms = new List<Transform>();
    public List<Transform> focusPointTransforms = new List<Transform>();
    public List<Animator> animators = new List<Animator>();
    public List<Transform> spawnpoint = new List<Transform>();

    List<GameObject> obj = new List<GameObject>();
    GameObject arrow_Obj = null;

    private void Update()
    {
        if (setupLevel)
        {
            setupLevel = false;

            for (int i=0; i<obj.Count; i++)
                obj[i].GetComponent<SpriteRenderer>().color = Color.white;

            obj.Clear();

            if (arrow_Obj)
                arrow_Obj.name = "a";

            switch(int.Parse(PlayerPrefs.GetString("Level")))
            {
                case 1:
                    for (int i = 0; i < streetArrows_Level1.Count; i++)
                    {
                        streetArrows_Level1[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        obj.Add(streetArrows_Level1[i]);
                    }
                    targetArrow_Level1.name = "-15";
                    arrow_Obj = targetArrow_Level1;
                    break;
                case 2:
                    for (int i = 0; i < streetArrows_Level2.Count; i++)
                    {
                        streetArrows_Level2[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        obj.Add(streetArrows_Level2[i]);
                    }
                    targetArrow_Level2.name = "-15";
                    arrow_Obj = targetArrow_Level2;
                    break;
                case 3:
                    for (int i = 0; i < streetArrows_Level3.Count; i++)
                    {
                        streetArrows_Level3[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        obj.Add(streetArrows_Level3[i]);
                    }
                    targetArrow_Level3.name = "-15";
                    arrow_Obj = targetArrow_Level3;
                    break;
                case 4:
                    for (int i = 0; i < streetArrows_Level4.Count; i++)
                    {
                        streetArrows_Level4[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        obj.Add(streetArrows_Level4[i]);
                    }
                    targetArrow_Level4.name = "-15";
                    arrow_Obj = targetArrow_Level4;
                    break;
                case 5:
                    for (int i = 0; i < streetArrows_Level5.Count; i++)
                    {
                        streetArrows_Level5[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        obj.Add(streetArrows_Level5[i]);
                    }
                    targetArrow_Level5.name = "-15";
                    arrow_Obj = targetArrow_Level5;
                    break;
            }
        }
    }
}
