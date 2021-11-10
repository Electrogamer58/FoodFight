using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BattleGameUIController : MonoBehaviour
{
    [SerializeField] Text _enemyThinkingTextUI = null;
    [SerializeField] Animator _UIAnimator = null;

    [Header("Menu Panels")]
    [SerializeField] GameObject battlePanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] public GameObject SpellPanel;
    [SerializeField] public GameObject LearnPanel;
    [SerializeField] public GameObject LearnSpellMenu;
    [SerializeField] public GameObject LearnStyleMenu;

    [Header("Buttons to Start Menu On")]
    [SerializeField] GameObject battleFirstButton;
    [SerializeField] GameObject pauseFirstButton;
    [SerializeField] GameObject settingsFirstButton;

    [Header("Battle UI Buttons")]
    [SerializeField] public GameObject AttackButton;
    [SerializeField] public GameObject SpellButton;
    [SerializeField] public GameObject LearnButton;
    [SerializeField] public GameObject FirstSpell;
    [SerializeField] public GameObject FirstLearnOption;
    [SerializeField] public GameObject LearnSpellButton;
    [SerializeField] public GameObject FirstLearnSpellOption;
    [SerializeField] public GameObject LearnStyleButton;
    [SerializeField] public GameObject FirstLearnStyleOption;


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

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == AttackButton)
        {
            _UIAnimator.SetBool("onAttack", true);
            _UIAnimator.SetBool("onSpell", false);
            _UIAnimator.SetBool("onLearn", false);
        }

        if (EventSystem.current.currentSelectedGameObject == SpellButton)
        {
            _UIAnimator.SetBool("onAttack",false);
            _UIAnimator.SetBool("onSpell", true);
            _UIAnimator.SetBool("onLearn", false);
        }

        if (EventSystem.current.currentSelectedGameObject == LearnButton)
        {
            _UIAnimator.SetBool("onAttack",false);
            _UIAnimator.SetBool("onSpell", false);
            _UIAnimator.SetBool("onLearn", true);
        }
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    public void ToPauseMenu()
    {
        PausePanel.SetActive(true);
        SettingsPanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ToSpellMenu(bool active)
    {
        SpellPanel.SetActive(active);
    }

    public void ToLearnMenu(bool active)
    {
        LearnPanel.SetActive(active);
    }

    public void PressedPause()
    {
        Paused = !Paused;
        PausePanel.SetActive(Paused);
        //clear event system selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);


        if (Paused == true)
            Time.timeScale = 0;
        else if (Paused == false)
            Time.timeScale = 1;
    }


}
