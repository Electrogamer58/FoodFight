using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnBattleState : BattleGameState
{
    [SerializeField] Text _playerTurnTextUI = null;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn:... Entering");
        _playerTurnTextUI.gameObject.SetActive(true);
            
        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

        //hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedCancel += OnPressedCancel;
        StateMachine.Input.PressedLeft += OnPressedLeft;
        StateMachine.Input.PressedRight += OnPressedRight;

    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        
        //unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedCancel -= OnPressedCancel;
        StateMachine.Input.PressedLeft -= OnPressedLeft;
        StateMachine.Input.PressedRight -= OnPressedRight;

        Debug.Log("Player Turn:... Exiting");
    }

    void OnPressedConfirm()
    {
        //GO FORWARD ONE MENU IF POSSIBLE

        //IF NOT POSSIBLE, COMMIT ACTION AND CHANGE TO ENEMY TURN STATE
        StateMachine.ChangeState<EnemyTurnBattleState>(); //change to enemy turn state
    }

    void OnPressedCancel()
    {
        //GO BACK ONE MENU IF POSSIBLE
    }

    void OnPressedLeft()
    {
        //MOVE SELECTION OVER ONE LEFT, SCROLL IF AT EDGE
    }

    void OnPressedRight()
    {
        //MOVE SELECTION OVER ONE RIGHT, SCROLL IF AT EDGE
    }

    public void Win() //TEMP but may be used later for same logic
    {
        StateMachine.ChangeState<WinBattleState>();
    }

    public void Lose()
    {
        StateMachine.ChangeState<LoseBattleState>();
    }
    
}
