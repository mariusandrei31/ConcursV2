using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButonOptiuni : MonoBehaviour
{
    public GameObject Meniu, Optiuni,Back;
    public SpriteRenderer imagine;
    public Sprite original;
    private void OnMouseDown()
    {
        Meniu.SetActive(false);
        Optiuni.SetActive(true);
        Back.SetActive(true);
        imagine.sprite = original;
    }
}
