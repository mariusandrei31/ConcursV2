using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public MainController mainController;

    private void OnCollisionStay2D(Collision2D collision)
    {
        mainController.target = mainController.playerTransform.localPosition;
    }
}
