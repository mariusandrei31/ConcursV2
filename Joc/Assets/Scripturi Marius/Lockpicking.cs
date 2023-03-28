using System.Collections;
using System.Collections.Generic;
using Unity.Rendering.HybridV2;
using Unity.VisualScripting;
using UnityEngine;

public class Lockpicking : MonoBehaviour
{
    public NavigationScenesController navigationScenesController;
    public GameObject lastPage;

    Vector2 right;
    public Vector2 pozInit;
    public float speed=5f;
    public List<float> pozitii = new List<float>();
    public List<Rigidbody2D> rb = new List<Rigidbody2D>();
    public float mPlayer, mPin;
    int k=0;
    bool startMove = false;
    private void Start()
    {
        right.x = 1;
        right.y = 0;
    }
    private void Update()
    {
        if (k == pozitii.Capacity)
        {
            navigationScenesController.currentScene.SetActive(true);

            lastPage.SetActive(true);

            this.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startMove = true;
            if (rb[k].position.x - mPlayer / 2 >= pozitii[k] - mPin / 2 && rb[k].position.x + mPlayer / 2 <= pozitii[k] + mPin / 2)
                k++;
            else
            {
                rb[k].position = pozInit;
            }
        }
    }

    private void FixedUpdate()
    {
        if (k < pozitii.Capacity && startMove)
        {
            rb[k].MovePosition(rb[k].position + right * speed * Time.deltaTime);
        }
    }   
}
