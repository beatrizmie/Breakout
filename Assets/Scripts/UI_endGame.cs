using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_endGame : MonoBehaviour
{
    public Text message;

    GameManager gm;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();

        if (gm.lives > 1)
        {
            message.text = "Parabéns, você Ganhou!!!";
        }
        else
        {
           message.text = "Que pena, você Perdeu!! \n Tente novamente :)";
        }
    }

    public void ReturnToMainMenu()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }
}
