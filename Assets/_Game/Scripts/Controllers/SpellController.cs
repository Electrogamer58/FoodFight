using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] Health _playerHealthComponent = null;
    [SerializeField] Health _enemyHealthComponent = null;
    [SerializeField] Animator _playerAnimator = null;
    [SerializeField] PlayerTurnBattleState _playerTurnAccess = null;

    [SerializeField] GameObject _cantBuy = null;



    private ICommand _spellCommand;

    public int spellTurnCount = 0;

    [Header("Spell Animations")]
    //public Animation _healingAnimation = null;
    AnimationClip animClip;
    [SerializeField] GameObject _pickle = null;

    [Header("Spell Sounds")]
    [SerializeField] AudioSource _audioSource = null;
    [SerializeField] AudioClip _sound1 = null;
    [SerializeField] AudioClip _sound2 = null;
    [SerializeField] AudioClip _sound3 = null;
    [SerializeField] AudioClip _sound4 = null;
    [SerializeField] AudioClip _downgrade = null;

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
        if (canBuy2 && PlayerScore.Tastecoins >= 15)
        {
            Spell2.SetActive(true);
            PlayerScore.Tastecoins -= 15;
            canBuy2 = false;
        }
        else
        {
            _playerTurnAccess.RefreshState();
            _cantBuy.SetActive(true);
            DelayHelper.DelayAction(this, DisableCantBuy, 1f);
        }
    }

    public void BuySpell3()
    {
        if (canBuy3 && PlayerScore.Tastecoins >= 30)
        {
            Spell3.SetActive(true);
            PlayerScore.Tastecoins -= 30;
            canBuy3 = false;
        }
        else
        {
            _playerTurnAccess.RefreshState();
            _cantBuy.SetActive(true);
            DelayHelper.DelayAction(this, DisableCantBuy, 1f);
        }
    }

    public void BuySpell4()
    {
        if (canBuy4 && PlayerScore.Tastecoins >= 130)
        {
            Spell4.SetActive(true);
            PlayerScore.Tastecoins -= 130;
            canBuy4 = false;
        }
        else
        {
            _playerTurnAccess.RefreshState();
            _cantBuy.SetActive(true);
            DelayHelper.DelayAction(this, DisableCantBuy, 1f);
        }
    }

    public void BuySpell5()
    {
        if (canBuy5 && PlayerScore.Tastecoins >= 50)
        {
            Spell5.SetActive(true);
            PlayerScore.Tastecoins -= 50;
            canBuy5 = false;
        }
        else
        {
            _playerTurnAccess.RefreshState();
            _cantBuy.SetActive(true);
            DelayHelper.DelayAction(this, DisableCantBuy, 1f);
        }
    }

    public void BuySpell6()
    {
        if (canBuy6)
        {
            Spell6.SetActive(true);
            canBuy6 = false;
        }
        else
        {
            _playerTurnAccess.RefreshState();
            _cantBuy.SetActive(true);
            DelayHelper.DelayAction(this, DisableCantBuy, 1f);
        }
    }

    void DisableCantBuy()
    {
        _cantBuy.SetActive(false);
    }

    public void UsedHealingHaiku()
    {
        if (_playerHealthComponent._currentSugar >= 3)
        {
            //hook into commands
            _spellCommand = new HealingHaiku(_playerAnimator, _playerHealthComponent);
            _spellCommand.Execute();
            _playerHealthComponent.UseMagic();
            _playerTurnAccess.GoToEnemyState();

            _audioSource.clip = _sound1;
            _audioSource.Play();
        }
    }

    public void UsedCreatePancake()
    {
        if (_playerHealthComponent._currentSugar >= 4)
        {
            _enemyHealthComponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
            //hook into commands
            _spellCommand = new CreatePancake(_playerAnimator, _playerHealthComponent, _enemyHealthComponent);
            _spellCommand.Execute();
            _playerHealthComponent.UseMagic();
            _playerTurnAccess.GoToEnemyState();

            _audioSource.clip = _sound2;
            _audioSource.Play();
        }
    }

    public void UsedInAPickle()
    {
        //hook into commands
        if (_playerHealthComponent._currentSugar >= 2)
        {
            if (spellTurnCount == 0)
            {
                spellTurnCount = 3;
                _spellCommand = new InAPickle(_playerAnimator, _playerHealthComponent, _pickle);
                _spellCommand.Execute();
                _playerHealthComponent.UseMagic();
                _playerTurnAccess.GoToEnemyState();

                _audioSource.clip = _sound3;
                _audioSource.Play();
            }
            else
            {
                Debug.Log("Pickle already deployed.");
            }
        }
    }

    public void UndoPickle()
    {
        _spellCommand = new InAPickle(_playerAnimator, _playerHealthComponent, _pickle);
        _spellCommand.Undo();

        _audioSource.clip = _downgrade;
        _audioSource.Play();
    }

    public void UsedExcalibread()
    {
        if (_playerHealthComponent._currentSugar >= 6)
        {
            _enemyHealthComponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
            //hook into commands
            _spellCommand = new Excalibread(_playerAnimator, _playerHealthComponent, _enemyHealthComponent);
            _spellCommand.Execute();
            _playerHealthComponent.UseMagic();
            DelayHelper.DelayAction(this, _playerTurnAccess.GoToEnemyState, 3f);

            _audioSource.clip = _sound4;
            _audioSource.Play();
        }
    }

    public void UsedLastStand()
    {
        if (_playerHealthComponent._currentSugar >= 4)
        {
            _enemyHealthComponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
            //hook into commands
            _spellCommand = new LastStand(_playerAnimator, _playerHealthComponent, _enemyHealthComponent);
            _spellCommand.Execute();
            _playerHealthComponent.UseMagic();
            _playerTurnAccess.GoToEnemyState();

            _audioSource.clip = _sound4;
            _audioSource.Play();
        }
    }

    
}
