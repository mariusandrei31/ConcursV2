using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<GameObject> streetArrows_Level1 = new List<GameObject>();
    public List<GameObject> streetArrows_Level2 = new List<GameObject>();
    public List<GameObject> streetArrows_Level3 = new List<GameObject>();
    public List<GameObject> streetArrows_Level4 = new List<GameObject>();
    public List<GameObject> streetArrows_Level5 = new List<GameObject>();

    public List<GameObject> targetArrow = new List<GameObject>();

    public List<GameObject> houses = new List<GameObject>();

    public List<Transform> playerTransforms = new List<Transform>();
    public List<Transform> focusPointTransforms = new List<Transform>();
    public List<Animator> animators = new List<Animator>();
    public List<Transform> spawnpoint = new List<Transform>();

    List<GameObject> obj = new List<GameObject>();
    GameObject arrow_Obj = null;

    private void Start()
    {
        for (int i = 0; i < obj.Count; i++)
            obj[i].GetComponent<SpriteRenderer>().color = Color.white;

        obj.Clear();

        if (arrow_Obj)
        {
            arrow_Obj.name = "-13";
        }

        switch (int.Parse(PlayerPrefs.GetString("Level")))
        {
            case 1:
                for (int i = 0; i < streetArrows_Level1.Count; i++)
                {
                    streetArrows_Level1[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    obj.Add(streetArrows_Level1[i]);
                }
                targetArrow[0].name = "-15";
                targetArrow[0].GetComponent<SpriteRenderer>().color = Color.red;
                arrow_Obj = targetArrow[0];
                break;
            case 2:
                for (int i = 0; i < streetArrows_Level2.Count; i++)
                {
                    streetArrows_Level2[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    obj.Add(streetArrows_Level2[i]);
                }
                targetArrow[1].name = "-15";
                targetArrow[1].GetComponent<SpriteRenderer>().color = Color.red;
                arrow_Obj = targetArrow[1];
                break;
            case 3:
                for (int i = 0; i < streetArrows_Level3.Count; i++)
                {
                    streetArrows_Level3[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    obj.Add(streetArrows_Level3[i]);
                }
                targetArrow[2].name = "-15";
                targetArrow[2].GetComponent<SpriteRenderer>().color = Color.red;
                arrow_Obj = targetArrow[2];
                break;
            case 4:
                for (int i = 0; i < streetArrows_Level4.Count; i++)
                {
                    streetArrows_Level4[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    obj.Add(streetArrows_Level4[i]);
                }
                targetArrow[3].name = "-15";
                targetArrow[3].GetComponent<SpriteRenderer>().color = Color.red;
                arrow_Obj = targetArrow[3];
                break;
            case 5:
                for (int i = 0; i < streetArrows_Level5.Count; i++)
                {
                    streetArrows_Level5[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    obj.Add(streetArrows_Level5[i]);
                }
                targetArrow[4].name = "-15";
                targetArrow[4].GetComponent<SpriteRenderer>().color = Color.red;
                arrow_Obj = targetArrow[4];
                break;
        }
    }
}
