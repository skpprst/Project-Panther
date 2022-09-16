using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] public CanvasGroup enemyTurnFade;
    [SerializeField] public CanvasGroup playerTurnFade;

    public void eTurnFadeOut()
    {
        enemyTurnFade.alpha -=Time.deltaTime;
    }

    public void pTurnFadeOut()
    {
        playerTurnFade.alpha -= Time.deltaTime;

    }


}
