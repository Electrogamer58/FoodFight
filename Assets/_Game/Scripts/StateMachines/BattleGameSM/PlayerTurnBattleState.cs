using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerTurnBattleState : BattleGameState
{
    [SerializeField] Text _playerTurnTextUI = null;
    [SerializeField] GameObject _playerTurnUI = null;
    [SerializeField] BattleGameUIController _battleUIController = null;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn:... Entering");
        _playerTurnTextUI.gameObject.SetActive(true);
        _playerTurnUI.SetActive(true);

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
        _playerTurnUI.SetActive(false);

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
        if (EventSystem.current.currentSelectedGameObject == _battleUIController.AttackButton)
        {
            //IF NOT POSSIBLE, COMMIT ACTION AND CHANGE TO ENEMY TURN STATE
            DelayHelper.DelayAction(this, GoToEnemyState, 4f);
            //TODO PLAY ATTACK ANIMATION
             //change to enemy turn state
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.SpellButton)
        {
            _battleUIController.ToSpellMenu(true);
            _battleUIController.AttackButton.SetActive(false);
            _battleUIController.LearnButton.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.FirstSpell);
        }
    }

    void OnPressedCancel()
    {
        //GO BACK ONE MENU IF POSSIBLE
        if (EventSystem.current.currentSelectedGameObject == _battleUIController.SpellButton)
        {
            _battleUIController.ToSpellMenu(false);
            _battleUIController.AttackButton.SetActive(true);
            _battleUIController.LearnButton.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.SpellButton);
        }
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

    void GoToEnemyState()
    {
        StateMachine.ChangeState<EnemyTurnBattleState>();
    }
    
}
