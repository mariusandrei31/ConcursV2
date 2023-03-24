using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonArea : MonoBehaviour
{
    public SpriteRenderer Imagine;
    public Sprite original, hover;
    private void OnMouseEnter()
    {
        Imagine.sprite = hover;
    }
    private void OnMouseExit()
    {
        Imagine.sprite = original;
    }
}
