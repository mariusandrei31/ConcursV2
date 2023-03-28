using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public MainController mainController;
    public DialogController dialogController;

    public ConversationStructure conversation;

    private void OnCollisionStay2D(Collision2D collision)
    {
        mainController.target = mainController.playerTransform.localPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "corpse_0")
        {
            dialogController.StartText(conversation);

            mainController.loadSceene = true;
        }
    }
}
