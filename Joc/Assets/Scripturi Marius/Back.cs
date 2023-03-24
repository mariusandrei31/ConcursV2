using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back:MonoBehaviour
{
    public GameObject Meniu, Optiuni,back;
    public SpriteRenderer imagine;
    public Sprite original;
    private void OnMouseDown()
    {
        Meniu.SetActive(true);
        Optiuni.SetActive(false);
        back.SetActive(false);
        imagine.sprite = original;
    }
}
