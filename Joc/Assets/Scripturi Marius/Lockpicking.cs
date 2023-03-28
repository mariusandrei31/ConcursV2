using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpicking : MonoBehaviour
{
    Vector2 right;
    public Vector2 pozInit;
    public float speed=5f;
    public List<float> pozitii = new List<float>();
    public List<Rigidbody2D> rb = new List<Rigidbody2D>();
    public float mPlayer, mPin;
    int k=0;
    private void Start()
    {
        right.x = 1;
        right.y = 0;
    }
    private void Update()
    {
        if (k == pozitii.Capacity)
            Debug.Log("Bravo frate");
        if (Input.GetKeyDown(KeyCode.Space))
            if (rb[k].position.x - mPlayer / 2 >= pozitii[k] - mPin / 2 && rb[k].position.x + mPlayer / 2 <= pozitii[k] + mPin / 2)
                k++;
            else
            {
                Debug.Log("Esti un fraier");
                rb[k].position = pozInit;
            }
    }

    private void FixedUpdate()
    {
        rb[k].MovePosition(rb[k].position +right*speed*Time.deltaTime);
    }   
}
