using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleGameUIController : MonoBehaviour
{
    [SerializeField] Text _enemyThinkingTextUI = null;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject SettingsPanel;

    BattleGameSM GameState;
    bool Paused = false;

    private void OnEnable()
    {
        EnemyTurnBattleState.EnemyTurnBegan += OnEnemyTurnBegan;
        EnemyTurnBattleState.EnemyTurnEnded += OnEnemyTurnEnded;
        GameState = FindObjectOfType<BattleGameSM>();
        GameState.Input.PressedP += PressedPause;
    }

    private void OnDisable()
    {
        EnemyTurnBattleState.EnemyTurnBegan -= OnEnemyTurnBegan;
        EnemyTurnBattleState.EnemyTurnEnded -= OnEnemyTurnEnded;
        GameState.Input.PressedP -= PressedPause;
    }

    private void Start()
    {
        // make sure text is disabled on start
        _enemyThinkingTextUI.gameObject.SetActive(false);
    }

    void OnEnemyTurnBegan()
    {
        _enemyThinkingTextUI.gameObject.SetActive(true);
    }

    void OnEnemyTurnEnded()
    {
        _enemyThinkingTextUI.gameObject.SetActive(false);
    }

    public void ToSettings()
    {
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void ToPauseMenu()
    {
        PausePanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void PressedPause()
    {
        Paused = !Paused;
        PausePanel.SetActive(Paused);
        if (Paused == true)
            Time.timeScale = 0;
        else if (Paused == false)
            Time.timeScale = 1;
    }


}
