using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnBattleState : BattleGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    CommandStack _commandStack = new CommandStack();

    [SerializeField] float _pauseDuration = 1.5f;

    Enemy currentEnemy;

    

    public override void Enter()
    {
        Debug.Log("Enemy Turn: ...Enter");
        EnemyTurnBegan?.Invoke();
        currentEnemy = FindObjectOfType<Enemy>();

        StartCoroutine(currentEnemy.EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
    }


    public void ChangeState()
    {
        StateMachine.ChangeState<PlayerTurnBattleState>();
    }
}
