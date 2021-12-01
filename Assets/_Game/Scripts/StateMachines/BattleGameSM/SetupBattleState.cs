using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBattleState : BattleGameState
{
    [SerializeField] int _startingSugar = 10;
    [SerializeField] int _numberOfPlayers = 2;
    [SerializeField] Health _playerHealth = null;
    [SerializeField] GameObject WinUI;
    [SerializeField] PlayerHUD _playerHUD = null;

    bool _activated = true;

    public override void Enter()
    {
        PlayerScore.EnemiesKilled = 0;
        PlayerScore.Tastecoins = 20;
        Debug.Log("Setup:... Entering");
        WinUI.SetActive(false);
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        //CANT change state while still in Enter()/Exit() transition! DONT put ChangeState<> here.
        _activated = false;
        _playerHealth._currentHealth = _playerHealth._maxHealth;
        _playerHealth._currentSugar = _playerHealth._maxSugar;
        
        PlayerTurnBattleState.inGame = true;
        _playerHUD.UpdateHealthBar();
        
    }

    public override void Tick()
    {
        //hacky for a demo, usually would want to have delays or Input.
        if (!_activated)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnBattleState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exiting...");

        
    }
}
