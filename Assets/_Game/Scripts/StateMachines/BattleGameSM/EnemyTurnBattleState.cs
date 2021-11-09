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

    [Header("My Animation and Stuff")]
    [SerializeField] Animation myAnimation = null;

    [Header("Attack Stats")]
    public int hitDC = 30;
    public int attackDamage = 40;

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
        attackDamage += UnityEngine.Random.Range(1, 30);

        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        //PERFORM ACTION (AND ALL LOGIC ATTRIBUTED TO IT)
        if (actionRoll == 1)
        {
            //ATTACK
            int myRoll = UnityEngine.Random.Range(1, 101);
            _commandStack.ExecuteCommand(new Attack_Player(myAnimation, myRoll, hitDC, attackDamage));

        }
        if (actionRoll == 2)
        {
            //HEAL
            Debug.Log("Heal!");
        }
        //PLAY ACTION ANIMATION

        yield return new WaitForSeconds(pauseDuration);
        EnemyTurnEnded?.Invoke();
        //turn over, go back to Player.
        StateMachine.ChangeState<PlayerTurnBattleState>();
    }
}
