using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, MISSILE, ENEMYTURN, WON, LOST }



public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    [SerializeField] private GameObject startCamera;


    void Start()
    {
        // startCamera.SetActive(true);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        Time.timeScale = 1;


    }

    IEnumerator SetupBattle()
    {
        startCamera.SetActive(false);
        yield return new WaitForSeconds(5f);
        state = BattleState.ENEMYTURN;
    }

    // void EnemyTurn()
    // {
    //     enemyAI.searchAndAttack();
    // }

}
