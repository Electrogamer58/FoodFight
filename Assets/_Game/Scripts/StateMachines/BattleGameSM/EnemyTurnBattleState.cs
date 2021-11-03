using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnBattleState : BattleGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] float _pauseDuration = 1.5f;

    int actionRoll = 0;

    public override void Enter()
    {
        Debug.Log("Enemy Turn: ...Enter");
        EnemyTurnBegan?.Invoke();

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        //CHOOSE RANDOM ACTION: ATTACK/HEAL
        actionRoll = UnityEngine.Random.Range(1, 3);

        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        //PERFORM ACTION (AND ALL LOGIC ATTRIBUTED TO IT)
        if (actionRoll == 1)
        {
            //ATTACK
        }
        if (actionRoll == 2)
        {
            //HEAL
        }
        //PLAY ACTION ANIMATION
        EnemyTurnEnded?.Invoke();
        //turn over, go back to Player.
        StateMachine.ChangeState<PlayerTurnBattleState>();
    }
}
