using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [HideInInspector]
    public int actionPoints = 0;
    [HideInInspector]
    public bool canOpenScreens = true;
    [HideInInspector]
    public bool canExit = false;

    public Animator PMAnim;

    public Transform playerTransform;
    public Transform focusPointTransform;

    [HideInInspector]
    public Vector3 target;
    public float moveSpeed;

    [HideInInspector]
    public bool canAnimate = false;

    private void Update()
    {
        if (actionPoints == 0)
            GetTargetPosition();

        if (canAnimate)
        {
            if (playerTransform.localPosition == target)
                PMAnim.SetBool("PlayerMove", false);
            else
                PMAnim.SetBool("PlayerMove", true);
        }

        //Click();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //void Click()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
    //        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

    //        if (hit.collider)
    //        {
    //            Debug.Log(hit.collider.gameObject.name);
    //            if (hit.collider.gameObject.name == "Floor")
    //            {
    //                target = GetPosition();
    //            }
    //        }
    //    }
    //}

    void GetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetPosition();

            if (target.x > playerTransform.localPosition.x)
                playerTransform.localEulerAngles = new Vector3(0, 0, 0);
            else
                playerTransform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Move()
    {
        playerTransform.localPosition = Vector3.MoveTowards(playerTransform.localPosition, target, Time.deltaTime * moveSpeed);

        float scaleFactor = focusPointTransform.localPosition.y - playerTransform.localPosition.y;
        scaleFactor /= 10.0f;
        playerTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }

    Vector3 GetPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pos.x, pos.y, 1);
    }
}
