using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameSM : StateMachine
{
    [SerializeField] InputController _input;
    public InputController Input => _input;

    void Start()
    {
        ChangeState<SetupBattleState>();
    }

}
