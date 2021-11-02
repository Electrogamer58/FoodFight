using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleGameSM))]
public class BattleGameState : State
{
   protected BattleGameSM StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = GetComponent<BattleGameSM>();
    }
}
