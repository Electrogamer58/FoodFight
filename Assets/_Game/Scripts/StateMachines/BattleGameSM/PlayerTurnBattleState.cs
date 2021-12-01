using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerTurnBattleState : BattleGameState
{
    public static event Action PlayerTurnStarted = delegate { };

    [SerializeField] Text _playerTurnTextUI = null;
    [SerializeField] GameObject _playerTurnUI = null;
    [SerializeField] Health _playerHealth = null;
    [SerializeField] BattleGameUIController _battleUIController = null;
    [SerializeField] SpellController _spellController = null;
    [SerializeField] public InputController _inputController = null;

    [Header("Menu Buttons")]
    [SerializeField] GameObject LoseButton;
    [SerializeField] GameObject WinButton;

    int _playerTurnCount = 0;
    bool inLearnMenu = false;
    bool inStyleMenu = false;
    bool inSpellMenu = false;
    bool UndidPickle = true;
    static public bool inGame = true;

    private void OnEnable()
    {
        _playerHealth.Died += Lose;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= Lose;
    }

    public override void Enter()
    {
        Debug.Log("Player Turn:... Entering");
        PlayerTurnStarted?.Invoke();

        _playerTurnTextUI.gameObject.SetActive(true);
        _playerTurnUI.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_battleUIController.AttackButton);

        _battleUIController.canChoose = true;
        _inputController.inputEnabled = true;

        _playerHealth._currentSugar += 1;
        _playerHealth.UseMagic();

        if (_playerHealth._currentSugar > 10)
            _playerHealth._currentSugar = 10;

        if (_spellController.spellTurnCount > 0)
        {
            UndidPickle = false;
            _spellController.spellTurnCount -= 1;
        }

        if (_spellController.spellTurnCount == 0 && !UndidPickle)
        {
            _spellController.UndoPickle();
            UndidPickle = true;
        }


        //hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedCancel += OnPressedCancel;
        StateMachine.Input.PressedLeft += OnPressedLeft;
        StateMachine.Input.PressedRight += OnPressedRight;
    }

    public override void Tick()
    {
        if (PlayerScore.EnemiesKilled == 2)
        {
            Win();
            Debug.Log("Win!");
        }
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        //_playerTurnUI.SetActive(false);

        ResetMenus();
        _battleUIController._UIAnimator.SetBool("onAttack", true);
        _battleUIController._UIAnimator.SetBool("onSpell", false);
        _battleUIController._UIAnimator.SetBool("onLearn", false);
        _battleUIController.canChoose = false;
        _inputController.inputEnabled = false;

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
            DelayHelper.DelayAction(this, GoToEnemyState, 10f);
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
            inLearnMenu = true; 
            _battleUIController.AttackButton.SetActive(false);
            _battleUIController.SpellButton.SetActive(false);

            //EventSystem.current.SetSelectedGameObject(null);
            //EventSystem.current.SetSelectedGameObject(_battleUIController.FirstLearnOption);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.LearnSpellButton) //wants Learn spell
        {
            _battleUIController.LearnSpellMenu.SetActive(true);
            inSpellMenu = true;
            _battleUIController.LearnStyleMenu.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.FirstLearnSpellOption);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.LearnStyleButton) //wants Learn Style
        {
            _battleUIController.LearnSpellMenu.SetActive(false);
            _battleUIController.LearnStyleMenu.SetActive(true);
            inStyleMenu = true;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.FirstLearnStyleOption);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.BackButton1 || EventSystem.current.currentSelectedGameObject == _battleUIController.BackButton2) //going back
        {
            _battleUIController.LearnSpellMenu.SetActive(false);
            _battleUIController.LearnStyleMenu.SetActive(false);
            inSpellMenu = false;
            inStyleMenu = false;

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
            inLearnMenu = false;
            _battleUIController.AttackButton.SetActive(true);
            _battleUIController.SpellButton.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_battleUIController.LearnButton);
        }

        if (EventSystem.current.currentSelectedGameObject == _battleUIController.BackButton1 || EventSystem.current.currentSelectedGameObject == _battleUIController.BackButton2) //going back
        {
            _battleUIController.LearnSpellMenu.SetActive(false);
            _battleUIController.LearnStyleMenu.SetActive(false);
            inSpellMenu = false;
            inStyleMenu = false;

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
        inGame = false;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(WinButton);
    }

    public void Lose()
    {
        StateMachine.ChangeState<LoseBattleState>();
        inGame = false;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(LoseButton);
    }

    public void GoToEnemyState()
    {
        if (inGame)
        StateMachine.ChangeState<EnemyTurnBattleState>();
    }

    public void RefreshState()
    {
        StateMachine.ChangeState<PlayerTurnBattleState>();
    }

    private void ResetMenus()
    {
        //reset menus
        _battleUIController.ToSpellMenu(false);
        _battleUIController.ToLearnMenu(false);
        _battleUIController.LearnSpellMenu.SetActive(false);
        _battleUIController.LearnStyleMenu.SetActive(false);
        //set Original 3 back to active
        _battleUIController.AttackButton.SetActive(true);
        _battleUIController.LearnButton.SetActive(true);
        _battleUIController.SpellButton.SetActive(true);
    }
    

   

   

}
