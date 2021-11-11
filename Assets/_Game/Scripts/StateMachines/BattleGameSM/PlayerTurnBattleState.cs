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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_battleUIController.AttackButton);

        //reset menus
        _battleUIController.ToSpellMenu(false);
        _battleUIController.ToLearnMenu(false);
        _battleUIController.AttackButton.SetActive(true);
        _battleUIController.LearnButton.SetActive(true);
        _battleUIController.SpellButton.SetActive(true);

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
        if (EventSystem.current.currentSelectedGameObject == _battleUIController.AttackButton) //pressed attack button
        {
            //IF NOT POSSIBLE, COMMIT ACTION AND CHANGE TO ENEMY TURN STATE
            DelayHelper.DelayAction(this, GoToEnemyState, 1f);
            //TODO PLAY ATTACK ANIMATION
             //change to enemy turn state
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.SpellButton) //pressed spell button
        {
            //if (_battleUIController.SpellPanel.active)
            _battleUIController.ToSpellMenu(true);
            _battleUIController.AttackButton.SetActive(false);
            _battleUIController.LearnButton.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.FirstSpell);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.LearnButton) //pressed Learn button
        {
            //if (_battleUIController.SpellPanel.active)
            _battleUIController.ToLearnMenu(true);
            _battleUIController.AttackButton.SetActive(false);
            _battleUIController.SpellButton.SetActive(false);

            //EventSystem.current.SetSelectedGameObject(null);
            //EventSystem.current.SetSelectedGameObject(_battleUIController.FirstLearnOption);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.LearnSpellButton) //wants Learn spell
        {
            _battleUIController.LearnSpellMenu.SetActive(true);
            _battleUIController.LearnStyleMenu.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.FirstLearnSpellOption);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.LearnStyleButton) //wants Learn Style
        {
            _battleUIController.LearnSpellMenu.SetActive(false);
            _battleUIController.LearnStyleMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.FirstLearnStyleOption);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.BackButton1 || EventSystem.current.currentSelectedGameObject == _battleUIController.BackButton2) //going back
        {
            _battleUIController.LearnSpellMenu.SetActive(false);
            _battleUIController.LearnStyleMenu.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.LearnButton);
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

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.LearnButton)
        {
            //if (_battleUIController.SpellPanel.active)
            _battleUIController.ToLearnMenu(false);
            _battleUIController.AttackButton.SetActive(true);
            _battleUIController.SpellButton.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.LearnButton);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.BackButton1 || EventSystem.current.currentSelectedGameObject == _battleUIController.BackButton2) //going back
        {
            _battleUIController.LearnSpellMenu.SetActive(false);
            _battleUIController.LearnStyleMenu.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.LearnButton);
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

    public void GoToEnemyState()
    {
        StateMachine.ChangeState<EnemyTurnBattleState>();
    }

   

   

}
