using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinBattleState : BattleGameState
{
    [SerializeField] GameObject WinUI;
    public override void Enter()
    {
        Debug.Log("Win State Enter");
        WinUI.SetActive(true);


        //hook into events
        //StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        WinUI.SetActive(false);

        //unhook from events
        //StateMachine.Input.PressedConfirm -= OnPressedConfirm;

        Debug.Log("Win State Exit");
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<SetupBattleState>();
        //change to enemy turn state
    }

    public void NewGame()
    {
        StateMachine.ChangeState<SetupBattleState>();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
