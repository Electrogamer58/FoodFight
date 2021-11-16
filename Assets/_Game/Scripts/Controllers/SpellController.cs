using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] Health _playerHealthComponent = null;
    [SerializeField] Health _enemyHealthComponent = null;
    [SerializeField] Animator _playerAnimator = null;
    [SerializeField] PlayerTurnBattleState _playerTurnAccess = null;

    private ICommand _spellCommand;

    public int spellTurnCount = 0;

    [Header("Spell Animations")]
    //public Animation _healingAnimation = null;
    AnimationClip animClip;

    [Header("Spell Shop Buttons")]
    public GameObject Spell2;
    public GameObject Spell3;
    public GameObject Spell4;
    public GameObject Spell5;
    public GameObject Spell6;


    //NOTE: THERE IS DEFINITELY A BETTER WAY TO DO THIS. I WILL COME BACK TO THIS IF I HAVE TIME OR NEED TO EXPAND ON IT
    bool canBuy2 = true;
    bool canBuy3 = true;
    bool canBuy4 = true;
    bool canBuy5 = true;
    bool canBuy6 = true;

    public void BuySpell2()
    {
        if (canBuy2)
        {
            Spell2.SetActive(true);
            canBuy2 = false;
        }
    }

    public void BuySpell3()
    {
        if (canBuy3)
        {
            Spell3.SetActive(true);
            canBuy3 = false;
        }
    }

    public void BuySpell4()
    {
        if (canBuy4)
        {
            Spell4.SetActive(true);
            canBuy4 = false;
        }
    }

    public void BuySpell5()
    {
        if (canBuy5)
        {
            Spell5.SetActive(true);
            canBuy5 = false;
        }
    }

    public void BuySpell6()
    {
        if (canBuy6)
        {
            Spell6.SetActive(true);
            canBuy6 = false;
        }
    }

    public void UsedHealingHaiku()
    {
        //hook into commands
        _spellCommand = new HealingHaiku(_playerAnimator, _playerHealthComponent);
        _spellCommand.Execute();
        _playerHealthComponent.UseMagic();
        _playerTurnAccess.GoToEnemyState();
    }

    public void UsedCreatePancake()
    {
        _enemyHealthComponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        //hook into commands
        _spellCommand = new CreatePancake(_playerAnimator, _playerHealthComponent, _enemyHealthComponent);
        _spellCommand.Execute();
        _playerHealthComponent.UseMagic();
        _playerTurnAccess.GoToEnemyState();
    }

    public void UsedInAPickle()
    {
        //hook into commands
        if (spellTurnCount == 0)
        {
            spellTurnCount = 3;
            _spellCommand = new InAPickle(_playerAnimator, _playerHealthComponent);
            _spellCommand.Execute();
            _playerHealthComponent.UseMagic();
            _playerTurnAccess.GoToEnemyState();
        } else
        {
            Debug.Log("Pickle already deployed.");
        }
    }

    public void UsedExcalibread()
    {
        _enemyHealthComponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        //hook into commands
        _spellCommand = new Excalibread(_playerAnimator, _playerHealthComponent, _enemyHealthComponent);
        _spellCommand.Execute();
        _playerHealthComponent.UseMagic();
        _playerTurnAccess.GoToEnemyState();
    }

    public void UsedLastStand()
    {
        _enemyHealthComponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        //hook into commands
        _spellCommand = new LastStand(_playerAnimator, _playerHealthComponent, _enemyHealthComponent);
        _spellCommand.Execute();
        _playerHealthComponent.UseMagic();
        _playerTurnAccess.GoToEnemyState();
    }

    
}
