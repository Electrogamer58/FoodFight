using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseBattleState : BattleGameState
{
    [SerializeField] GameObject LoseUI;
    public override void Enter()
    {
        Debug.Log("Lose State Enter");
        LoseUI.SetActive(true);


        //hook into events
        //StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        LoseUI.SetActive(false);

        //unhook from events
        //StateMachine.Input.PressedConfirm -= OnPressedConfirm;

        Debug.Log("Lose State Exit");
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<SetupBattleState>();
        //change to enemy turn state
    }

    public void Restart()
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

